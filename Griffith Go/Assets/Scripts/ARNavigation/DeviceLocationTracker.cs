using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.Android; // Android permission

public class DeviceLocationTracker : MonoBehaviour
{
    public TMP_Text locationText;
    public float updateInterval = 1.0f;

    private float lastUpdateTime;

    void Start()
    {
        #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        #endif
        Input.location.Start(0.5f, 0.5f); // update every 0.5m 
    }

    void Update()
    {
        if (Time.time - lastUpdateTime > updateInterval)
        {
            UpdateLocation();
            lastUpdateTime = Time.time;
        }
    }

    void UpdateLocation()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            LocationInfo loc = Input.location.lastData;
            locationText.text = $"Lat: {loc.latitude:F6} Lon: {loc.longitude:F6}\nAlt: {loc.altitude:F1}m";
        }
        else
        {
            locationText.text = "GPS connecting...";
        }
    }

    void OnDestroy()
    {
        Input.location.Stop();
    }
}