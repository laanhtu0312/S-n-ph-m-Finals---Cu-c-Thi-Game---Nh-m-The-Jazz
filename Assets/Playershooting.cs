using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootings : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab của viên đạn
    public Transform firePoint;     // Vị trí mà đạn được bắn ra
    public float bulletSpeed = 10f; // Tốc độ bay của viên đạn
    public float fireRate = 0.5f;   // Thời gian giữa mỗi lần bắn
    public int bulletDamage = 1;    // Số lần đạn trúng để tiêu diệt quái vật
    public Slider healthSlider;     // Thanh máu của người chơi
    private float timeSinceLastFire = 0f;
    private bool canShoot = true;   // Kiểm soát có thể bắn hay không

    private void Update()
    {
        timeSinceLastFire += Time.deltaTime;

        // Kiểm tra nếu nhấn phím Tab và đủ thời gian để bắn đạn tiếp theo
        if (Input.GetKeyDown(KeyCode.C) && canShoot)
        {
            // Kiểm tra hướng di chuyển và bắn theo hướng đó
            Vector3 bulletDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) bulletDirection = Vector3.up;
            else if (Input.GetKey(KeyCode.A)) bulletDirection = Vector3.left;
            else if (Input.GetKey(KeyCode.S)) bulletDirection = Vector3.down;
            else if (Input.GetKey(KeyCode.D)) bulletDirection = Vector3.right;

            if (bulletDirection != Vector3.zero && timeSinceLastFire >= fireRate)
            {
                Shoot(bulletDirection);
                StartCoroutine(WaitToShootAgain());  // Đợi để bắn lần tiếp theo
            }
        }
    }

    // Hàm bắn đạn
    void Shoot(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        Destroy(bullet, 5f); // Đạn biến mất sau 5 giây
        timeSinceLastFire = 0f;
    }

    // Coroutine để đợi trước khi cho phép bắn tiếp
    IEnumerator WaitToShootAgain()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.5f); // Đợi 0.5 giây
        canShoot = true;
    }

    // Gọi hàm này khi bị quái vật chạm
    public void TakeDamage(int damage)
    {
        healthSlider.value -= damage; // Giảm máu của người chơi
        if (healthSlider.value <= 0)
        {
            Debug.Log("Player is dead.");
            // Thêm logic cho việc xử lý người chơi chết nếu cần thiết
        }
    }
}