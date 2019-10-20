using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed { get; set; }
    public event Action<Collider> OnTriggerEnterEvent;
    public event Action Die;

    private Rigidbody _rigidbody;

    private enum MoveDirection
    {
        Forward,
        Right
    }

    private MoveDirection _moveDirection = MoveDirection.Forward;
    private Vector3 _moveVector;
    private AbstractInput _input;


    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = MainController.Instance.GetInput;
    }


    private void Update()
    {
        if ( _input.Tap )
            ChangeDirection();

        CheckDie();
    }


    private void ChangeDirection()
    {
        _moveDirection = _moveDirection == MoveDirection.Forward ? MoveDirection.Right : MoveDirection.Forward;
        _moveVector = GetVector( _moveDirection );
    }


    private void FixedUpdate()
    {
        var vel = _moveVector * Speed * Time.deltaTime;
        vel.y = _rigidbody.velocity.y;
        _rigidbody.velocity = vel;
    }


    private Vector3 GetVector( MoveDirection direction )
    {
        switch ( direction )
        {
            case MoveDirection.Forward:
                return Vector3.forward;

            case MoveDirection.Right:
                return Vector3.right;
        }

        return Vector3.zero;
    }


    private void OnTriggerEnter( Collider other )
    {
        OnTriggerEnterEvent?.Invoke( other );
    }


    private void CheckDie()
    {
        if ( transform.position.y < -1f )
        {
            Die?.Invoke();
        }
    }
}