using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CheckpointTrigger : MonoBehaviour
{
    public float activationDistance = 4f;
    public float collectionDistance = 1f;
    private Transform arCamera;
    private Renderer objRenderer; // manage object visibility
    public GameObject onCollectEffect;
    public bool isDestination = false;  // true if this is Destination badge obj


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
        if (arCamera == null) return;

        // calculation of XZ plane distance (ignore Y value)
        Vector2 camPos = new Vector2(arCamera.position.x, arCamera.position.z);
        Vector2 objPos = new Vector2(transform.position.x, transform.position.z);
        float distance = Vector2.Distance(camPos, objPos);
        // visible / unvisible check
        if (distance <= activationDistance && !objRenderer.enabled)
        {
            objRenderer.enabled = true;
        }
        else if (distance > activationDistance && objRenderer.enabled)
        {
            objRenderer.enabled = false;
        }
        // collection condition check 
        if (distance <= collectionDistance && objRenderer.enabled)
        {
            CollectCheckpoint();
        }
    }

    void CollectCheckpoint()
    {
        if (isDestination)  // destination badge collected
        {
            XPManager.Instance.AddXP(100);
            CanvasGroup popupGroup = GameObject.Find("Complete_popup").GetComponent<CanvasGroup>();
            // let Complete_popup visible, with CanvasGroup value change 
            if (popupGroup != null)
            {
                popupGroup.alpha = 1;
                popupGroup.interactable = true;
                popupGroup.blocksRaycasts = true;
            }
        }
        else    // arrow collected
        {   
            XPManager.Instance.AddXP(10);
        }
        // common function
        if (onCollectEffect != null)
        {
            Instantiate(onCollectEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}