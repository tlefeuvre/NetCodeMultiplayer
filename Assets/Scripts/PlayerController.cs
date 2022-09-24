using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rb;
    private Vector3 _input;
    private float _velocity = 80;
    private float _maxVelocity = 10;

    private void Awake()
    {
        _rb = GetComponent < Rigidbody>();
    }
    private void Update()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _rb.velocity += _input.normalized * (_velocity * Time.deltaTime);
        _rb.velocity +=Vector3.ClampMagnitude(_rb.velocity,_maxVelocity);

    }
}
