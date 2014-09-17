using UnityEngine;
using System.Collections;

public class EntityMovementScript : MonoBehaviour
{
    public float Speed;

    public LookDirection EntityLookDirection { get; set; } 
    public bool Jumping
    { 
        get
        {
            return !(_verticalAceleration == 0 && rigidbody2D.velocity.y == 0);
        }
    }

    void Awake()
    {
        EntityLookDirection = LookDirection.Left;
    }

    // Use this for initialization
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
    }

    // Fixed update for movement
    void FixedUpdate()
    {
        Vector2 moveVector = Vector2.one;

        moveVector.x = Speed * _horizontalAceleration;
        moveVector.y = Speed * _verticalAceleration;

        _transform.Translate(moveVector * Time.deltaTime, Space.World);

        if(Jumping)
        {
            HandleJump();
        }
    }

    public void GoRight()
    {
        if(EntityLookDirection == LookDirection.Left)
        {
            EntityLookDirection = LookDirection.Right;
            _horizontalAceleration = 0;
            FlipImage();
        }

        if(_horizontalAceleration < Utils.Constants.MAX_ACELERATION)
            _horizontalAceleration += Utils.Constants.INCREASE_IN_ACELERATION;
    }

    public void GoLeft()
    {
        if(EntityLookDirection == LookDirection.Right)
        {
            EntityLookDirection = LookDirection.Left;
            _horizontalAceleration = 0;
            FlipImage();
        }

        if(_horizontalAceleration > -Utils.Constants.MAX_ACELERATION)
            _horizontalAceleration -= Utils.Constants.INCREASE_IN_ACELERATION;
    }

    public void Jump()
    { 
        if(!Jumping)
        {
            _verticalAceleration = Utils.Constants.JUMPING_ACELERATION;
        }
    }

    void HandleJump()
    {
        _verticalAceleration -= Utils.Constants.GRAVITY;
            
        if(_verticalAceleration < 0)
            _verticalAceleration = 0;
    }

    public void StopMoving()
    {
        _horizontalAceleration = 0;
    }

    // Maybe this shouldn't be here
    private void FlipImage()
    {
        var newScale = _transform.localScale;
        newScale.x *= -1;
        _transform.localScale = newScale;
    }

    private Transform _transform;
    private float _horizontalAceleration;
    private float _verticalAceleration;
}
