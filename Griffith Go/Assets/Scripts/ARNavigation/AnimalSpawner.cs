using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public float triggerDistance = 4f; // trigger when user moved over 4 meters
    public Vector3 offsetFromUser = new Vector3(2, 1.5f, -2); // left below of camera
    public GameObject animalPopup;

    private Transform arCamera;
    private bool hasSpawned = false;
    private Renderer objRenderer;
    private Vector3 initialPosition = Vector3.zero; // app start point

    void Start()
    {
        arCamera = Camera.main.transform;
        objRenderer = GetComponent<Renderer>();
        if (objRenderer == null)
        {
            objRenderer = GetComponentInChildren<Renderer>(true);
        }
        objRenderer.enabled = false; // invisible at first
    }

    void Update()
    {
        if (hasSpawned || arCamera == null) return;
        // compare initialPosition and current position
        Vector3 flatCurrentPos = new Vector3(arCamera.position.x, 0, arCamera.position.z);
        float distance = Vector3.Distance(flatCurrentPos, initialPosition);
        if (distance >= triggerDistance)
        {
            animalPopup.SetActive(true);
            SpawnAnimal();
            hasSpawned = true;
        }
    }

    void SpawnAnimal()
    {
        // calculate the position of left below of Camera
        Vector3 spawnDirection = 
            -arCamera.right * offsetFromUser.x +
            -arCamera.up * offsetFromUser.y +
            -arCamera.forward * offsetFromUser.z;
        Vector3 spawnPosition = arCamera.position + spawnDirection.normalized * 1.5f;
        transform.position = spawnPosition;
        transform.rotation = Quaternion.LookRotation(arCamera.forward); // lookat user

        Debug.Log("🐾 Animal spawned at: " + spawnPosition);
    }
}
