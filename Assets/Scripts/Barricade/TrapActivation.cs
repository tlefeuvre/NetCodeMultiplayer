using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TrapActivation : NetworkBehaviour
{
    public GameObject trap;
    public GameObject textInfo;
    public Animator animator;
    private bool flagIsActive = false;
    private bool IsOnCollider = false;
    private void Start()
    {

    }
    private void Update()
    {
        if (!animator && IsHost) 
        {
            animator = trap.GetComponent<Animator>();
        }
        if (IsOnCollider)
        {
            Debug.Log("DANS COLLIGER");
            Debug.Log(flagIsActive);
            textInfo.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && flagIsActive == false)
            {
                Debug.Log("Trap Activation");
                textInfo.SetActive(false);
                animator.enabled = true;
                StartCoroutine("getActive");
            }
        }
        else
            textInfo.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //si on percute un joueur, le monstre meurt
        {
            IsOnCollider = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IsOnCollider = false;
    }

        IEnumerator getActive() //La coroutine sert à désactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons à la fin
    {
        flagIsActive = true;
        yield return new WaitForSeconds(6f);
        flagIsActive = false;
        animator.enabled = false;
    }
}
