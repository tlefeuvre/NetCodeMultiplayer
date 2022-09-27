using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ZombieSpawner : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject[] zombies;
    private float nextActionTime = 0.0f;
    public float period = 3f;
    private int[] phasesTimer = new int[5] { 5, 4, 3, 2, 1 };
    private bool ok = false;
    void Start()
    {
        if (IsHost)
        {
            ok = true;

            InvokeRepeating("Spawn", 2.0f, 6f);
            StartCoroutine(StopFirstPhase());
        }

    }
    IEnumerator StopFirstPhase()
    {
        // suspend execution for 10 seconds
        yield return new WaitForSeconds(10);
        Debug.Log("StopFirstPhase - Start Second Phase : " + Time.time);

        CancelInvoke();
        StartCoroutine(StopSecondPhase());
        InvokeRepeating("Spawn", 2.0f, phasesTimer[0]);

    }
    IEnumerator StopSecondPhase()
    {
        // suspend execution for 10 seconds
        yield return new WaitForSeconds(10);
        Debug.Log("Stop Second Phase - Start third Phase  : " + Time.time);

        CancelInvoke();
        StartCoroutine(StopThirdPhase());
        InvokeRepeating("Spawn", 1.0f, phasesTimer[1]);

    }
    IEnumerator StopThirdPhase()
    {
        // suspend execution for 10 seconds
        yield return new WaitForSeconds(10);
        Debug.Log("Stop third Phase - Start fourth Phase  : " + Time.time);

        StartCoroutine(StopFourthPhase());

        CancelInvoke();
        InvokeRepeating("Spawn", 1.0f, phasesTimer[2]);

    }
    IEnumerator StopFourthPhase()
    {
        // suspend execution for 10 seconds
        yield return new WaitForSeconds(10);
        Debug.Log("Stop fourth Phase - Start fifth Phase : " + Time.time);
        StartCoroutine(StopFifthtPhase());

        CancelInvoke();
        InvokeRepeating("Spawn", 1.0f, phasesTimer[3]);

    }
    IEnumerator StopFifthtPhase()
    {
        // suspend execution for 10 seconds
        yield return new WaitForSeconds(10);
        Debug.Log("Stop fifth Phase - Start sith Phase : " + Time.time);

        CancelInvoke();
        InvokeRepeating("Spawn", 1.0f, phasesTimer[4]);

    }
    private void Spawn()
    {
       if(IsHost && EntitiesManager.Instance.nbzombies < 1)
        {
            int randomZombieId = Random.Range(0, zombies.Length);

            int rand = Random.Range(1, zombies[randomZombieId].transform.childCount);

            Debug.Log("SPAWNNNN!!");

            Vector3 pos = new Vector3(0f, -1.0f, 0f);
            GameObject newObject = Instantiate(zombies[randomZombieId]) as GameObject;
            //newObject.transform.GetChild(rand).gameObject.SetActive(true);
            newObject.GetComponent<NetworkObject>().Spawn();
            EntitiesManager.Instance.nbzombies += 1;
        }
       

    }
    private void Update()
    {
        if (IsHost && !ok)
        {
            ok = true;
            InvokeRepeating("Spawn", 2.0f, 6f);
            StartCoroutine(StopFirstPhase());
            Debug.Log("is host!!");
        }
    }

}
