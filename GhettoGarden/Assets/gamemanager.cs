using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;

    private int score = 0;
    private PlayerController player;

    // Префабы для спавна
    public GameObject healPrefab; // Префаб хилка
    public GameObject gribPrefab; // Префаб grib
    public GameObject krapivaPrefab; // Префаб krapiva

    // Интервалы спавна
    public float healSpawnInterval = 10f; // Интервал между спавнами хилок
    public float gribSpawnInterval = 15f; // Интервал между спавнами grib
    public float krapivaSpawnInterval = 20f; // Интервал между спавнами krapiva

    private float nextHealSpawnTime;
    private float nextGribSpawnTime;
    private float nextKrapivaSpawnTime;

    public float spawnRange = 10f; // Диапазон спавна объектов по осям X и Z

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        nextHealSpawnTime = Time.time + healSpawnInterval; // Устанавливаем время первого спавна
        nextGribSpawnTime = Time.time + gribSpawnInterval; // Устанавливаем время первого спавна grib
        nextKrapivaSpawnTime = Time.time + krapivaSpawnInterval; // Устанавливаем время первого спавна krapiva
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

        // Спавн grib
        if (Time.time >= nextGribSpawnTime)
        {
            SpawnGrib();
            nextGribSpawnTime = Time.time + gribSpawnInterval;
        }

        // Спавн krapiva
        if (Time.time >= nextKrapivaSpawnTime)
        {
            SpawnKrapiva();
            nextKrapivaSpawnTime = Time.time + krapivaSpawnInterval;
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
            Vector3 playerPosition = player.transform.position;

            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);

            Vector3 spawnPosition = new Vector3(playerPosition.x + randomX, playerPosition.y, playerPosition.z + randomZ);

            Instantiate(healPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Метод для спавна grib
    void SpawnGrib()
    {
        if (gribPrefab != null && player != null)
        {
            Vector3 playerPosition = player.transform.position;

            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);

            Vector3 spawnPosition = new Vector3(playerPosition.x + randomX, playerPosition.y, playerPosition.z + randomZ);

            Instantiate(gribPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Метод для спавна krapiva
    void SpawnKrapiva()
    {
        if (krapivaPrefab != null && player != null)
        {
            Vector3 playerPosition = player.transform.position;

            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);

            Vector3 spawnPosition = new Vector3(playerPosition.x + randomX, playerPosition.y, playerPosition.z + randomZ);

            Instantiate(krapivaPrefab, spawnPosition, Quaternion.identity);
        }
    }
}