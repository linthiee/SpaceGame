using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private Rigidbody rigidBody;
   [SerializeField] private GameObject particlePrefab;

    [SerializeField] private float lifeTime = 3.0f;
    private void Awake()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    public void Logic(float speed)
    {
        rigidBody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        var bullet = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
