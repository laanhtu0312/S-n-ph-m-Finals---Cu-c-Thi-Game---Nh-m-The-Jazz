using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 35;
    public float lifetime = 3f;  // Thời gian tồn tại của đạn

    private void Start()
    {
        Destroy(gameObject, lifetime);  // Hủy đạn sau 3 giây
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Monster"))
    //    {
    //        collision.GetComponent<MonsterController>().TakeDamage(damage);
    //        Destroy(gameObject);  // Hủy đạn sau khi trúng quái vật
    //    }
    //}
}