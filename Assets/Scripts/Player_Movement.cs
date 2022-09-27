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
    [SerializeField] GameObject armsNRiffle;

    [SerializeField] float Speed = 10.0f;
    [SerializeField] float mouseSensitivityX = 4.0f;
    [SerializeField] float mouseSensitivityY = 3.0f;
    [SerializeField] private float jumpForce = 2.75f;

    // Start is called before the first frame update
    void Start()
    {
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
        if (IsOwner)
        {
            Debug.Log(cam.transform.localEulerAngles.x);

        }
        float xMov = Input.GetAxisRaw("Horizontal");
            float zMov = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && onground == true)
            {
                m_Rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            }

            Vector3 moveHorizontal = transform.right * xMov;
            Vector3 moveVertical = transform.forward * zMov;

            Vector3 velocity = (moveHorizontal + moveVertical).normalized * Speed;

            movePlayer(velocity);

            //mouse managment (left/right)
            float yRot = Input.GetAxisRaw("Mouse X") * mouseSensitivityX;
            Vector3 rotation = new Vector3(0, yRot, 0);
            rotatePlayer(rotation);

        //mouse managment (up/down)
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRot, 0, 0) * mouseSensitivityY;
        rotateCamera(cameraRotation);

        Debug.Log(cam.transform.localEulerAngles.x);

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

    private void rotateCamera(Vector3 cameraRotation)
    {
        if(cam.transform.localEulerAngles.x > 300 || cam.transform.localEulerAngles.x < 60)
        {
            cam.transform.Rotate(-cameraRotation);
            armsNRiffle.transform.Rotate(-cameraRotation);
        }

        if (cam.transform.localEulerAngles.x > 180 && cam.transform.localEulerAngles.x < 300)
        {
            cam.transform.localEulerAngles = new Vector3(305, 0, 0);
            armsNRiffle.transform.localEulerAngles = new Vector3(305, 0, 0);
        }
        if(cam.transform.localEulerAngles.x <= 180 && cam.transform.localEulerAngles.x > 60)
        {
            cam.transform.localEulerAngles = new Vector3(55, 0, 0);
            armsNRiffle.transform.localEulerAngles = new Vector3(55, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
     if (collision.gameObject.tag == "Ground")
        {
            onground = true;
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
        if (Input.GetKey(KeyCode.W))
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
