using System;
using System.Collections;
using UnityEngine;

public class MonsterControllerss : MonoBehaviour
{
    public int health = 3;

    public event Action<GameObject> OnMonsterDeath;  // Sự kiện báo khi quái vật chết

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnMonsterDeath?.Invoke(gameObject);  // Kích hoạt sự kiện khi quái vật chết
        Destroy(gameObject);  // Hủy quái vật
    }
}