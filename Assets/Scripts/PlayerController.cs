using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 150.0f;

    private void Update()
    {
        float movementY = 0;

        if (Input.GetKey(KeyCode.Space))
            movementY = 1;
        if (Input.GetKey(KeyCode.LeftControl))
            movementY = -1;

        float turn = Input.GetAxis("Horizontal");

        transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);

        Vector3 forwardMovement = transform.forward * Input.GetAxis("Vertical");
        Vector3 verticalMovement = Vector3.up * movementY;

        Vector3 finalMovement = forwardMovement + verticalMovement;

        transform.Translate(finalMovement * (speed * Time.deltaTime), Space.World);
    }
}