using UnityEngine;
using System.Collections;

public abstract class BulletScript : MonoBehaviour 
{
    [HideInInspector]
    public LookDirection DropLookDirection;
    
    public Transform DropParticleSystem;
    
    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
    }
    
    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void StartBullet()
    {
        if(DropLookDirection == LookDirection.Left)
        {
            _direction = new Vector2(-1f, 0.3f);
        }
        else
        {
            _direction = new Vector2(1f, 0.3f);
        }
        
        rigidbody2D.AddForce(_direction * 20f);
    }
    
    public abstract void OnCollisionEnter2D(Collision2D collision);
    
    public ParticleSystem CreateParticleSystem(Quaternion orientation)
    {
        var _newSplash = (Transform)GameObject.Instantiate(DropParticleSystem, _transform.position, orientation);
        _newSplash.renderer.sortingLayerName = "Front";
        
        return _newSplash.GetComponent<ParticleSystem>();
    }

    private Transform _transform;
    private Vector2 _direction;
}
