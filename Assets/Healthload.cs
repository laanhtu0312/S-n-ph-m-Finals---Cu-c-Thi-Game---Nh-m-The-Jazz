using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthCheck : MonoBehaviour
{
    public Slider healthSlider; // Kéo thả thanh máu vào đây trong Inspector
    public string defeatScreenName; // Tên của màn hình thua cần load

    void Update()
    {
        // Kiểm tra nếu máu giảm xuống còn 1
        if (healthSlider.value <= 1)
        {
            LoadDefeatScreen();
        }
    }

    void LoadDefeatScreen()
    {
        // Load màn hình thua
        SceneManager.LoadScene(defeatScreenName);
    }
}