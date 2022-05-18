using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public PlayerStats playerStats;
    public HealthBar healthBar;
    public int HealAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        HealAmount = 20;
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
                playerStats.CurrentHP = playerStats.CurrentHP + HealAmount;
                healthBar.SetHealth(playerStats.CurrentHP);
                FindObjectOfType<AudioManager>().Play("PotionDrink");

            }
        }
    }
}
