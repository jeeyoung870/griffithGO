using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    [System.Serializable]
    public class Checkpoint
    {
        public double latitude;
        public double longitude;
        [HideInInspector] public Vector3 relativePosition;
    }

    public List<Checkpoint> shortestPath = new List<Checkpoint>();
    public List<Checkpoint> scenicPath = new List<Checkpoint>();
    public Quaternion rotationToAlign; // used when ArrowTester3 spawns objects
    private bool isReferenceSet = false;

    IEnumerator Start()
    {
        SetReferencePosition();
        yield break;
    }

    void SetReferencePosition()
    {
        double refLat = shortestPath[0].latitude;
        double refLon = shortestPath[0].longitude;
        foreach (var path in new[] { shortestPath, scenicPath })
        {
            foreach (var point in path)
            {
                point.relativePosition = GpsToArPosition(
                    point.latitude,
                    point.longitude,
                    refLat,
                    refLon
                );
            }
        }
        // calculate rotationToAlign: to adjust every objects' spawn right before camera
        Vector3 p0 = shortestPath[0].relativePosition; // == Vector3.zero
        Vector3 p1 = shortestPath[1].relativePosition;
        Vector3 routeDirection = (p1 - p0).normalized;
        Vector3 flatRouteDirection = new Vector3(routeDirection.x, 0, routeDirection.z);
        rotationToAlign = Quaternion.FromToRotation(flatRouteDirection, Vector3.forward);

        isReferenceSet = true;
        Debug.Log($"Anchor: from 1st Checkpoint Lat={refLat}, Lon={refLon}");
    }

    Vector3 GpsToArPosition(double targetLat, double targetLon, double refLat, double refLon)
    {
        double latDiff = (targetLat - refLat) * 111319.491;  // 111319.491 = meter constant
        double lonDiff = (targetLon - refLon) * 111319.491 * Mathf.Cos((float)refLat * Mathf.Deg2Rad);

        Vector3 rawPosition = new Vector3(
            (float)lonDiff, 
            0,
            (float)latDiff
        );
        return rawPosition;
    }

    public bool IsReady => isReferenceSet;
}