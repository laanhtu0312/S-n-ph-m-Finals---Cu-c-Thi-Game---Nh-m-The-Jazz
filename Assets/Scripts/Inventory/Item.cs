using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]

// Represent the data of a single item
public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
    public bool isSeed;
}

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // Đối tượng đạn
    public Transform firePoint;      // Vị trí bắn
    public int maxBulletsPerShot = 2; // Số viên đạn tối đa mỗi lần bắn
    public float bulletLifetime = 5f; // Thời gian tồn tại của viên đạn

    private bool canShoot = false;  // Kiểm tra xem người chơi đã chọn súng hay chưa

    // Hàm dùng để gọi khi người chơi chọn súng từ toolbar
    public void SelectGun()
    {
        canShoot = true;
    }

    void Update()
    {
        if (canShoot && Input.GetMouseButtonDown(0)) // Kiểm tra xem đã bắn
        {
            Shoot();
        }
    }

    void Shoot()
    {
        for (int i = 0; i < maxBulletsPerShot; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Bắn đạn theo hướng của chuột
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

            rb.velocity = direction * 10f;  // Tốc độ đạn

            // Đặt thời gian cho viên đạn biến mất
            Destroy(bullet, bulletLifetime);
        }
    }
}
