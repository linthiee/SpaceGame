using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 100.0f;

    [SerializeField] private Engine engineLeft;
    [SerializeField] private Engine engineRight;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform tip;

    [SerializeField] private float shootingSpeed = 100.0f;

    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private AudioClip explosionSound;

    private float nextFireTime = 0f;
    private void Start()
    {
        engineLeft.Set(false);
        engineRight.Set(false);
    }
    private void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");
        float verticalInput = GetVerticalInput();

        HandleRotation(turnInput);
        HandleMovement(forwardInput, verticalInput);
        HandleEngines(forwardInput, turnInput);
    }
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Bullet bullet = Instantiate(bulletPrefab, tip.position, tip.rotation);
            bullet.Logic(shootingSpeed);
        }
    }
    private float GetVerticalInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return 1f;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return -1f;
        }
        return 0f;
    }

    private void HandleRotation(float turnInput)
    {
        transform.Rotate(0f, turnInput * rotationSpeed * Time.deltaTime, 0f);
    }

    private void HandleMovement(float forwardInput, float verticalInput)
    {
        Vector3 forwardMovement = transform.forward * forwardInput;
        Vector3 verticalMovement = Vector3.up * verticalInput;
        Vector3 finalMovement = forwardMovement + verticalMovement;

        transform.Translate(finalMovement * (speed * Time.deltaTime), Space.World);
    }

    private void HandleEngines(float forwardInput, float turnInput)
    {
        bool isMovingForward = forwardInput > 0.1f;
        bool isTurningRight = turnInput > 0.1f;
        bool isTurningLeft = turnInput < -0.1f;

        bool leftEngineActive = isMovingForward || isTurningRight;
        bool rightEngineActive = isMovingForward || isTurningLeft;

        if (engineLeft != null)
        { 
            engineLeft.Set(leftEngineActive); 
        }

        if (engineRight != null)
        {
            engineRight.Set(rightEngineActive);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlanetOrbit>(out PlanetOrbit planetComponent))
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("player collided with a planet!");

        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
        }

        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(null);
        }

        Destroy(gameObject);
        GameManager.Instance.GameOver();
    }
}