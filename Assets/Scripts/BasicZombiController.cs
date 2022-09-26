using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;

public class BasicZombiController : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
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
        player = GameObject.FindGameObjectWithTag("Player");

        if(player)
            playerPosition = player.transform.position;

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

    private void getPlayerPosition()
    {
        if (player)
            playerPosition = player.transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && IsHost)
        {
            NetworkObject m_SpawnedNetworkObject = this.GetComponent<NetworkObject>();
            m_SpawnedNetworkObject.Despawn();
        }
       
        if (!player)
        {
            Debug.Log("search player");
            player = GameObject.FindGameObjectWithTag("Player");

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
            getPlayerPosition();
            if (agent)
                agent.SetDestination(playerPosition);


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
        Debug.Log("TOUCHE BALLE");
        if (collision.transform.tag == "Bullet")
            currentHealth -= 30;
    }
}
