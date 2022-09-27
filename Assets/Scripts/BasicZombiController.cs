using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;

public class BasicZombiController : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject[] players;
    public Vector3 playerPosition;
    public NavMeshSurface[] surfaces;

    public NavMeshAgent agent;
    private Animator animator;
    private bool isAnimationEnabled = false;
    private float startAnimationRandom = 0;

    public float currentHealth=100;
    public float maxHealth = 100;

    void Start()
    {
        currentHealth = maxHealth;

        startAnimationRandom = Random.Range(0, 5.0f);
        animator = GetComponent<Animator>();
        players = GameObject.FindGameObjectsWithTag("Player");


        StartCoroutine(ExampleCoroutine());

    }
    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(startAnimationRandom);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        isAnimationEnabled = true;
    }

    private Vector3 getNearestPlayerPosition()
    {
        int id = 0;
        float nearestDistance=-1;
        for(int i=0;i< players.Length;i++)
        {
            float tmp = Vector3.Distance(this.transform.position, players[i].transform.position);
            if (tmp < 0)
                tmp *= -1;

            if(tmp < nearestDistance || nearestDistance == -1)
            {
                nearestDistance = tmp;
                id = i;
            }
        }
         return players[id].transform.position;

    }
    IEnumerator DestroyZombie()
    {

        // suspend execution for 10 seconds
        yield return new WaitForSeconds(5);
        EntitiesManager.nbzombies -= 1;
        EntitiesManager.nbzombiesKilled += 1;

        NetworkObject m_SpawnedNetworkObject = this.GetComponent<NetworkObject>();
        m_SpawnedNetworkObject.Despawn();

    }
    // Update is called once per frame
    void Update()
    {
        if(currentHealth >0)
        {
            gameObject.GetComponent<Animator>().enabled = true;

        }
        if (currentHealth <= 0 && IsHost)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            gameObject.GetComponent<Animator>().enabled = false;
            StartCoroutine(DestroyZombie());
           
        }
        if (currentHealth <= 0 && !IsHost)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;

            gameObject.GetComponent<Animator>().enabled = false;

        }

        if (players.Length ==0)
        {
            Debug.Log("search player");
            players = GameObject.FindGameObjectsWithTag("Player");


        }

        if (isAnimationEnabled)
        {
            if(gameObject.tag == "BasicZombie")
                animator.SetBool("isWalking", true);
            else if (gameObject.tag == "BasicZombie2")
                animator.SetBool("isWalking2", true);
            else if (gameObject.tag == "FastZombie")
                animator.SetBool("isRunning", true);
            else
                animator.SetBool("isWalking", true);

            //animator.Play("ZombieWalk");
            Debug.Log("zombie avance");
            if (agent && currentHealth >0)
                agent.SetDestination(getNearestPlayerPosition());
            if(agent && currentHealth <0)
                agent.SetDestination(transform.position);



        }
        //animator.SetBool("isRunning", true);
        if (Input.GetKeyDown(KeyCode.Space))
            for (int i = 0; i < surfaces.Length; i++)
            {
                surfaces[i].BuildNavMesh();
            }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TOUCHE " + collision.transform.tag);
        if (collision.transform.tag == "Bullet")
        {
            Debug.Log("TOUCHE BALLE");
            currentHealth -= 30;

        }
        if (collision.transform.tag == "Trap")
        {
            Debug.Log("TOUCHER PIEGE");
            currentHealth -= -1000;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trap")
        {
            Debug.Log("TOUCHER PIEGE");
            currentHealth = 0;

        }
    }
}
