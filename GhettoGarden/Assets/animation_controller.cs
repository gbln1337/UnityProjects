using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator gribAnimator;
    [SerializeField] private Animator krapivaAnimator;

    [SerializeField] private Transform playerTransform;

    private Vector3 lastPosition;
    [SerializeField] private float speedThreshold = 0.1f; // Теперь используется явно

    void Start()
    {
        if (playerTransform == null)
            playerTransform = transform;

        lastPosition = playerTransform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, lastPosition);
        float speed = distance / Time.deltaTime;
        lastPosition = playerTransform.position;

        UpdateAnimator(playerAnimator, speed);
        UpdateAnimator(gribAnimator, speed);
        UpdateAnimator(krapivaAnimator, speed);
    }

    void UpdateAnimator(Animator animator, float speed)
    {

        if (animator == null) return;

        animator.SetFloat("Speed", speed);

        // Используем порог из поля
        animator.SetBool("IsMoving", speed > speedThreshold);
        Debug.Log($"Speed: {speed}, IsMoving: {speed > speedThreshold}");
    }
}