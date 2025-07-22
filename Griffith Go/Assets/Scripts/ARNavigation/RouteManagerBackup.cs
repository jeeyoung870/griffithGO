using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManagerBackup : MonoBehaviour
{
    public List<Vector3> path1 = new List<Vector3>(); // shortest
    public List<Vector3> path2 = new List<Vector3>(); // scenic

    void Start()
    {
        // List of checkpoints (test values)
        path1.Add(new Vector3(0, -0.4f, 1f));  // start point
        path1.Add(new Vector3(-1.26f, -0.4f, 3.39f));  // corner 1
        path1.Add(new Vector3(-7.39f, -0.4f, 2.75f));  // corner 2
        path1.Add(new Vector3(-8.74f, -0.0f, 6.85f)); // destination

        path2.Add(new Vector3(0, 0, -10));
        path2.Add(new Vector3(-5, 0, -15));
        path2.Add(new Vector3(-10, 0, -20)); // destination
    }
}
