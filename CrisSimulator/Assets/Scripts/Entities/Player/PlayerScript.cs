using UnityEngine;
using System.Collections;

public class PlayerScript : EntityScript
{
	// Use this for initialization
	void Start()
	{
		_transform = gameObject.GetComponent<Transform>();
		_movementScript = gameObject.GetComponent<EntityMovementScript>();
        _animator = gameObject.GetComponent<Animator>();
        _shootingScript = gameObject.GetComponent<ShootingScript>();

        _stunned = 0f;
	}

	// Update for inputs
	void Update()
	{
        if(_stunned <= 0)
        {
            //Recieve any command from input
            ProcessInput();
        }
        else
        {
            _stunned -= Time.deltaTime;
           
        }
	}

    void FixedUpdate()
    {
        if(_attacking && _stunned <= 0)
        {
            _shootingScript.Shoot();

            _attacking = false;
        }
    }

    private void ProcessInput()
    {
        //Debug.Log(Input.GetAxis("Horizontal"));

        if(Input.GetAxis("Horizontal") > 0)
        {
            _movementScript.GoRight();
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            _movementScript.GoLeft();
        }
        else
        {
            _movementScript.StopMoving();
        }

        _animator.SetInteger("HorizontalAceleration", (int)(Input.GetAxis("Horizontal") * 100));

        if(Input.GetButtonDown("Jump") && !_movementScript.Jumping)
        {
            _movementScript.Jump();
        }

        _animator.SetBool("Jumping", _movementScript.Jumping);

        if(Input.GetButtonDown("Attack"))
        {
            _attacking = true;
            _animator.SetTrigger("Attacking");
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "EnemyBullet" || coll.gameObject.tag == "Enemy")
        {
            _movementScript.StopMoving();

            _animator.SetTrigger("Hit");
            Health--;

            Stun();

            _stunned = Utils.Constants.STUNNED_TIME;
        }
    }

    private void Stun()
    {
        Vector2 direction = new Vector2(1f, 0.5f);

        if(_movementScript.EntityLookDirection == LookDirection.Right)
        {
            direction.x *= -1;
        }

        rigidbody2D.AddForce(direction * Utils.Constants.STUN_FORCE);
    }
    
    private bool _attacking;
    private ShootingScript _shootingScript;
    private float _stunned;
}
