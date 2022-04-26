using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float AutoDestroyTime = 1f;

    public void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            DamagePlayer(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Enemy") == true)
        {
            DamageEnemy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            Explode();
        }
    }

    public void DamagePlayer(GameObject col)
    {

    }
    public void DamageEnemy(GameObject col)
    {

    }

    public void Explode()
    {
        Destroy(gameObject);
    }
}
