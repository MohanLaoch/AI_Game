using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public AttackingState attackState;
    public FearState fearState;

    public bool isInAttackRange;

    private GameObject Player;
    public GameObject ChaseIcon;

    public NavMeshAgent agent;
    public float AttackRange = 10f;

    //Fear Variables
    public GameObject ParentEnemy;
    public float AllyDetectionRange = 20f;
    public bool IsAfraid;
    public bool IsAllyCloseEnough;
    public bool NoAllies;

    public override State RunCurrentState()
    {
        RunState();

        if(IsAfraid == true && NoAllies == false)
        {
            IsAllyCloseEnough = false;
            IsAfraid = false;
            fearState.ClosestAlly = null;
            fearState.FindAlly();
            ChaseIcon.gameObject.SetActive(false);
            return fearState;
        }
        else if (isInAttackRange)
        {
            attackState.NumShotsRequired = (int)Random.Range(4f, 7f);
            attackState.NumShotsTaken = 0;
            IsAllyCloseEnough = false;
            IsAfraid = false;
            ChaseIcon.gameObject.SetActive(false);
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
        if (ChaseIcon.activeSelf == false)
        {
            ChaseIcon.gameObject.SetActive(true);
            Debug.Log("test");
        }
        agent.SetDestination(Player.transform.position);

        if (Vector3.Distance(transform.position, Player.transform.position) <= AttackRange)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit, AttackRange))
            {
                if (hit.transform.tag == "Player")
                {
                    isInAttackRange = true;
                }
                else
                {
                    isInAttackRange = false;
                }
            }
        }
        else
        {
            isInAttackRange = false;
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
