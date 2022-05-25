using System;
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
    [SerializeField] private GameObject previewObectToPlace;
    [SerializeField] private Button btnGenerate;

    private bool useCursor = false;
    private bool placeObject = false;
    private Pose placementPose;

    GameObject spawnObject;

    void Start()
    {
        rotateObject = previewObectToPlace; // set default object for rotateObject
        // cursorChildObject.SetActive(useCursor);

        btnGenerate.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "GENERATE";
        
        btnGenerate.onClick.AddListener(ConditionPlayAR);
    }


    void Update()
    {
        ARController(); // set ar controller like scaling and rotating object

        UpdateCursor();
        UpdatePlacementIndicator();
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


}
