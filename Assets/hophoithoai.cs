using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hophoithoai : MonoBehaviour
{
    public GameObject dialogueBox;   // Hộp hội thoại
    public Text dialogueText;        // Văn bản trong hộp thoại
    public bool isDialogueActive;    // Trạng thái xem hộp thoại có đang mở không
    private Queue<string> dialogueQueue;  // Hàng đợi chứa các câu thoại

    private void Start()
    {
        dialogueQueue = new Queue<string>();
        dialogueBox.SetActive(false);  // Tắt hộp thoại ban đầu
        isDialogueActive = false;
    }

    // Bắt đầu hội thoại với danh sách câu thoại
    public void StartDialogue(List<string> dialogues)
    {
        dialogueQueue.Clear();  // Xóa hàng đợi trước đó (nếu có)

        foreach (string sentence in dialogues)
        {
            dialogueQueue.Enqueue(sentence);  // Thêm câu thoại vào hàng đợi
        }

        isDialogueActive = true;
        dialogueBox.SetActive(true);  // Hiển thị hộp thoại
        DisplayNextSentence();  // Hiển thị câu đầu tiên
    }

    // Hiển thị câu thoại tiếp theo trong hàng đợi
    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();  // Kết thúc hội thoại nếu không còn câu thoại
            return;
        }

        string sentence = dialogueQueue.Dequeue();
        dialogueText.text = sentence;  // Cập nhật nội dung câu thoại
    }

    // Kết thúc hội thoại
    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);  // Tắt hộp thoại
    }

    private void Update()
    {
        // Khi người chơi nhấn phím cách và hộp thoại đang hoạt động
        if (Input.GetKeyDown(KeyCode.Space) && isDialogueActive)
        {
            DisplayNextSentence();  // Hiển thị câu thoại tiếp theo
        }
    }
}