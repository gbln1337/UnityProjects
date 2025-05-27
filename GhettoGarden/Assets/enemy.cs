using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int damage = 10;
    public float bulletSpeed= 10f;
    public float shootInterval = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int health = 20;
    public float jumpForce = 5f;
    public bool isFlying = false;
    public bool isJumping = false;
    public Transform target;
    private float nextShot;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        nextShot = Time.time + shootInterval;
    }
    void OnCollisionEnter(Collision collision)
    {
        // Если пуля врага столкнулась с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Уничтожаем пулю врага
            Destroy(gameObject);
        }
        
    }
    void Update()
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;

        if (isFlying)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position + Vector3.up * 2, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += new Vector3(dir.x, 0, dir.z) * moveSpeed * Time.deltaTime;

            if (isJumping && Mathf.Abs(GetComponent<Rigidbody>().linearVelocity.y) < 0.1f)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if (Time.time >= nextShot)
        {
            Shoot();
            nextShot = Time.time + shootInterval;
        }
    }

void Shoot()
{
    if (bulletPrefab != null && firePoint != null)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Enemy_Bullet bullet = bulletGO.GetComponent<Enemy_Bullet>(); // Используем правильный класс

        if (bullet != null)
        {
            // Задаем направление пули — к игроку
            bullet.direction = (target.position - firePoint.position).normalized;
        }
    }
    else
    {
        Debug.LogWarning("Bullet Prefab или Fire Point не назначен у " + gameObject.name);
    }
}


    public void TakeDamage(int dmg)
{
    health -= dmg;
    if (health <= 0)
    {
        // Уничтожаем врага
        Destroy(gameObject);
        GameManager.instance.AddScore(20); // Добавляем очки за уничтожение врага
    }
}

}