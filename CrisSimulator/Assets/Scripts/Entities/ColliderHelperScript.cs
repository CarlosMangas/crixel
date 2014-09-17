using UnityEngine;
using System.Collections;

public delegate void CollidedEventHandler(bool state);

public abstract class ColliderHelperScript : MonoBehaviour
{
    public event CollidedEventHandler Collided;

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void OnTriggerEnter2D(Collider2D coll);
    public abstract void OnTriggerExit2D(Collider2D coll);

    protected virtual void OnCollided(bool state) 
    {
        if (Collided != null)
            Collided(state);
    }
}
