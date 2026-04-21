using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    [SerializeField] private Transform sun;
    [SerializeField] private float orbitSpeed = 10f;
    [SerializeField] private float rotationSpeed = 50f;

    [SerializeField] private int maxHealth = 3;

    [SerializeField] private bool isSun;

    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;

        if (!isSun)
        {
            GameManager.Instance.RegisterPlanet();
        }
    }

    private void Update()
    {
        if (sun != null)
        {
            Vector3 sunPos = sun.position;
            transform.RotateAround(sunPos, Vector3.up, orbitSpeed * Time.deltaTime);
        }

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("somebody hit the planet " + other.name);
        if (other.TryGetComponent<Bullet>(out Bullet bulletComponent))
        {
            other.gameObject.SetActive(false);
            TakeDamage(1);
        }
    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("planet was attacked!");

        if (currentHealth <= 0)
        {
            Debug.Log("planet died!");
            Die();
        }
    }
    private void Die()
    {
        if (!isSun)
        {
            GameManager.Instance.PlanetDestroyed();
        }

        Destroy(gameObject);
    }
}