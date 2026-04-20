using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        float movementY = 0;

        if (Input.GetKey(KeyCode.Space))
            movementY = 1;
        if (Input.GetKey(KeyCode.LeftControl))
            movementY = -1;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), movementY, Input.GetAxis("Vertical"));

        transform.Translate(movement * (speed * Time.deltaTime));

        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;

        float deltaX = mousePos.x - playerScreenPos.x;
        float deltaY = mousePos.y - playerScreenPos.y;

        float angle = Mathf.Atan2(deltaX, deltaY) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0f, angle, 0f);
    }
}
