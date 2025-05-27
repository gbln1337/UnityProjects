using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 10; // Урон пули
    public float bulletSpeed = 100f; // Скорость пули
    public float lifetime = 10f; // Время жизни пули
    private Rigidbody rb;

    void Start()
    {
        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();

        // Если Rigidbody не найден, добавляем его
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false; // Пуля не должна падать

        // Применяем скорость пули
        rb.linearVelocity = transform.forward * bulletSpeed;

        // Уничтожаем пулю через заданное время
        Destroy(gameObject, lifetime);
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
                GameManager.instance.AddScore(20); // Увеличиваем счет на 20 очков
            }

            // Уничтожаем пулю игрока
            Destroy(gameObject);
            return;
        }
    }
}