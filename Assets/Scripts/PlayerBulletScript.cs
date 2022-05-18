using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public int damage;
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
        col.GetComponent<EnemyStats>().TakeDamage(damage);
        Explode();
    }

    public void Explode()
    {
        Destroy(gameObject);
    }
}
