using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour//, IDamageble
{
    //float IDamageble.Health { get; set; }
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float startWaitTime = 4;
    [SerializeField] private float timeToRotate = 2;
    [SerializeField] private float speedWalk = 6;
    [SerializeField] private float speedRun = 9;
    [SerializeField] private int damage = 10;

    [SerializeField] private float viewRadius = 10;
    [SerializeField] private float viewAngle = 75;
    
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;
    
    [SerializeField]private Material agro;
    [SerializeField]private Material passive;
    [SerializeField]private MeshRenderer meshRenderer;
    
    [SerializeField]private GameObject player;
    [SerializeField]private Vector3 waypoint;

    //[SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex;

    private Vector3 playerLastPosition = Vector3.zero;
    private Vector3 keepPlayerPosition;
    private Vector3 moveto;

    
    
    private float walkWaitTime;
    private float walkTimeToRotate;
    private bool walkPlayerInRange;
    private bool walkPlayerNear;
    private bool walkIsPatrol;
    private bool walkCaughtPlayer;
    private bool attackingPlayer;
    private bool canAttack;
    private float health;
    //private IDamageble _damagebleImplementation;

    void Start()
    {
        keepPlayerPosition = Vector3.zero;
        walkIsPatrol = true;
        walkCaughtPlayer = false;
        walkPlayerInRange = false;
        walkWaitTime = startWaitTime;
        walkTimeToRotate = timeToRotate;

        currentWaypointIndex = 0;
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speedWalk;
        SetRandomDestination(transform.position, 10);
        _navMeshAgent.SetDestination(waypoint);
        meshRenderer.GetComponent<MeshRenderer>();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if (health <= 0)
        {
            //Destroy(gameObject);
        }
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
            _navMeshAgent.SetDestination(keepPlayerPosition);
        }
        
        if (Vector3.Distance(transform.position, player.transform.position) <= 4.0f)
        {
            walkCaughtPlayer = true;
            canAttack = true;
            AttackPlayer();
        }
        else
        {
            canAttack = false;
            walkCaughtPlayer = false;
        }
        
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && !canAttack)
        {
            attackingPlayer = false;
            if (walkWaitTime <= 0 && !walkCaughtPlayer && Vector3.Distance(transform.position,
                    player.transform.position) >= 6f)
            {
                walkIsPatrol = true;
                walkPlayerNear = false;
                Move(speedWalk);
                walkTimeToRotate = timeToRotate;
                walkWaitTime = startWaitTime;
                _navMeshAgent.SetDestination(waypoint);
            }
            else
            {
                if (Vector3.Distance(transform.position,
                        player.transform.position) >= 2.5f)
                {
                    Stop();
                    walkCaughtPlayer = false;
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
            _navMeshAgent.SetDestination(waypoint);
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (walkWaitTime <= 0)
                {
                    SetRandomDestination(transform.position, 10);
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
    private void Move(float speed)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    private void AttackPlayer()
    {
        Stop();
        attackingPlayer = true;
        //walkCaughtPlayer = true;
        //player.GetComponent<IDamageble>()?.TakeDamage(damage);
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
                SetRandomDestination(transform.position, 10);
                _navMeshAgent.SetDestination(waypoint);
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
                Transform _player = playerInRange[i].transform;
                Vector3 dirToPlayer = (_player.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
                {
                        float dsToPlayer = Vector3.Distance(transform.position, _player.position);
                        if (!Physics.Raycast(transform.position, dirToPlayer, dsToPlayer, obstacleMask))
                        {
                            walkPlayerInRange = true;
                            walkIsPatrol = false;
                        }
                        else
                        {
                            walkPlayerInRange = false;
                            walkIsPatrol = true;
                        }
                    }

                    if (Vector3.Distance(transform.position, _player.position) > viewAngle)
                    {
                        walkPlayerInRange = false;
                    }
                    if (walkPlayerInRange)
                    {
                        keepPlayerPosition = player.transform.position;
                    }
            }
    }

    private Vector3 SetRandomDestination(Vector3 pos, float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += pos;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1)) {
            finalPosition = hit.position;
        }
        return finalPosition;
        /*float randomX = Random.Range(patrolPointRange.min.x, patrolPointRange.max.x);
        float randomZ = Random.Range(patrolPointRange.min.z, patrolPointRange.max.z);
        waypoint = new Vector3(randomX, this.transform.position.y, randomZ);*/
        _navMeshAgent.SetDestination(waypoint);
    }
}
