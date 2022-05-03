using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoverState : State
{
    public AttackingState attackState;
    public ChaseState chaseState;
    public FearState fearState;

    public NavMeshAgent agent;

    private GameObject Player;

    [Range(1, 500)] public float walkRadius;
    private float walkCurrentCooldown;
    public float walkCooldown = 10f;

    public float AttackRange = 10f;

    private bool HasMoved;

    //Fear Variables
    public GameObject ParentEnemy;
    public float AllyDetectionRange = 20f;
    public bool IsAfraid;
    public bool IsAllyCloseEnough;
    public bool NoAllies;

    public override State RunCurrentState()
    {
        RunState();
        if (IsAfraid == true && NoAllies == false)
        {
            IsAllyCloseEnough = false;
            IsAfraid = false;
            fearState.ClosestAlly = null;
            fearState.FindAlly();
            return fearState;
        }
        else if (agent.remainingDistance <= agent.stoppingDistance && walkCurrentCooldown <= walkCooldown && HasMoved == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit, AttackRange))
            {
                if (hit.transform.tag == "Player")
                {
                    walkCurrentCooldown = walkCooldown;
                    HasMoved = false;
                    attackState.NumShotsRequired = (int)Random.Range(4f, 7f);
                    attackState.NumShotsTaken = 0;
                    return attackState;
                }
                else
                {
                    walkCurrentCooldown = walkCooldown;
                    HasMoved = false;
                    return chaseState;
                }
            }

            return chaseState;
        }
        else
        {
            return this;
        }
    }
    public void Start()
    {
        walkCurrentCooldown = walkCooldown;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if(NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void RunState()
    {
        if(HasMoved == false)
        {
            HasMoved = true;
            agent.SetDestination(RandomNavMeshLocation());
        }

        if(walkCurrentCooldown <= walkCooldown)
        {
            walkCurrentCooldown--;
        }

        IsAllyCloseEnough = false;

        GameObject[] Allies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var Ally in Allies)
        {
            if (Ally != ParentEnemy)
            {
                if (Vector3.Distance(gameObject.transform.position, Ally.transform.position) < AllyDetectionRange)
                {
                    IsAllyCloseEnough = true;
                }
            }
        }
        if (IsAllyCloseEnough != true)
        {
            IsAfraid = true;
        }
    }
}
