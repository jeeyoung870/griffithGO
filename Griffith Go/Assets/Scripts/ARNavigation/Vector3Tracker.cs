using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vector3Tracker : MonoBehaviour
{
    public Camera arCamera; 
    public TMP_Text positionText; // UI Text (TMP)
    public float updateInterval = 0.2f; 
    private float timer;

    void Start()
    {
        if (arCamera == null)
            arCamera = Camera.main; // auto allocation

        UpdatePositionText();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            UpdatePositionText();
            timer = 0;
        }
    }

    void UpdatePositionText()
    {
        if (arCamera != null && positionText != null)
        {
            Vector3 pos = arCamera.transform.position;
            positionText.text = $"X: {pos.x:F2}  Y: {pos.y:F2}  Z: {pos.z:F2}";
        }
    }
}
