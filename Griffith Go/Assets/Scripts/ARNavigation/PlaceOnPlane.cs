using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AutoPlaceOnPlane : MonoBehaviour
{
    [Header("AR Settings")]
    public ARPlaneManager planeManager;
    public Camera arCamera;
    [Tooltip("Minimum plane size in meters")]
    public float minPlaneSize = 0.3f; 

    [Header("Object Settings")]
    public GameObject targetObject;
    public Light sceneLight;

    private bool isPlaced = false;

    void Start()
    {
        // component auto allocation
        if (arCamera == null) arCamera = FindObjectOfType<ARCameraManager>()?.gameObject.GetComponent<Camera>();
        if (planeManager == null) planeManager = FindObjectOfType<ARPlaneManager>();
        
        // targetObject=prefab
        if (targetObject != null && targetObject.scene.rootCount == 0)
        {
            targetObject = Instantiate(targetObject); 
        }
        targetObject.SetActive(false);
        
        if (sceneLight != null)
        {
            sceneLight.shadows = LightShadows.Soft;
            sceneLight.shadowStrength = 0.3f;
        }
    }

    void Update()
    {
        if (isPlaced || planeManager == null || arCamera == null) return;

        // check every plane
        foreach (var plane in planeManager.trackables)
        {
            // 1. filter horizontal
            if (plane.alignment != PlaneAlignment.HorizontalUp) 
                continue;
            // 2. area check
            if (plane.size.x * plane.size.y < minPlaneSize * minPlaneSize)
                continue;
            // 3. Camera view ^ plane area cross point 
            Vector3 hitPoint = GetCameraHitPoint(plane);
            if (hitPoint != Vector3.zero)
            {
                PlaceObject(hitPoint, plane);
                return;
            }
        }
    }

    private Vector3 GetCameraHitPoint(ARPlane plane)
    {
        // Ray from main camera
        Ray ray = new Ray(arCamera.transform.position, arCamera.transform.forward);
        Plane groundPlane = new Plane(plane.transform.up, plane.transform.position);

        // Raycast
        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }

    private void PlaceObject(Vector3 position, ARPlane plane)
    {
        targetObject.transform.position = position;
        targetObject.transform.rotation = Quaternion.Euler(0, arCamera.transform.rotation.eulerAngles.y, 0);
        targetObject.SetActive(true);
        isPlaced = true;

        // debug info
        Debug.Log($"Placed on plane (Size: {plane.size.x:F2}m x {plane.size.y:F2}m)");
        Debug.Log($"Camera: {arCamera.transform.position}, Object: {position}");

        // plane inactive
        plane.gameObject.SetActive(false);
    }

    public void ResetPlacement()
    {
        isPlaced = false;
        targetObject.SetActive(false);
    }
}