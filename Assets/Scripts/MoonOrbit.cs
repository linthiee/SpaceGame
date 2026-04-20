using Unity.VisualScripting;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    [SerializeField] private Transform earth;
    [SerializeField] private float orbitSpeed = 10f;
    [SerializeField] private float rotationSpeed = 50f;
    private void Update()
    {
        if (earth != null)
        {
            Vector3 earthPos = earth.position;
            transform.RotateAround(earthPos, Vector3.up, orbitSpeed * Time.deltaTime);
        }

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

    }
}
