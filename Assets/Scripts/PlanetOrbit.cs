using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    [SerializeField] private Transform sun;
    [SerializeField] private float orbitSpeed = 10f;
    [SerializeField] private float rotationSpeed = 50f;
    private void Update()
    {
        if (sun != null)
        {
            Vector3 sunPos = sun.position;
            transform.RotateAround(sunPos, Vector3.up, orbitSpeed * Time.deltaTime);
        }

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}