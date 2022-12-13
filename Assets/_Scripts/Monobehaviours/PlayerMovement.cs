using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rb;

    float _moveHorizontal;
    float _moveVertical;
    Vector3 forward;
    Vector3 right;
    Vector3 forwardRelativeVertical;
    Vector3 rightRelativeHorizontal;
    [SerializeField]float _movementSpeed;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");

        forward = Camera.main.transform.forward;
        right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward = forward.normalized;
        right = right.normalized;

        forwardRelativeVertical = _moveVertical * forward;
        rightRelativeHorizontal = _moveHorizontal * right;

    }

    private void FixedUpdate()
    {
        MovePlayerRelativeToCamera();
    }

    private void MovePlayerRelativeToCamera()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");

        forward = Camera.main.transform.forward;
        right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward = forward.normalized;
        right = right.normalized;

        forwardRelativeVertical = _moveVertical * forward;
        rightRelativeHorizontal = _moveHorizontal * right;

        Vector3 cameraRelativeMovement = forwardRelativeVertical + rightRelativeHorizontal;

        if(_moveHorizontal != 0f || _moveVertical != 0f)
        {
            _rb.AddForce(cameraRelativeMovement * _movementSpeed);
        }
    }

    private void Move()
    {
        if(_moveHorizontal != 0f)
        {
            _rb.AddForce(new Vector3(_moveHorizontal * _movementSpeed, 0f, 0f));
            //_rb.velocity = new Vector3(_moveHorizontal * _movementSpeed, 0f, 0f);
        }
        if(_moveVertical != 0f)
        {
            _rb.AddForce(new Vector3(0f, 0f, _moveVertical * _movementSpeed));
            //_rb.velocity = new Vector3(0f, 0f, _moveVertical * _movementSpeed);
        }
    }
}
