using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;
    public GameManager gameManager;
    public int MaxHP = 20;
    public int CurrentHP;
    public int MaxDamage;
    public int MinDamage;

    public bool hasKey;

    public void Start()
    {
        CurrentHP = MaxHP;
        healthBar.SetMaxHealth(MaxHP);
    }

    public void Update()
    {
        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
            gameManager.ToMenu();
        }

        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        healthBar.SetHealth(CurrentHP);
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
    }
}
