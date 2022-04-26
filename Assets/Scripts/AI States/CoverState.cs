using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoverState : State
{
    public AttackingState attackState;
    public ChaseState chaseState;
    public NavMeshAgent agent;

    private GameObject Player;

    [Range(1, 500)] public float walkRadius;
    private float walkCurrentCooldown;
    public float walkCooldown = 10f;

    public float AttackRange = 10f;

    public override State RunCurrentState()
    {
        RunState();

        if(agent.remainingDistance <= agent.stoppingDistance && walkCurrentCooldown <= walkCooldown)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit, AttackRange))
            {
                if (hit.transform.tag == "Player")
                {
                    walkCurrentCooldown = walkCooldown;
                    return attackState;
                }
                else
                {
                    walkCurrentCooldown = walkCooldown;
                    return chaseState;
                }
            }
            return this;
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
        if(walkCurrentCooldown <= walkCooldown)
        {
            walkCurrentCooldown--;
        }
    }
}
