using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivation : MonoBehaviour
{
    public GameObject trap;
    private Animator animator;
    private bool flagIsActive = false;
    private bool IsOnCollider = false;
    private void Start()
    {
        animator = trap.GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsOnCollider)
        {
            if (Input.GetKeyDown(KeyCode.E) && flagIsActive == false)
            {
                Debug.Log("Trap Activation");
                animator.enabled = true;
                StartCoroutine("getActive");
            }
        }
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
