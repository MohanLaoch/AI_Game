using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FearState : State
{
    public AttackingState attackState;
    public ChaseState chaseState;
    public CoverState coverState;

    public GameObject ParentEnemy;
    public NavMeshAgent agent;

    public GameObject ClosestAlly;

    public bool NoAllies;
    public bool AllyCloseEnough;
    public float AllyDetectionRange = 20f;

    public override State RunCurrentState()
    {
        RunState();

        if(NoAllies)
        {
            attackState.NumShotsRequired = (int)Random.Range(4f, 7f);
            attackState.NumShotsTaken = 0;
            attackState.NoAllies = true;
            chaseState.NoAllies = true;
            coverState.NoAllies = true;
            return attackState;
        }
        else if(AllyCloseEnough)
        {
            attackState.NumShotsRequired = (int)Random.Range(4f, 7f);
            attackState.NumShotsTaken = 0;
            return attackState;
        }
        else
        {
            return this;
        }
    }

    public void RunState()
    {
        if (ClosestAlly == null && NoAllies == false)
        {
            FindAlly();
        }
        else if (NoAllies == false && Vector3.Distance(gameObject.transform.position, ClosestAlly.transform.position) <= AllyDetectionRange / 2)
        {
            AllyCloseEnough = true;
        }
        else
        {
            AllyCloseEnough = false;
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

        if(ClosestAlly == null)
        {
            NoAllies = true;
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
