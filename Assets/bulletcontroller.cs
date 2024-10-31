using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm có layer là "Enemy"
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Gọi phương thức TakeDamage của quái vật để tăng số lần trúng đạn
            MonsterHealth monsterHealth = other.GetComponent<MonsterHealth>();
            if (monsterHealth != null)
            {
                monsterHealth.TakeDamage();
            }

            // Hủy viên đạn sau khi va chạm với quái vật
            Destroy(gameObject);
        }
    }
}