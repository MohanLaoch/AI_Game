using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;
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
        }

        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        healthBar.SetHealth(CurrentHP);
    }
}
