using UnityEngine;

public class HealPickup : MonoBehaviour
{
    public int healAmount = 20;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
