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

    public GameObject FearIcon;

    public bool NoAllies;
    public bool AllyCloseEnough;
    public float AllyDetectionRange = 20f;

    public float AttackRange = 10f;
    private GameObject Player;

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
            FearIcon.gameObject.SetActive(false);
            return attackState;
        }
        else if(AllyCloseEnough)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                
                RaycastHit hit;

                if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit, AttackRange))
                {
                    if (hit.transform.tag == "Player")
                    {
                        attackState.NumShotsRequired = (int)Random.Range(4f, 7f);
                        attackState.NumShotsTaken = 0;
                        FearIcon.gameObject.SetActive(false); 
                        if (ClosestAlly.GetComponent<StateManager>().currentState = ClosestAlly.GetComponent<StateManager>().DefaultState)
                        {
                            ClosestAlly.GetComponent<StateManager>().DefaultState.canSeeThePlayer = true;
                        }
                        return attackState;
                    }
                    else
                    {
                        FearIcon.gameObject.SetActive(false);
                        chaseState.isInAttackRange = false;
                        if (ClosestAlly.GetComponent<StateManager>().currentState = ClosestAlly.GetComponent<StateManager>().DefaultState)
                        {
                            ClosestAlly.GetComponent<StateManager>().DefaultState.canSeeThePlayer = true;
                        }
                        return chaseState;
                    }
                }
                FearIcon.gameObject.SetActive(false);
                chaseState.isInAttackRange = false;
                if (ClosestAlly.GetComponent<StateManager>().currentState = ClosestAlly.GetComponent<StateManager>().DefaultState)
                {
                    ClosestAlly.GetComponent<StateManager>().DefaultState.canSeeThePlayer = true;
                }
                return chaseState;
            }
            FearIcon.gameObject.SetActive(false);
            chaseState.isInAttackRange = false;
            if (ClosestAlly.GetComponent<StateManager>().currentState = ClosestAlly.GetComponent<StateManager>().DefaultState)
            {
                ClosestAlly.GetComponent<StateManager>().DefaultState.canSeeThePlayer = true;
            }
            return chaseState;
        }
        else
        {
            return this;
        }
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RunState()
    {
        if (FearIcon.activeSelf == false)
        {
            FearIcon.gameObject.SetActive(true);
            Debug.Log("test");
        }
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
