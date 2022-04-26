using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float AutoDestroyTime = 1f;

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") == true)
        {
            DamagePlayer(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Wall"))
        {
            Explode();
        }
    }

    public void DamagePlayer(GameObject col)
    {

        Explode();
    }

    public void Explode()
    {
        Destroy(gameObject);
    }
}
