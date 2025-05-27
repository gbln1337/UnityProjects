using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public int damage = 10; // Урон пули
    public float bulletSpeed = 100f; // Скорость пули
    private Rigidbody rb;

    // Добавляем поле direction
    [HideInInspector]
    public Vector3 direction; // Направление полёта пули

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;

        // Используем заданное направление вместо transform.forward
        rb.linearVelocity = direction.normalized * bulletSpeed;

        // Удаляем пулю через 5 секунд
        Destroy(gameObject, 5f); // Удаление через 5 секунд
    }

    void OnCollisionEnter(Collision collision)
    {
        // Если пуля игрока столкнулась с пулей врага
        if (collision.gameObject.CompareTag("enemy_bullet") && CompareTag("player_bullet"))
        {
            // Уничтожаем обе пули
            Destroy(gameObject);
            Destroy(collision.gameObject);  // Уничтожаем пулю врага
            return;
        }

        // Если пуля врага столкнулась с игроком
        if (collision.gameObject.CompareTag("Player") && CompareTag("enemy_bullet"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject); // Уничтожаем пулю
            return;
        }

        // Если пуля игрока столкнулась с врагом
        if (collision.gameObject.CompareTag("Enemy") && CompareTag("player_bullet"))
        {
            // Уничтожаем врага
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Наносим урон врагу
            }

            // Уничтожаем пулю игрока
            Destroy(gameObject);
            return;
        }
    }
}