using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffectPrefab;
    public GameObject other;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Destructible")
        {
            Destroy(collision.gameObject);
            Debug.Log("Снаряд столкнулся с: " + collision.gameObject.name);
        }
        if (hitEffectPrefab)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

    }
}

