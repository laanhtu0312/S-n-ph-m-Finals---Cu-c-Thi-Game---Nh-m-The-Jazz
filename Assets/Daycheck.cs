using UnityEngine;
using UnityEngine.SceneManagement;

public class DayCheck : MonoBehaviour
{
    public int day; // Số ngày mà bạn đã có code hiển thị
    public string victoryScreenName; // Tên của màn hình chiến thắng cần load

    void Update()
    {
        // Kiểm tra nếu day bằng 30
        if (day == 30)
        {
            LoadVictoryScreen();
        }
    }

    void LoadVictoryScreen()
    {
        // Load màn hình chiến thắng
        SceneManager.LoadScene(victoryScreenName);
    }
}