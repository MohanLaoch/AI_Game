using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FearState : State
{
    public AttackingState AttackState;
    public ChaseState chaseState;

    public GameObject ParentEnemy;
    public NavMeshAgent agent;

    private GameObject ClosestAlly;

    private bool NoAllies;
    private bool AllyCloseEnough;
    public float AllyDetectionRange = 20f;

    public override State RunCurrentState()
    {
        RunState();

        if(NoAllies)
        {
            AttackState.NumShotsRequired = (int)Random.Range(4f, 7f);
            AttackState.NoAllies = true;
            chaseState.NoAllies = true;
            return AttackState;
        }
        else if(AllyCloseEnough)
        {
            AttackState.NumShotsRequired = (int)Random.Range(4f, 7f);
            return AttackState;
        }
        else
        {
            return this;
        }
    }

    public void RunState()
    {
        if(Vector3.Distance(gameObject.transform.position, agent.destination) <= AllyDetectionRange/2)
        {
            AllyCloseEnough = true;
        }
        else if(ClosestAlly == null)
        {
            NoAllies = true;
        }
    }

    public void FindAlly()
    {

        GameObject[] Allies = GameObject.FindGameObjectsWithTag("Enemy");
        float ClosestAllyDistance = 9999;

        foreach (var Ally in Allies)
        {
            if (Ally != ParentEnemy)
            {
                if (Vector3.Distance(gameObject.transform.position, Ally.transform.position) <= ClosestAllyDistance)
                {
                    ClosestAlly = Ally;
                    ClosestAllyDistance = Vector3.Distance(gameObject.transform.position, Ally.transform.position);
                }
            }
        }

        if (ClosestAlly != null)
        { 
            agent.SetDestination(ClosestAlly.transform.position); 
        }
        else
        {
            NoAllies = true;
        }
    }
}
