using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicZombiController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Vector3 playerPosition;
    public NavMeshSurface[] surfaces;

    public NavMeshAgent agent;
    private Animator animator;
    private bool isAnimationEnabled = false;
    private float startAnimationRandom = 0;

    void Start()
    {
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
}
