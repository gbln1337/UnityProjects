using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public int maxHealth = 100;
    public int currentHealth;
    public int ammo = 10;

    public int damage = 10;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 100f;
    public AudioSource shootSound;

    private Camera cam;
    private Vector3 moveDirection = Vector3.forward;
    public Vector3 cameraOffset = new Vector3(0, 5, -5); // Камера над игроком и позади

    void Start()
    {
        currentHealth = maxHealth;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Если пуля врага столкнулась с игроком
        if (collision.gameObject.CompareTag("enemy_bullet"))
        {
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Уничтожаем пулю врага
            // Destroy(gameObject);
        }
        // if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     Debug.Log("stolknovenie s vragom");
        // }
    }
    void Update()
    {
        Move();

        // Стрельба
        if (Input.GetKeyDown(KeyCode.Space) && ammo > 0)
        {
            Shoot();
        }

        // Перезарядка
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Move()
    {
        float rotation = 0f;

        if (Input.GetKey(KeyCode.D))
            rotation = rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            rotation = -rotationSpeed * Time.deltaTime;

        transform.Rotate(0, rotation, 0);

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) // Назад
            direction += transform.forward;
        if (Input.GetKey(KeyCode.S)) // Вперёд
            direction -= transform.forward;

        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(transform.forward));

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = bullet.AddComponent<Rigidbody>();
            }

            rb.useGravity = false;
            rb.linearVelocity = transform.forward * bulletSpeed; // Направление — строго вперёд

            // Визуальный след
            if (bullet.GetComponent<TrailRenderer>() == null)
            {
                TrailRenderer trail = bullet.AddComponent<TrailRenderer>();
                trail.time = 0.2f;
                trail.startWidth = 0.1f;
                trail.endWidth = 0.01f;
                trail.material = new Material(Shader.Find("Sprites/Default"));
                trail.startColor = Color.yellow;
                trail.endColor = Color.red;
            }

            ammo--;  // Уменьшаем количество патронов

            if (shootSound != null)
                shootSound.Play();
        }
    }

    void Reload()
    {
        ammo = 10;  // Перезарядка: восстанавливаем 10 патронов
        Debug.Log("Перезарядка завершена!");
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
    void Die()
    {
        // Останавливаем игру
        Time.timeScale = 0f;

        // Можно показать экран Game Over
        Debug.Log("Игрок погиб! Игра остановлена.");

        // Альтернатива: перезагрузить сцену или выйти в меню
        // SceneManager.LoadScene("GameOverScene"); // если есть отдельная сцена
    }
}
