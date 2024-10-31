using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float patrolTime = 5f;  // Thời gian di chuyển
    public float idleTime = 3f;    // Thời gian đứng yên
    public float detectionRange = 5f; // Phạm vi phát hiện người chơi
    public Animator animator;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Transform player;
    private bool isIdle = false;
    private bool isChasingPlayer = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi qua Tag
        StartCoroutine(PatrolAndIdleCycle());
    }

    private void Update()
    {
        if (isChasingPlayer)
        {
            ChasePlayer(); // Đuổi theo người chơi
        }
        else if (!isIdle)
        {
            rb.velocity = moveDirection * moveSpeed; // Di chuyển tuần tra
            UpdateAnimation(); // Cập nhật animation
        }
        else
        {
            rb.velocity = Vector2.zero; // Dừng lại khi Idle
            animator.Play("Idle"); // Animation đứng yên
        }

        DetectPlayer(); // Kiểm tra xem người chơi có trong phạm vi không
    }

    IEnumerator PatrolAndIdleCycle()
    {
        while (true)
        {
            // Di chuyển ngẫu nhiên trong thời gian tuần tra
            isIdle = false;
            moveDirection = GetRandomDirection();
            yield return new WaitForSeconds(patrolTime);

            // Đứng yên trong thời gian Idle
            isIdle = true;
            yield return new WaitForSeconds(idleTime);
        }
    }

    Vector2 GetRandomDirection()
    {
        // Chọn hướng di chuyển ngẫu nhiên: lên, xuống, trái, phải
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0: return Vector2.up;
            case 1: return Vector2.down;
            case 2: return Vector2.right;
            case 3: return Vector2.left;
        }
        return Vector2.zero;
    }

    private void UpdateAnimation()
    {
        if (moveDirection == Vector2.up)
        {
            animator.Play("Run_Up"); // Animation chạy lên
        }
        else if (moveDirection == Vector2.down)
        {
            animator.Play("Run_Down"); // Animation chạy xuống
        }
        else if (moveDirection == Vector2.right)
        {
            animator.Play("Run_Side"); // Animation chạy ngang phải
            transform.localScale = new Vector3(-1, 1, 1); // Không lật hình
        }
        else if (moveDirection == Vector2.left)
        {
            animator.Play("Run_Side"); // Animation chạy ngang trái
            transform.localScale = new Vector3(1, 1, 1); // Lật hình sang trái
        }
    }

    private void DetectPlayer()
    {
        // Kiểm tra khoảng cách giữa quái vật và người chơi
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            isChasingPlayer = true; // Bắt đầu đuổi người chơi
            StopAllCoroutines(); // Dừng tuần tra
        }
        else
        {
            isChasingPlayer = false; // Quay lại tuần tra nếu người chơi ra khỏi phạm vi
        }
    }

    private void ChasePlayer()
    {
        moveDirection = (player.position - transform.position).normalized; // Hướng về phía người chơi
        rb.velocity = moveDirection * moveSpeed; // Di chuyển về phía người chơi
        UpdateAnimation(); // Cập nhật animation khi di chuyển
    }

    private void OnDrawGizmosSelected()
    {
        // Hiển thị phạm vi phát hiện người chơi trong Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}