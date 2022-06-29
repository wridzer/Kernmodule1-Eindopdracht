using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour//, IDamageble
{
    //float IDamageble.Health { get; set; }
    [SerializeField] private NavMeshAgent navMeshAgent;
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

    //[HideInInspector] public TerrainGen owner;
    
    private int currentWaypointIndex;

    private Vector3 playerLastPosition = Vector3.zero;
    private Vector3 keepPlayerPosition;

    private float walkWaitTime;
    private float walkTimeToRotate;
    private bool walkPlayerInRange  = true;
    private bool walkPlayerNear  = true;
    private bool walkIsPatrol  = true;
    private bool walkCaughtPlayer = true;
    private bool attackingPlayer  = true;
    private bool canAttack  = true;
    private float health;

    private bool _active;
    //private IDamageble _damagebleImplementation;
    

<<<<<<< Updated upstream:Assets/Scripts/AI/AIController.cs
    public bool Active
    {
        get => _active;
        set => _active = value;
    }

    public void OnEnableObject()
    {
        waypoint = SetRandomDestination(transform.position,10);
        navMeshAgent.SetDestination(waypoint);
        keepPlayerPosition = Vector3.zero;
=======
    public void OnDisableObject() {
        //waypoint = Vector3.zero;
        //Stop();
    }

    public void Init(TerrainGen owner) {
        this.owner = owner;
>>>>>>> Stashed changes:Assets/Scripts/AIController.cs
        walkIsPatrol = true;
        walkCaughtPlayer = false;
        walkPlayerInRange = false;
        walkCaughtPlayer = false;
        walkPlayerNear  = false;
        canAttack = false;
        walkWaitTime = startWaitTime;
        walkTimeToRotate = timeToRotate;
<<<<<<< Updated upstream:Assets/Scripts/AI/AIController.cs

        currentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoint);
=======
        
        StartCoroutine(AttachNavmesh());
>>>>>>> Stashed changes:Assets/Scripts/AIController.cs
        meshRenderer.GetComponent<MeshRenderer>();
    }

    public void OnDisableObject()
    {
        waypoint = Vector3.zero;
        Stop();
    }

<<<<<<< Updated upstream:Assets/Scripts/AI/AIController.cs
    public void Init()
    {
        Init();
        //player = GameManager.instance.player;
    }
    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        if (health <= 0)
        {
            //Destroy(gameObject);
=======
        var checkNav = SetRandomDestination(transform.position,5);
        if (checkNav == Vector3.zero)
        {
            owner.DespawnEnemy(this.gameObject);
>>>>>>> Stashed changes:Assets/Scripts/AIController.cs
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
<<<<<<< Updated upstream:Assets/Scripts/AI/AIController.cs
            navMeshAgent.SetDestination(keepPlayerPosition);
=======
            //navMeshAgent.SetDestination(player.transform.position);
>>>>>>> Stashed changes:Assets/Scripts/AIController.cs
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
        
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !canAttack)
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
                waypoint = SetRandomDestination(transform.position,10);
                navMeshAgent.SetDestination(waypoint);
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
            navMeshAgent.SetDestination(waypoint);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (walkWaitTime <= 0)
                {
                    waypoint = SetRandomDestination(transform.position,10);
                    navMeshAgent.SetDestination(waypoint);
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
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
    private void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
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
    navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) < 0.3)
        {
            if (walkWaitTime <= 0)
            {
                walkPlayerNear = false;
                Move(speedWalk);
<<<<<<< Updated upstream:Assets/Scripts/AI/AIController.cs
                SetRandomDestination(transform.position, 10);
                navMeshAgent.SetDestination(waypoint);
=======
                //waypoint = SetRandomDestination(transform.position, 10);
                //navMeshAgent.SetDestination(waypoint);
>>>>>>> Stashed changes:Assets/Scripts/AIController.cs
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
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= viewRadius)
        {
            Transform _player = player.transform;
            Vector3 dirToPlayer = (_player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleMask))
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
<<<<<<< Updated upstream:Assets/Scripts/AI/AIController.cs

        if (finalPosition == Vector3.zero)
        {
            //owner.DespawnEnemy(this);
            return Vector3.zero;
        }
        return finalPosition;
    }
}
=======
        return finalPosition;
    }

    IEnumerator AttachNavmesh()
    {
        yield return new WaitForSeconds(2);
        navMeshAgent.enabled = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speedWalk;
        waypoint = SetRandomDestination(transform.position, 10);
        navMeshAgent.SetDestination(waypoint);
        
    }
}
>>>>>>> Stashed changes:Assets/Scripts/AIController.cs
