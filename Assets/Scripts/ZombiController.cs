using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Vector3 playerPosition;
    public NavMeshSurface[] surfaces;

    public NavMeshAgent agent;
    public 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player)
            playerPosition = player.transform.position;
    }
    private void getPlayerPosition()
    {
        if (player)
            playerPosition = player.transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            for (int i = 0; i < surfaces.Length; i++)
            {
                surfaces[i].BuildNavMesh();
            }
        getPlayerPosition();
        if(agent)
            agent.SetDestination(playerPosition);
    }
}
