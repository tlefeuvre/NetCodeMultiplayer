using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Net.Sockets;

public class Player_Movement : NetworkBehaviour
{

    Vector3 position;

    private float jumpState;
    private bool onground = false;

    Animator animator;

    Rigidbody m_Rigidbody;

    [SerializeField] GameObject cam;

    [SerializeField] float Speed = 5;
    [SerializeField] float mouseSensitivityX = 6;
    [SerializeField] float mouseSensitivityY = 6;
    [SerializeField] float jumpForce = 6;

    // Start is called before the first frame update
    void Start()
    {
        jumpState = 0;
        Speed = 10.0f;

        animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        if (!IsOwner)
            cam.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsOwner) { }
            float xMov = Input.GetAxisRaw("Horizontal");
            float zMov = Input.GetAxisRaw("Vertical");

            Vector3 moveHorizontal = transform.right * xMov;
            Vector3 moveVertical = transform.forward * zMov;

            Vector3 velocity = (moveHorizontal + moveVertical).normalized * Speed;

            movePlayer(velocity);

            //mouse managment (left/right)
            float yRot = Input.GetAxisRaw("Mouse X");
            Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;
            rotatePlayer(rotation);

        //mouse managment (up/down)
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRot, 0, 0) * mouseSensitivityY;
        if (cam.transform.rotation.x >= -90 && cam.transform.rotation.x <= 90)
        {
            rotateCamera(cameraRotation);
        }

        //gestion des animations
        AnimationsManagement();


        //jump
        position = transform.position;

        if (Input.GetKeyDown("space") && jumpState == 0)
        {
            jumpState = 1;
        }

        if (jumpState > 0)
        {
            position.y += Time.fixedDeltaTime * jumpForce;
            transform.position = position;

            if (jumpState > 0)
            {
                position.y += Time.fixedDeltaTime;
                transform.position = position;

                jumpState -= Time.fixedDeltaTime;
                if (jumpState < 0)
                {
                    jumpState = 0;
                }
            }

        }
       
    }


    private void movePlayer(Vector3 _velocity)
    {
        if(_velocity != Vector3.zero)
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + _velocity * Time.fixedDeltaTime);
        }
    }

    private void rotatePlayer(Vector3 _rotation)
    {
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * Quaternion.Euler(_rotation));
    }

    private void rotateCamera(Vector3 _cameraRotation)
    {
        if(cam.transform.rotation.x > -90 && cam.transform.rotation.x < 90)
        {
            cam.transform.Rotate(-_cameraRotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            onground = true;
        }
    }

    private void AnimationsManagement()
    {
        Debug.Log("la go la c'est ptetre une fille bien");
        if (Input.GetKey("z"))
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalkingForward", true);
        }
        else
        {
            animator.SetBool("IsWalkingForward", false);
        }
        if (Input.GetKey("s"))
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalkingBackward", true);
        }
        else
        {
            animator.SetBool("IsWalkingBackward", false);
        }
        if (!Input.GetKey("z") && !Input.GetKey("s"))
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsWalkingBackward", false);
            animator.SetBool("IsWalkingForward", false);
        }
    }
}
