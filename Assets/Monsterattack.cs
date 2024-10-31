using UnityEngine;

public class Monster : MonoBehaviour
{
    public int damageAmount = 30; // Sát thương mà quái vật gây ra mỗi lần chạm

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController playerHealth = collision.gameObject.GetComponent<HealthController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Gây sát thương cho người chơi
            }
        }
    }
}