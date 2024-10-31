using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HungerController : MonoBehaviour
{
    public Slider hungerSlider;
    public float maxHunger = 100f;
    public static float currentHunger;
    public Image fillSlider;
    public float hungerDecreaseAmount = 20f;  // Giảm 20 đơn vị độ đói mỗi lần
    public float hungerDecreaseInterval = 900f; // 15 phút (900 giây)

    void Start()
    {
        currentHunger = maxHunger;
        fillSlider = hungerSlider.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill");

        // Bắt đầu Coroutine giảm độ đói theo thời gian
        StartCoroutine(DecreaseHungerOverTime());
    }

    void Update()
    {
        hungerSlider.value = currentHunger;

        // Cập nhật màu thanh đói dựa trên giá trị
        if (currentHunger >= 50)
            fillSlider.color = Color.green;
        else if (currentHunger < 50 && currentHunger >= 30)
            fillSlider.color = new Color(1f, 0.5f, 0f); // Màu cam
        else if (currentHunger < 30)
            fillSlider.color = Color.red;
    }

    // Coroutine giảm độ đói sau mỗi 15 phút (900 giây)
    IEnumerator DecreaseHungerOverTime()
    {
        while (true) // Lặp vô hạn
        {
            yield return new WaitForSeconds(hungerDecreaseInterval); // Đợi 15 phút

            if (currentHunger > 0) // Giảm nếu độ đói > 0
            {
                currentHunger -= hungerDecreaseAmount;
                currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger); // Giới hạn từ 0 đến maxHunger
            }
        }
    }
}