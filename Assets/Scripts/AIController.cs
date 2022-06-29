using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour {
    ////float IDamageble.Health { get; set; }
    //[SerializeField] private NavMeshAgent navMeshAgent;
    //[SerializeField] private float startWaitTime = 4;
    //[SerializeField] private float timeToRotate = 2;
    //[SerializeField] private float speedWalk = 6;
    //[SerializeField] private float speedRun = 9;
    //[SerializeField] private int damage = 10;

    //[SerializeField] private float viewRadius = 10;
    //[SerializeField] private float viewAngle = 75;

    //[SerializeField] private LayerMask playerMask;
    //[SerializeField] private LayerMask obstacleMask;

    //[SerializeField] private Material agro;
    //[SerializeField] private Material passive;
    //[SerializeField] private MeshRenderer meshRenderer;

    //[SerializeField] private Vector3 waypoint;

    //[HideInInspector] public GameObject player;
    //[HideInInspector] public TerrainGen owner;

    //private Vector3 playerLastPosition = Vector3.zero;
    //private Vector3 keepPlayerPosition;

    //private float walkWaitTime;
    //private float walkTimeToRotate;
    //private bool walkPlayerInRange = false;
    //private bool walkPlayerNear = false;
    //private bool walkIsPatrol = false;
    //private bool walkCaughtPlayer = false;
    //private bool attackingPlayer = false;
    //private bool canAttack = false;
    //private float health;
    ////private IDamageble _damagebleImplementation;

    //public void OnDisableObject() {
    //    waypoint = Vector3.zero;
    //    Stop();
    //}

    //public void Init(TerrainGen owner) {
    //    this.owner = owner;
    //    waypoint = SetRandomDestination(transform.position, 10);
    //    navMeshAgent.SetDestination(waypoint);
    //    keepPlayerPosition = Vector3.zero;
    //    walkIsPatrol = true;
    //    walkWaitTime = startWaitTime;
    //    walkTimeToRotate = timeToRotate;

    //    navMeshAgent = GetComponent<NavMeshAgent>();

    //    navMeshAgent.isStopped = false;
    //    navMeshAgent.speed = speedWalk;
    //    meshRenderer.GetComponent<MeshRenderer>();
    //}

    //void Update() {
    //    if (health <= 0) {
    //        //owner.DespawnEnemy(this.gameObject);
    //    }

    //    EnviromentView();

    //    if (!walkIsPatrol) {
    //        Chasing();
    //    }
    //    else {
    //        Patroling();
    //    }
    //}

    //private void Chasing() {
    //    meshRenderer.material = agro;
    //    walkPlayerNear = false;
    //    playerLastPosition = player.transform.position;

    //    if (!walkCaughtPlayer) {
    //        Move(speedRun);
    //        navMeshAgent.SetDestination(player.transform.position);
    //    }

    //    if (Vector3.Distance(transform.position, player.transform.position) <= 4.0f) {
    //        walkCaughtPlayer = true;
    //        canAttack = true;
    //        AttackPlayer();
    //    }
    //    else {
    //        canAttack = false;
    //        walkCaughtPlayer = false;
    //    }

    //    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !canAttack) {
    //        attackingPlayer = false;
    //        if (walkWaitTime <= 0 && !walkCaughtPlayer && Vector3.Distance(transform.position,
    //                player.transform.position) >= 6f) {
    //            walkIsPatrol = true;
    //            walkPlayerNear = false;
    //            Move(speedWalk);
    //            walkTimeToRotate = timeToRotate;
    //            walkWaitTime = startWaitTime;
    //            waypoint = SetRandomDestination(transform.position, 10);
    //            navMeshAgent.SetDestination(waypoint);
    //        }
    //        else {
    //            if (Vector3.Distance(transform.position,
    //                    player.transform.position) >= 2.5f) {
    //                Stop();
    //                walkCaughtPlayer = false;
    //                walkWaitTime -= Time.deltaTime;
    //            }
    //        }
    //    }
    //}

    //private void Patroling() {
    //    meshRenderer.material = passive;
    //    if (walkPlayerNear) {
    //        if (walkTimeToRotate <= 0) {
    //            Move(speedWalk);
    //            LookingPlayer(playerLastPosition);
    //        }
    //        else {
    //            Stop();
    //            walkTimeToRotate -= Time.deltaTime;
    //        }
    //    }
    //    else {
    //        walkPlayerNear = false;
    //        playerLastPosition = player.transform.position;
    //        waypoint = SetRandomDestination(transform.position, 10);
    //        navMeshAgent.SetDestination(waypoint);
    //        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
    //            if (walkWaitTime <= 0) {
    //                waypoint = SetRandomDestination(transform.position, 10);
    //                navMeshAgent.SetDestination(waypoint);
    //                Move(speedWalk);
    //                walkWaitTime = startWaitTime;
    //            }
    //            else {
    //                Stop();
    //                walkWaitTime -= Time.deltaTime;
    //            }
    //        }
    //    }
    //}

    //private void Stop() {
    //    navMeshAgent.isStopped = true;
    //    navMeshAgent.speed = 0;
    //}

    //private void Move(float speed) {
    //    navMeshAgent.isStopped = false;
    //    navMeshAgent.speed = speed;
    //}

    //private void AttackPlayer() {
    //    Stop();
    //    attackingPlayer = true;
    //    //walkCaughtPlayer = true;
    //    //player.GetComponent<IDamageble>()?.TakeDamage(damage);
    //}

    //private void LookingPlayer(Vector3 player) {
    //    navMeshAgent.SetDestination(player);
    //    if (Vector3.Distance(transform.position, player) < 0.3) {
    //        if (walkWaitTime <= 0) {
    //            walkPlayerNear = false;
    //            Move(speedWalk);
    //            waypoint = SetRandomDestination(transform.position, 10);
    //            navMeshAgent.SetDestination(waypoint);
    //            walkWaitTime = startWaitTime;
    //            walkTimeToRotate = timeToRotate;
    //        }
    //        else {
    //            Stop();
    //            walkWaitTime -= Time.deltaTime;
    //        }
    //    }
    //}

    //private void EnviromentView() {
    //    Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
    //    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

    //    if (distanceToPlayer <= viewRadius) {
    //        Transform _player = player.transform;
    //        Vector3 dirToPlayer = (_player.position - transform.position).normalized;

    //        if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2) {
    //            if (!Physics.Raycast(transform.position, dirToPlayer, out var hit)) {
    //                if (hit.collider.CompareTag("Player")) {
    //                    walkPlayerInRange = true;
    //                    walkIsPatrol = false;
    //                }
    //            }
    //            else {
    //                walkPlayerInRange = false;
    //                walkIsPatrol = true;
    //            }
    //        }
    //    }
    //    else {
    //        walkPlayerInRange = false;
    //    }

    //    if (walkPlayerInRange) {
    //        keepPlayerPosition = player.transform.position;
    //    }
    //}

    //public Vector3 SetRandomDestination(Vector3 pos, float radius) {
    //    Vector3 randomDirection = Random.insideUnitSphere * radius;
    //    randomDirection += pos;
    //    Vector3 finalPosition = Vector3.zero;

    //    if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1)) {
    //        finalPosition = hit.position;
    //    }

    //    if (finalPosition == Vector3.zero) {
    //        owner.DespawnEnemy(this.gameObject);
    //        return Vector3.zero;
    //    }
    //    return finalPosition;
    //}
}