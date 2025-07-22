using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class AnimalCollecter : MonoBehaviour
{
    public GameObject getAnimalPopup;
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;
        // check object ray hit
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == this.transform)
                StartCoroutine(CollectAfterDelay());
        }
    }

    IEnumerator CollectAfterDelay()
    {
        yield return new WaitForSeconds(1f); // wait for 1 second
        Debug.Log("✅ Animal touched via raycast");
        XPManager.Instance.AddXP(30);
        if (getAnimalPopup != null)
            getAnimalPopup.SetActive(true);
        Destroy(gameObject);
    }
}