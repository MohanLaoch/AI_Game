using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingState : State
{
    public CoverState coverState;
    public int NumShotsTaken;
    [HideInInspector] public int NumShotsRequired;

    private GameObject Player;

    public float BulletCooldown = 60f;
    private float BulletCurrentCooldown = 0f;

    public GameObject BulletPrefab;
    public float BulletForce = 10f;
    public Transform firePoint;
    public Transform ParentTransform;

    public NavMeshAgent agent;

    public override State RunCurrentState()
    {
        RunState();

        if(NumShotsTaken == NumShotsRequired)
        {
            NumShotsTaken = 0;
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
        if(BulletCurrentCooldown <= 0)
        {
            Shoot();
        }
        else if(BulletCurrentCooldown > 0)
        {
            BulletCurrentCooldown--;
        }
    }
    public void Shoot()
    {
        Vector3 FireDir = Player.transform.position;
        BulletCurrentCooldown = BulletCooldown;
        GameObject Bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        FireDir.y = transform.position.y;
        rb.AddForce((FireDir - firePoint.position).normalized * BulletForce, ForceMode.Impulse);
        NumShotsTaken++;
    }
    private void CharRotation()
    {
        Vector3 pointToLook = Player.transform.position;
        ParentTransform.LookAt(pointToLook);
    }
}
