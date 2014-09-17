using UnityEngine;
using System.Collections;

public class EnemyColliderHelperScript : ColliderHelperScript
{

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    public override void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.transform.tag == "Player")
        {
            OnCollided(true);
        }
    }

    public override void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.transform.tag == "Player")
        {
            OnCollided(false);
        }
    }
}
