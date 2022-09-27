using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EntitiesManager : MonoBehaviour
{
    public static int nbzombies =0;
    public static int nbzombiesKilled=0;
    public static int maxZombies=50;
    public GameObject kills;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        maxZombies = 50;
        nbzombies = 0;
        nbzombiesKilled = 0;
    }
    private void Update()
    {
        kills.GetComponent<TMP_Text>().text = nbzombiesKilled.ToString();
    }
}
