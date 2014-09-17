using UnityEngine;
using System.Collections;

public class VirusAlphaBulletScript : BulletScript
{

    // Use this for initialization
    void Start()
    {
        StartBullet();
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "EnemyBullet")
        {
            var _orientation = Quaternion.LookRotation(collision.contacts[0].normal);
            var pSystem = CreateParticleSystem(_orientation);
            Destroy(pSystem.gameObject,pSystem.duration);
            
            Destroy(gameObject);
        }
    }
}
