using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerController : MonoBehaviour
{
    [SerializeField] private UI_ShopController uiShop;
    private bool isPlayerInRange = false;  // Biến kiểm tra người chơi có trong phạm vi cửa hàng hay không

    private void Update()
    {
        // Nếu người chơi trong phạm vi và click chuột trái thì mở UI cửa hàng
        if (isPlayerInRange && Input.GetMouseButtonDown(0))
        {
            uiShop.Show();
            FindObjectOfType<SoundManager>().Play("shop");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))  // Kiểm tra nếu đối tượng vào là người chơi
        {
            isPlayerInRange = true;  // Đặt biến true khi người chơi trong phạm vi
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))  // Kiểm tra nếu đối tượng ra là người chơi
        {
            isPlayerInRange = false;  // Đặt biến false khi người chơi ra khỏi phạm vi
            uiShop.Hide();  // Ẩn UI cửa hàng
        }
    }
}