using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            Vector3 newPos = player.transform.position;
            newPos.y += 1.5f;
            transform.position = newPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 newPos = player.transform.position;
            newPos.y += 1.5f;
            transform.position = newPos;

        }
    }
}
