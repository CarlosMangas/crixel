using UnityEngine;
using System.Collections;

public class VirusAlphaScript : EnemyEntityScript
{
    // Use this for initialization
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _movementScript = gameObject.GetComponent<EntityMovementScript>();
        _animator = gameObject.GetComponent<Animator>();
        _shootingScript = gameObject.GetComponent<ShootingScript>();

        FSM = new EnemyEntityFSM();

        _visionCollider = _transform.Find("Vision_collider").GetComponent<EnemyColliderHelperScript>();
        _attackCollider = _transform.Find("Attack_collider").GetComponent<EnemyColliderHelperScript>();

        _visionCollider.Collided += new CollidedEventHandler(VisionChanged);
        _attackCollider.Collided += new CollidedEventHandler(AttackChanged);

        _attackCooldown = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(_attackCooldown == 0)
            CheckState();
        else
            ReduceCooldown();
    }

    private void CheckState()
    {
        switch(FSM.CurrentState)
        {
            case EnemyEntityState.Iddle:

                if(Health <= 0)
                {
                    FSM.MoveNext(EnemyCommand.Die);
                    break;
                }

                if(_playerAttackable)
                {
                    FSM.MoveNext(EnemyCommand.Attack);
                    break;
                }

                if(_playerVisible)
                {
                    FSM.MoveNext(EnemyCommand.Chase);       
                    break;
                }

                break;
            case EnemyEntityState.Chasing:

                if(Health <= 0)
                {
                    _movementScript.StopMoving();
                    FSM.MoveNext(EnemyCommand.Die);
                    break;
                }

                if(_playerAttackable)
                {
                    _movementScript.StopMoving();
                    FSM.MoveNext(EnemyCommand.Attack);
                    break;
                }

                if(!_playerVisible)
                {
                    _movementScript.StopMoving();
                    FSM.MoveNext(EnemyCommand.Stop);
                    break;
                }

                ChasePlayer();

                break;
            case EnemyEntityState.Attacking:

                if(Health <= 0)
                {
                    FSM.MoveNext(EnemyCommand.Die);
                    break;
                }
                
                if(!_playerAttackable)
                {
                    FSM.MoveNext(EnemyCommand.Stop);
                    break;
                }

                AttackPlayer();

                FSM.MoveNext(EnemyCommand.Stop);
                
                break;
        }
    }

    private void ReduceCooldown()
    {
        _attackCooldown -= Time.deltaTime;

        if(_attackCooldown <= 0)
            _attackCooldown = 0;
    }

    private void ChasePlayer()
    {
        if(GameObject.Find("Crixel").transform.position.x > _transform.position.x)
            _movementScript.GoRight();
        else
            _movementScript.GoLeft();
    }

    private void AttackPlayer()
    {
        _shootingScript.Shoot();
        _attackCooldown = 2;
    }

    private void VisionChanged(bool state)
    {
        _playerVisible = state;
    }

    private void AttackChanged(bool state)
    {
        _playerAttackable = state;
    }

    private ShootingScript _shootingScript;
    private EnemyColliderHelperScript _visionCollider;
    private EnemyColliderHelperScript _attackCollider;
    private bool _playerVisible;
    private bool _playerAttackable;
    private float _attackCooldown;
}
