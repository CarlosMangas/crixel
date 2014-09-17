using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour
{

    public Transform Bullet;

	// Use this for initialization
	void Start () 
    {
        _transform = gameObject.GetComponent<Transform>();
        _movementScript = gameObject.GetComponent<EntityMovementScript>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Shoot()
    {
        Transform _newBullet = (Transform)GameObject.Instantiate(Bullet);
        _newBullet.transform.position = _transform.position;
        var _correctPosition = _newBullet.transform.position;
        
        if(_movementScript.EntityLookDirection == LookDirection.Left)
        {
            _correctPosition.x -= (gameObject.GetComponent<BoxCollider2D>().collider2D.bounds.size.x);
        }
        else
        {
            var _correctScale = _newBullet.transform.localScale;
            _correctScale.x *= -1;
            _newBullet.transform.localScale = _correctScale;
            
            _correctPosition.x += (gameObject.GetComponent<BoxCollider2D>().collider2D.bounds.size.x);
        }
        
        _newBullet.transform.position = _correctPosition;
        _newBullet.transform.GetComponent<DropScript>().DropLookDirection = _movementScript.EntityLookDirection;
    }

    private Transform _transform;
    private EntityMovementScript _movementScript;
}
