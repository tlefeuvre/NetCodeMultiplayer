using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBarricade : MonoBehaviour
{
    public GameObject barricade;
    private bool IsOnCollider = false;
    // Update is called once per frame
    void Update()
    {
        if (IsOnCollider)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Construct barricade");
                Instantiate(barricade, this.transform.position, this.transform.rotation);
                Destroy(gameObject);
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
}
