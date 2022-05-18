using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private PlayerStats stats;
    private Camera cam;

    public float speed = 6f;
    public float BulletCooldown = 60f;
    private float BulletCurrentCooldown = 0f;

    public GameObject BulletPrefab;
    public float BulletForce = 10f;
    public Transform firePoint;

    public float gravity = 5f;
    private Vector3 velocity = new Vector3(0, 0, 0);

    private Vector3 pointToLook;
    [SerializeField] private AudioSource ShootSFX;

    private void Start()
    {
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        CharRotation();

        BulletCurrentCooldown--;
        if(Input.GetMouseButton(0))
        {
            if (BulletCurrentCooldown <= 0)
            {
                Shoot();
            }
        }

        velocity.y -= gravity * Time.deltaTime;

        if(velocity.y >= 10f)
        {
            velocity.y = 10f;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void CharRotation()
    {
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            pointToLook = cameraRay.GetPoint(rayLength);
            pointToLook.y = transform.position.y;
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    public void Shoot()
    {
        ShootSFX.Play();

        Vector3 FireDir = pointToLook;
        BulletCurrentCooldown = BulletCooldown;
        GameObject Bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        FireDir.y = transform.position.y;
        rb.AddForce((FireDir - firePoint.position).normalized * BulletForce, ForceMode.Impulse);

        Bullet.GetComponent<PlayerBulletScript>().damage = (int)Random.Range(stats.MinDamage, stats.MaxDamage);
    }
}
