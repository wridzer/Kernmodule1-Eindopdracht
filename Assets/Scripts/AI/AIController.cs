using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float startWaitTime = 4;
    [SerializeField] private float timeToRotate = 2;
    [SerializeField] private float speedWalk = 6;
    [SerializeField] private float speedRun = 9;

    [SerializeField] private float viewRadius = 10;
    [SerializeField] private float viewAngle = 75;
    
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;
    
    [SerializeField]private Material agro;
    [SerializeField]private Material passive;
    [SerializeField]private MeshRenderer meshRenderer;
    
    [SerializeField] private Transform[] waypoints;
    private int m_CurrentWaypointIndex;

    private Vector3 playerLastPosition = Vector3.zero;
    private Vector3 m_PlayerPosition;

    private float walkWaitTime;
    private float walkTimeToRotate;
    private bool walkPlayerInRange;
    private bool walkPlayerNear;
    private bool walkIsPatrol;
    private bool walkCaughtPlayer;
    
    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        walkIsPatrol = true;
        walkCaughtPlayer = false;
        walkPlayerInRange = false;
        walkWaitTime = startWaitTime;
        walkTimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speedWalk;
        _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        meshRenderer.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        EnviromentView();
        if (!walkIsPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
    }

    private void Chasing()
    {
        meshRenderer.material = agro;
        walkPlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!walkCaughtPlayer)
        {
            Move(speedRun);
            _navMeshAgent.SetDestination(m_PlayerPosition);
        }

        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (walkWaitTime <= 0 && !walkCaughtPlayer && Vector3.Distance(transform.position,
                    GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                walkIsPatrol = true;
                walkPlayerNear = false;
                Move(speedWalk);
                walkTimeToRotate = timeToRotate;
                walkWaitTime = startWaitTime;
                _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position,
                        GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    AttackPlayer();
                    walkWaitTime -= Time.deltaTime;
                }
            }
        }
    }
    private void Patroling()
    {
        meshRenderer.material = passive;
        if (walkPlayerNear)
        {
            if (walkTimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                walkTimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            walkPlayerNear = false;
            playerLastPosition = Vector3.zero;
            _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (walkWaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    walkWaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    walkWaitTime -= Time.deltaTime;
                }
            }
        }
    }
    private void Stop()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
    private void Move(float speed)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    private void AttackPlayer()
    {
        walkCaughtPlayer = true;
    }

    private void LookingPlayer(Vector3 player)
    {
        _navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) < 0.3)
        {
            if (walkWaitTime <= 0)
            {
                walkPlayerNear = false;
                Move(speedWalk);
                _navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                walkWaitTime = startWaitTime;
                walkTimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                walkWaitTime -= Time.deltaTime;
            }
        }
    }

    private void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

            for (int i = 0; i < playerInRange.Length; i++)
            {
                Transform player = playerInRange[i].transform;
                Vector3 dirToPlayer = (player.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
                {
                    float dsToPlayer = Vector3.Distance(transform.position, player.position);
                    if (!Physics.Raycast(transform.position, dirToPlayer, dsToPlayer, obstacleMask))
                    {
                        walkPlayerInRange = true;
                        walkIsPatrol = false;
                    }
                    else
                    {
                        walkPlayerInRange = false;
                    }
                }

                if (Vector3.Distance(transform.position, player.position) > viewAngle)
                {
                    walkPlayerInRange = false;
                }
            if (walkPlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }


    }
}
