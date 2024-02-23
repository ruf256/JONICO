using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraDelay : MonoBehaviour
{
    public float rotationSpeed = 0.1f;
    private Quaternion targetRotation;
    public float offsetY;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.y += offsetY; // Agrega el desplazamiento en el eje Y

        targetRotation = Quaternion.LookRotation(cameraPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
