using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 15; // time in seconds to wait before seeking a new patrol destination
    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area

    int index; // the current waypoint index in the waypoints array
    float speed, agentSpeed;
    Transform player;

    Animator animator;
    NavMeshAgent agent;

    [Space, Header("Use for Debugging Aggro Radius")]
    public Boolean showAggro = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agentSpeed = agent.speed; }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f); // executes Tick() every 0.5 sec

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        }
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    // updates waypoint for NPC to navigate to
    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    /**
     * evaluates status of the NPC periodically
     */
    void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if (player != null
            && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }
    }

    // built-in method executes when the object is rendered in Scene View. So just for debug
    private void OnDrawGizmos()
    {
        if (showAggro)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
}
