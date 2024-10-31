using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    const float SecondsInDay = 900f; // 15 phút thực tế = 900 giây (10 phút ngày + 5 phút đêm)
    public float time;  // Thời gian trong ngày
    public int day;  // Số ngày sinh tồn

    [SerializeField] Text TimeDisplay;  // Hiển thị thời gian
    [SerializeField] Text DayDisplay;  // Hiển thị số ngày sinh tồn
    [SerializeField] float TimeScale;  // Tốc độ thời gian
    [SerializeField] float LightTransition = 0.0001f;  // Mức chuyển đổi ánh sáng

    public int hungerUpdaterCounter;
    public int healthUpdaterCounter;
    public int temperatureUpdateCounter;

    public static event Action OnNightStart;  // Sự kiện cho ban đêm

    private void Start()
    {
        day = 1;  // Ngày bắt đầu từ 1
        time = 0f;  // Khởi tạo thời gian ban đầu
        hungerUpdaterCounter = 0;
        healthUpdaterCounter = 0;
        temperatureUpdateCounter = 0;

        TemperatureController.currentTemperature = 100;
        UpdateDayDisplay();  // Cập nhật hiển thị ngày đầu tiên
    }

    private float getHours
    {
        get { return (time / 900f) * 24f; }  // Tính giờ dựa trên chu kỳ ngày/đêm
    }

    public float GetTime
    {
        get { return time; }
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        // Cập nhật trạng thái đói và nhiệt độ
        hungerUpdaterCounter++;
        temperatureUpdateCounter++;

        if (hungerUpdaterCounter == 250)
        {
            HungerController.currentHunger -= 1;
            hungerUpdaterCounter = 0;
        }

        if (HungerController.currentHunger < 10 || TemperatureController.currentTemperature < 10)
        {
            healthUpdaterCounter++;
            if (healthUpdaterCounter == 100)
            {
                HealthController.currentHealth -= 1;
                healthUpdaterCounter = 0;
            }
        }

        // Tăng thời gian trong ngày
        time += Time.deltaTime * TimeScale;
        int hours = (int)getHours;
        TimeDisplay.text = hours.ToString("00") + ":00";

        // Điều chỉnh độ sáng dựa trên thời gian
        var light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        if (time >= 0 && time < 600f)  // Ban ngày (0 - 600 giây)
        {
            light.intensity = Mathf.Min(1f, light.intensity + LightTransition);  // Tăng độ sáng
            TemperatureController.currentTemperature = 100;
        }
        else if (time >= 600f && time < 900f)  // Ban đêm (600 - 900 giây)
        {
            light.intensity = Mathf.Max(0.3f, light.intensity - LightTransition);  // Giảm độ sáng
            if (temperatureUpdateCounter > 50)
            {
                TemperatureController.currentTemperature -= 1;
                temperatureUpdateCounter = 0;
            }

            OnNightStart?.Invoke();  // Gọi sự kiện spawn quái vật
        }

        // Khi kết thúc một ngày
        if (time >= SecondsInDay)
        {
            time = 0f;
            day++;
            MoneyController.money += 200;  // Cộng tiền mỗi ngày
            UpdateDayDisplay();  // Cập nhật số ngày sinh tồn

            if (day == 30)  // Chiến thắng sau 30 ngày
            {
                Application.LoadLevel("Victory");  // Chuyển sang scene chiến thắng
            }
        }

        // Kiểm tra điều kiện thua
        if (HealthController.currentHealth < 1)
        {
            Application.LoadLevel("GameOver");  // Chuyển sang scene Game Over
        }
    }

    // Cập nhật hiển thị số ngày sinh tồn
    private void UpdateDayDisplay()
    {
        DayDisplay.text = "Day: " + day.ToString();
    }
}