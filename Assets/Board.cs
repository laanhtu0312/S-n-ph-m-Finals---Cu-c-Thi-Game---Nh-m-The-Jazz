using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject dialogueBox; // Hộp hội thoại
    private bool isDialogueActive = false; // Trạng thái hộp hội thoại
    private bool isPlayerInRange = false;  // Kiểm tra người chơi có trong phạm vi

    private void Start()
    {
        dialogueBox.SetActive(false); // Ẩn hộp hội thoại lúc đầu
    }

    private void Update()
    {
        // Nếu người chơi đã ra khỏi phạm vi và hộp hội thoại đang mở, tự động đóng hộp thoại
        if (isDialogueActive && !isPlayerInRange)
        {
            CloseDialogue();
        }
    }

    private void OnMouseDown()
    {
        if (!isDialogueActive && isPlayerInRange)
        {
            ShowDialogue(); // Chỉ mở hộp thoại nếu người chơi ở trong phạm vi
        }
    }

    void ShowDialogue()
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true); // Hiển thị hộp hội thoại
    }

    public void CloseDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false); // Tắt hộp hội thoại
    }

    // Kiểm tra khi người chơi vào phạm vi
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true; // Đánh dấu người chơi đã vào phạm vi
        }
    }

    // Kiểm tra khi người chơi ra khỏi phạm vi
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false; // Đánh dấu người chơi đã ra khỏi phạm vi
        }
    }
}