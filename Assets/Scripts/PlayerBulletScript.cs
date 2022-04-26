using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float AutoDestroyTime = 1f;

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Enemy") == true)
        {
            DamageEnemy(col.gameObject);
        }
        else if(col.gameObject.CompareTag("Wall"))
        {
            Explode();
        }
    }

    public void DamageEnemy(GameObject col)
    {

        Explode();
    }

    public void Explode()
    {
        Destroy(gameObject);
    }
}
