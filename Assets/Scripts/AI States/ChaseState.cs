using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public AttackingState attackState;
    public bool isInAttackRange;

    private GameObject Player;

    public NavMeshAgent agent;
    public float AttackRange = 10f;

    public override State RunCurrentState()
    {
        RunState();
        if (isInAttackRange)
        {
            attackState.NumShotsRequired = (int)Random.Range(4f, 7f);
            return attackState;
        }
        else
        {
            return this;
        }
    }
    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void RunState()
    {
        agent.SetDestination(Player.transform.position);

        if (Vector3.Distance(transform.position, Player.transform.position) >= AttackRange)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit, AttackRange))
            {
                if (hit.transform.tag == "Player")
                {
                    isInAttackRange = true;
                }
            }
        }
    }
}
