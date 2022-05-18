using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingState : State
{
    public CoverState coverState;
    public FearState fearState;

    public EnemyStats stats;

    [HideInInspector] public int NumShotsRequired;

    private GameObject Player;

    public float BulletCooldown = 60f;

    public GameObject BulletPrefab;
    public GameObject AttackingIcon;
    public float BulletForce = 10f;
    public Transform firePoint;
    public Transform ParentTransform;

    public NavMeshAgent agent;

    //Fear Variables
    public GameObject ParentEnemy;
    public float AllyDetectionRange = 20f;
    public bool IsAfraid;
    public bool IsAllyCloseEnough;
    public bool NoAllies;

    public int NumShotsTaken;
    private float BulletCurrentCooldown = 0f;

    [SerializeField] private AudioSource ShootSFX;

    public override State RunCurrentState()
    {
        RunState();

        if (IsAfraid == true && NoAllies == false)
        {
            NumShotsTaken = 0;
            BulletCurrentCooldown = 0f;
            IsAllyCloseEnough = false;
            IsAfraid = false;
            fearState.ClosestAlly = null;
            AttackingIcon.gameObject.SetActive(false);
            return fearState;
        }
        else if (NumShotsTaken == NumShotsRequired)
        {
            NumShotsTaken = 0;
            BulletCurrentCooldown = 0f;
            IsAllyCloseEnough = false;
            IsAfraid = false;
            AttackingIcon.gameObject.SetActive(false);
            return coverState;
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
        agent.SetDestination(transform.position);
        CharRotation();

        if(AttackingIcon.activeSelf == false)
        {
            AttackingIcon.gameObject.SetActive(true);
            Debug.Log("test");
        }

         /*if(AttackingIcon.SetActive(false)
           {
         attackingicon.setactive(true)
           }
         

         */
        if(BulletCurrentCooldown <= 0)
        {
            Shoot();
        }
        else if(BulletCurrentCooldown > 0)
        {
            BulletCurrentCooldown--;
        }

        IsAllyCloseEnough = false;

        GameObject[] Allies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var Ally in Allies)
        {
            if (Ally != ParentEnemy)
            {
                if (Vector3.Distance(gameObject.transform.position, Ally.transform.position) < AllyDetectionRange)
                {
                    IsAllyCloseEnough = true;
                }
            }
        }
        if(IsAllyCloseEnough != true)
        {
            IsAfraid = true;
        }
    }
    public void Shoot()
    {
        ShootSFX.Play();

        Vector3 FireDir = Player.transform.position;
        BulletCurrentCooldown = BulletCooldown;
        GameObject Bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        FireDir.y = transform.position.y;
        rb.AddForce((FireDir - firePoint.position).normalized * BulletForce, ForceMode.Impulse);

        Bullet.GetComponent<EnemyBulletScript>().damage = (int)Random.Range(stats.MinDamage, stats.MaxDamage);
        NumShotsTaken++;
    }
    private void CharRotation()
    {
        Vector3 pointToLook = Player.transform.position;
        ParentTransform.LookAt(pointToLook);
    }
}
