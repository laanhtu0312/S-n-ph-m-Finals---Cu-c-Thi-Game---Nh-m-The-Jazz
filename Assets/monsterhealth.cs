using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHits = 3; // Số lần trúng đạn trước khi quái vật bị tiêu diệt
    private int currentHits = 0; // Đếm số lần trúng đạn hiện tại

    public void TakeDamage()
    {
        currentHits++;

        if (currentHits >= maxHits)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Tiêu diệt quái vật
    }
}