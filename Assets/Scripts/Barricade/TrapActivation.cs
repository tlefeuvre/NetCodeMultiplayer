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
    public NetworkVariable<bool> _isActivate = new(writePerm: NetworkVariableWritePermission.Owner);

    private void Start()
    {

    }
    private void Update()
    {
        if (_isActivate.Value)
        {
            ActivateTrap();
        }
        if (trap && !animator && IsHost)
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

                if (IsHost)
                {
                    Debug.Log("activate Trap host");
                    //_isActivate.Value = true;
                    ActivateTrap();
                }
                else
                {
                    Debug.Log("activate Trap client");
                    SubmitRequestActivateTrapServerRpc();
                }

            }
        }   
        else
            textInfo.SetActive(false);
       
    }
    [ServerRpc(RequireOwnership = false)]
    private void SubmitRequestActivateTrapServerRpc(ServerRpcParams rpcParams = default)
    {
        ActivateTrap();
    }
    private void ActivateTrap()
    {
        Debug.Log("activate Trap");
        textInfo.SetActive(false);
        animator.enabled = true;
        StartCoroutine("getActive");
        
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

        if (other.gameObject.tag == "Player") //si on percute un joueur, le monstre meurt
        {
            IsOnCollider = false;
        }
    }

        IEnumerator getActive() //La coroutine sert ? d?sactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons ? la fin
    {
        flagIsActive = true;
        yield return new WaitForSeconds(6f);
        flagIsActive = false;
        animator.enabled = false;
        if (IsHost)
            _isActivate.Value = false;
    }
}
