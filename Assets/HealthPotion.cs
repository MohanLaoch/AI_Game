using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerStats.CurrentHP < playerStats.MaxHP)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
                playerStats.CurrentHP += 20;
                Debug.Log("Healed");

            }
        }
    }
}
