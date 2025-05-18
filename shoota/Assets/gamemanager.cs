using UnityEngine; // Это необходимо для использования MonoBehaviour
using TMPro; // Для работы с TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;

    private int score = 0;
    private PlayerController player;

    public GameObject healPrefab; // Префаб хилка
    public float healSpawnInterval = 10f; // Интервал между спавнами хилок
    private float nextHealSpawnTime;

    public float spawnRange = 10f; // Диапазон спавна хилок по осям X и Z

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        nextHealSpawnTime = Time.time + healSpawnInterval; // Устанавливаем время первого спавна
    }

    void Update()
    {
        // Обновляем значения UI
        if (player != null)
        {
            healthText.text = "HP: " + player.currentHealth;
            ammoText.text = "Ammo: " + player.ammo;
        }

        scoreText.text = "Score: " + score;

        // Спавн хилок
        if (Time.time >= nextHealSpawnTime && player.currentHealth < player.maxHealth)
        {
            SpawnHealItem();
            nextHealSpawnTime = Time.time + healSpawnInterval;
        }
    }

    public void AddScore(int s)
    {
        score += s;
    }

    // Метод для спавна хила
    void SpawnHealItem()
    {
        if (healPrefab != null && player != null)
        {
            // Получаем позицию игрока
            Vector3 playerPosition = player.transform.position;

            // Случайным образом изменяем координаты X и Z в пределах диапазона от -10 до 10 метров
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);

            // Создаем позицию для хилка на уровне Y игрока
            Vector3 spawnPosition = new Vector3(playerPosition.x + randomX, playerPosition.y, playerPosition.z + randomZ);

            // Создаем хилка в вычисленной позиции
            Instantiate(healPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
