using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

public partial class ARManager : MonoBehaviour
{
    [SerializeField] private GameObject cursorObject;
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private GameObject previewObectToPlace;
    [SerializeField] private Button btnGenerate;

    [SerializeField] private Button btnToggleAR;
    [SerializeField] private Button btnToggleVisualizer;

    private delegate void DelegateToggleAR();
    private DelegateToggleAR delegateToggleAR;

    private delegate void DelegateToggleVisual();
    private DelegateToggleAR delegateToggleVisual;

    private bool useCursor = false;
    private bool placeObject = false;
    private Pose placementPose;

    private Color32 colorEnable = new Color32(147, 61, 255, 255); // purple
    private Color32 colorDisable = new Color32(73, 73, 73, 255); // grey

    GameObject spawnObject;

    void Start()
    {
        rotateObject = previewObectToPlace; // set default object for rotateObject

        delegateToggleVisual = DisableVisual;
        delegateToggleAR = DisableAR;

        btnGenerate.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "GENERATE";

        btnGenerate.onClick.AddListener(ConditionPlayAR);

        btnToggleAR.onClick.AddListener(SetToggleAR);
        btnToggleVisualizer.onClick.AddListener(SetToggleVisual);
        // btnToggle.onClick.AddListener();
    }


    void Update()
    {
        ARController(); // set ar controller like scaling and rotating object

        UpdateCursor();
        UpdatePlacementIndicator();
    }

    private void SetToggleAR()
    {
        delegateToggleAR?.Invoke();
        if (delegateToggleAR == DisableAR)
        {
            delegateToggleAR = EnableAR;
        }
        else if (delegateToggleAR == EnableAR)
        {
            delegateToggleAR = DisableAR;
        }
    }

    private void DisableAR()
    {
        arPlaneManager.enabled = false;
        raycastManager.enabled = false;
        btnToggleAR.GetComponent<Image>().color = colorDisable;
        btnToggleAR.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "OFF";
    }

    private void EnableAR()
    {
        arPlaneManager.enabled = true;
        raycastManager.enabled = true;
        btnToggleAR.GetComponent<Image>().color = colorEnable;
        btnToggleAR.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ON";
    }

    private void SetToggleVisual()
    {
        delegateToggleVisual?.Invoke();
        if (delegateToggleVisual == DisableVisual)
        {
            delegateToggleVisual = EnableVisual;
        }
        else if (delegateToggleVisual == EnableVisual)
        {
            delegateToggleVisual = DisableVisual;
        }
    }

    private void DisableVisual()
    {
        SetAllPlaneActive(false);
        btnToggleVisualizer.GetComponent<Image>().color = colorDisable;
        btnToggleVisualizer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "OFF";
    }

    private void EnableVisual()
    {
        SetAllPlaneActive(true);
        btnToggleVisualizer.GetComponent<Image>().color = colorEnable;
        btnToggleVisualizer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ON";
    }

    private void SetAllPlaneActive(bool value)
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

    private void ConditionPlayAR()
    {
        if (!spawnObject)
        {
            if (!useCursor) return;
            PlaceObject();
            btnGenerate.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PLAY";
        }
        else
        {
            btnGenerate.gameObject.SetActive(false);
            ARPlay.Instance.PlayGame();
        }
    }

    private void PlaceObject()
    {
        if (spawnObject) return;

        Parameters.scalingObject = scaleObject.y;

        spawnObject = Instantiate(objectToPlace, placementPose.position, rotateObject.transform.rotation);
        spawnObject.transform.localScale = scaleObject;

        cursorObject.transform.GetChild(0).gameObject.SetActive(false);
        previewObectToPlace.SetActive(false);
    }

    private void UpdatePlacementIndicator()
    {
        if (useCursor && !spawnObject)
        {
            cursorObject.transform.GetChild(0).gameObject.SetActive(true);
            previewObectToPlace.SetActive(true);

            // cursorChildObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            previewObectToPlace.transform.position = cursorObject.transform.position;
        }
        else if (!useCursor)
        {
            cursorObject.transform.GetChild(0).gameObject.SetActive(false);
            previewObectToPlace.SetActive(false);
        }
    }

    private void UpdateCursor()
    {
        if (placeObject) return;
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        if (arPlaneManager.enabled)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(screenPosition, hits, TrackableType.Planes);

            useCursor = hits.Count > 0;
            if (useCursor)
            {
                placementPose = hits[0].pose;

                cursorObject.transform.rotation = hits[0].pose.rotation;
                cursorObject.transform.position = hits[0].pose.position;

                // transform.rotation = hits[0].pose.rotation;

                // var cameraForward = Camera.main.transform.forward;
                // var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

                // placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
        else
        {
            useCursor = false;
        }
    }


}
