using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Guncontrol : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab của viên đạn
    public Transform firePoint;      // Vị trí bắn đạn ra
    public float bulletSpeed = 10f;  // Tốc độ bay của viên đạn
    public float fireRate = 0.5f;    // Thời gian hồi giữa mỗi lần bắn
    public int bulletDamage = 1;     // Sát thương của viên đạn
    public Slider healthSlider;      // Thanh máu của người chơi

    private float timeSinceLastFire = 0f;
    private bool canShoot = true;    // Kiểm soát bắn đạn
    private Vector3 currentDirection = Vector3.right; // Hướng hiện tại của người chơi (mặc định hướng phải)

    private void Update()
    {
        timeSinceLastFire += Time.deltaTime;

        // Cập nhật hướng của người chơi nếu có di chuyển
        UpdatePlayerDirection();

        // Bắn khi nhấn phím Tab và có thể bắn được
        if (Input.GetKeyDown(KeyCode.Tab) && canShoot)
        {
            Shoot(currentDirection);  // Bắn theo hướng đang đối diện
            StartCoroutine(WaitToShootAgain());
        }
    }

    // Hàm cập nhật hướng người chơi dựa trên phím WASD
    void UpdatePlayerDirection()
    {
        if (Input.GetKey(KeyCode.W)) currentDirection = Vector3.up;
        else if (Input.GetKey(KeyCode.A)) currentDirection = Vector3.left;
        else if (Input.GetKey(KeyCode.S)) currentDirection = Vector3.down;
        else if (Input.GetKey(KeyCode.D)) currentDirection = Vector3.right;
    }

    // Hàm bắn đạn theo hướng được chỉ định
    void Shoot(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;  // Đặt vận tốc cho viên đạn
        }
        else
        {
            Debug.LogError("Bullet prefab is missing a Rigidbody2D component.");
        }

        Destroy(bullet, 2f);  // Hủy viên đạn sau 2 giây
    }

    // Coroutine đợi trước khi cho phép bắn lần tiếp theo
    IEnumerator WaitToShootAgain()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate); // Thời gian hồi bắn
        canShoot = true;
    }

    // Gọi hàm này khi bị quái vật chạm
    public void TakeDamage(int damage)
    {
        healthSlider.value -= damage;  // Giảm máu của người chơi

        if (healthSlider.value <= 0)
        {
            Debug.Log("Player is dead.");
            // Thêm logic xử lý khi người chơi chết
        }
    }
}