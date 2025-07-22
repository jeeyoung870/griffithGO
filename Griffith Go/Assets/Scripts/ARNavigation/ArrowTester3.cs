using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RouteManager;

public class ArrowTester3 : MonoBehaviour
{
    public RouteManager routeManager;
    public GameObject conePrefab;   // arrow prefab
    public GameObject destinationBadge; // destination badge prefab
    public float rotationSpeed = 70f; // Y-axis rotate speed (degree/sec)

    void Start()
    {
        if (conePrefab == null || routeManager == null || destinationBadge == null)
        {
            Debug.LogError("no allocation of component!");
            return;
        }
        StartCoroutine(WaitForGpsAndSpawn());
    }

    IEnumerator WaitForGpsAndSpawn()
    {
        yield return new WaitUntil(() => routeManager.IsReady);
        SpawnTestArrows();
    }

    void SpawnTestArrows()
    {
        List<Checkpoint> route = routeManager.shortestPath;
        Quaternion routeRotation = routeManager.rotationToAlign;

        for (int i = 1; i < route.Count; i++)   // i=0 > start point: no object
        {
            Vector3 rotatedPos = routeRotation * route[i].relativePosition;
            GameObject obj;
            if (i == route.Count - 1)
            {
                obj = Instantiate(destinationBadge, rotatedPos, Quaternion.identity);
                obj.AddComponent<RotateOnYAxis>().speed = rotationSpeed;
            }
            else
            {
                obj = Instantiate(conePrefab, rotatedPos, Quaternion.identity);
                Vector3 nextRotated = routeRotation * route[i + 1].relativePosition;
                Vector3 direction = nextRotated - rotatedPos;
                obj.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
            }
            Debug.Log($"checkpoint: {obj.name} | location: {rotatedPos}");
        }
    }
}

// Y-axis rotation Component
public class RotateOnYAxis : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}