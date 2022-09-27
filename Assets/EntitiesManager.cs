using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EntitiesManager : MonoBehaviour
{
    public static EntitiesManager Instance;
    public int nbzombies=0;
    public int nbzombiesKilled=0;
    public int maxZombies=50;
    public GameObject kills;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        maxZombies = 50;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        kills.GetComponent<TextMeshPro>().text = nbzombiesKilled.ToString();
    }
}
