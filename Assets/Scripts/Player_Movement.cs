using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Net.Sockets;

public class Player_Movement : NetworkBehaviour
{

    private bool onground = false;

    Animator animator;

    Rigidbody m_Rigidbody;

    [SerializeField] GameObject cam;

    [SerializeField] float Speed = 10.0f;
    [SerializeField] float mouseSensitivityX = 6.0f;
    [SerializeField] float mouseSensitivityY = 6.0f;
    private float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        jumpForce = 2.75f;
        

        //animator = GetComponent<Animator>();
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
        if (IsOwner)
        { }
            float xMov = Input.GetAxisRaw("Horizontal");
            float zMov = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown("space") && onground == true)
            {
                m_Rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            }

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
            rotateCamera(cameraRotation);

            //gestion des animations
            AnimationsManagement();
        //}
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
        if(cam.transform.rotation.x > -0.70 && cam.transform.rotation.x < 0.70)
        {
            cam.transform.Rotate(-_cameraRotation);
        }

        if(cam.transform.rotation.x < -0.70)
        {
            _cameraRotation.x = -0.70f;
            cam.transform.Rotate(-_cameraRotation);
        }
        if(cam.transform.rotation.x > 0.70)
        {
            _cameraRotation.x = 0.70f;
            cam.transform.Rotate(-_cameraRotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
     if (collision.gameObject.tag == "Ground")
        {
            onground = true;
            Debug.Log("pte");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onground = false;
        }
    }

        private void AnimationsManagement()
    {
        if (Input.GetKey("z"))
        {
            animator.SetBool("IsWalkingForward", true);
        }
        else
        {
            animator.SetBool("IsWalkingForward", false);
        }

        if (Input.GetKey("s"))
        {
            animator.SetBool("IsWalkingBackward", true);
        }
        else
        {
            animator.SetBool("IsWalkingBackward", false);
        }
    }
}
