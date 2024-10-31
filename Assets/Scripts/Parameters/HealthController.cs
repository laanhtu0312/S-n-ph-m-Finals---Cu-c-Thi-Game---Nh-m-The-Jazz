using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HealthController : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth;
    public static float currentHealth;
    public Image fillSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        fillSlider = healthSlider.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill");
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        healthSlider.value = currentHealth;

        // Thay đổi màu sắc của thanh máu dựa trên lượng máu còn lại
        if (currentHealth >= 50)
            fillSlider.color = Color.green;
        else if (currentHealth < 50 && currentHealth >= 30)
            fillSlider.color = new Color(1f, 0.5f, 0f); // Màu cam
        else if (currentHealth < 30)
            fillSlider.color = Color.red;
    }

    // Hàm này sẽ được gọi khi người chơi nhận sát thương
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            // Xử lý logic người chơi chết, ví dụ: hiển thị "Game Over"
        }
    }

    // Xử lý khi máu người chơi giảm xuống 0
    private void Die()
    {
        Debug.Log("Player is dead.");
        // Thêm logic xử lý khi người chơi chết (ví dụ hiển thị màn hình Game Over)
    }
}