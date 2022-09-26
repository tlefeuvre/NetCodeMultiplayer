using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBarricade : MonoBehaviour
{
    public GameObject barricade;
    public GameObject textInfo;
    private bool IsOnCollider = false;
    // Update is called once per frame
    void Update()
    {
        if (IsOnCollider)
        {
            textInfo.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Construct barricade");
                textInfo.SetActive(false);
                Instantiate(barricade, this.transform.position, this.transform.rotation);
                Destroy(gameObject);
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
}
