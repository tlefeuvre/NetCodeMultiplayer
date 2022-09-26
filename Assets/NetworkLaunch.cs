using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
public class NetworkLaunch : MonoBehaviour
{
    public void StartHost()
    {
        Debug.Log("serv");
        PlayerPrefs.SetInt("Type", 0);
        SceneManager.LoadScene(1);
    }
    public void StartClient()
    {
        Debug.Log("client");

        PlayerPrefs.SetInt("Type", 1);
        SceneManager.LoadScene(1);

    }
}
