using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private float pitchSpeed = 100.0f;
    [SerializeField] private float rollSpeed = 100.0f;

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

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        float forwardInput = Input.GetAxis("Vertical"); 
        float verticalInput = GetVerticalInput();      

        float yawInput = Input.GetAxis("Horizontal");  
        float pitchInput = Input.GetAxis("Mouse Y");    
        float rollInput = GetRollInput();               

        HandleRotation(pitchInput, yawInput, rollInput);
        HandleMovement(forwardInput, verticalInput);
        HandleEngines(forwardInput, yawInput);
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

    private float GetRollInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            return 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            return -1f; 
        }
        return 0f;
    }

    private void HandleRotation(float pitchInput, float yawInput, float rollInput)
    {
        float pitch = -pitchInput * pitchSpeed * Time.deltaTime;
        float yaw = yawInput * rotationSpeed * Time.deltaTime;
        float roll = rollInput * rollSpeed * Time.deltaTime;

        transform.Rotate(pitch, yaw, roll, Space.Self);
    }

    private void HandleMovement(float forwardInput, float verticalInput)
    {
        Vector3 forwardMovement = transform.forward * forwardInput;
        Vector3 verticalMovement = transform.up * verticalInput;
        Vector3 finalMovement = forwardMovement + verticalMovement;

        transform.Translate(finalMovement * (speed * Time.deltaTime), Space.World);
    }

    private void HandleEngines(float forwardInput, float yawInput)
    {
        bool isMovingForward = forwardInput > 0.1f;
        bool isTurningRight = yawInput > 0.1f;
        bool isTurningLeft = yawInput < -0.1f;

        bool leftEngineActive = isMovingForward || isTurningRight;
        bool rightEngineActive = isMovingForward || isTurningLeft;

        engineLeft.Set(leftEngineActive);
        engineRight.Set(rightEngineActive);
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

        if (explosionSound != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
        }

        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(null);
        }

        Cursor.lockState = CursorLockMode.None;

        Destroy(gameObject);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}