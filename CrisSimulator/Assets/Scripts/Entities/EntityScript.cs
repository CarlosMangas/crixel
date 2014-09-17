using UnityEngine;
using System.Collections;

public enum LookDirection
{
    Right,
    Left
}

public class EntityScript : MonoBehaviour
{
    [HideInInspector]
    public Transform _transform;

    [HideInInspector]
    public EntityMovementScript _movementScript;

    [HideInInspector]
    public Animator _animator;

    public int Health;

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
