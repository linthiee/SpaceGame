using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 150.0f;
    [SerializeField] private Engine engineLeft;
    [SerializeField] private Engine engineRight;

    private void Start()
    {
        engineLeft.Set(false);
        engineRight.Set(false);
    }
    private void Update()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");
        float verticalInput = GetVerticalInput();

        HandleRotation(turnInput);
        HandleMovement(forwardInput, verticalInput);
        HandleEngines(forwardInput, turnInput);
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
}