using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform cannonPivot;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float rotationSpeed = 50f;
    public float shootForce = 700f;
    public ParticleSystem shootEffect;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        cannonPivot.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        cannonPivot.Rotate(Vector3.right, -vertical * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.up * shootForce);

            if (shootEffect) shootEffect.Play();

            //Camera.main.GetComponent<FollowCamera>().target = projectile.transform;
        }
    }
}