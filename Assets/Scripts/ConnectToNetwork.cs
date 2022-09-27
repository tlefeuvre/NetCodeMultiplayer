using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class ConnectToNetwork : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LaunchHost());
    }
    IEnumerator LaunchHost()
    {
        yield return new WaitForSeconds(0.1f);
        if (PlayerPrefs.GetInt("Type") == 0)
        {
            NetworkManager.Singleton.StartHost();

        }
        if (PlayerPrefs.GetInt("Type") == 1)
        {
            NetworkManager.Singleton.StartClient();

        }
    }

}
