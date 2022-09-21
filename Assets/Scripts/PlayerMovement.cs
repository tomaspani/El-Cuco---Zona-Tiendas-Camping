using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    public float DashSpeed;

    public float groundDrag;

    [Header("Grounde Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private bool _grounded;
    public LayerMask ground;


    public Transform orientation;

    private float _horizontalInput, _verticalInput;
    private Vector3 moveDirection;

    Rigidbody myRb;

    private Dash dash;
    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.freezeRotation = true;
        dash = GetComponent<Dash>();
    }

    private void Update()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, ground);

        MyInput();
        SpeedControl();

        if (_grounded)
            myRb.drag = groundDrag;
        else
            myRb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        myRb.AddForce(moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
    }


    private void SpeedControl()
    {

        Vector3 flatVel = new Vector3(myRb.velocity.x, 0f, myRb.velocity.z);

        if (flatVel.magnitude > _moveSpeed && !dash.isDashing)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            myRb.velocity = new Vector3(limitedVel.x, myRb.velocity.y, limitedVel.z);
        }
       
    }

}
