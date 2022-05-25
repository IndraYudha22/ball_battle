using System;
using System.Collections;
using UnityEngine;

public partial class ARManager
{
    private float initialDistance;
    private Vector3 initialScale;
    private float startingPointRotate;

    private Vector3 scaleObject;
    private GameObject rotateObject;

    private void ARController()
    {
        ScaleObject();
        RotateObject();
    }

    private void ScaleObject()
    {
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            // if any one of touchzero or touchOne is cancelled or maybe ended then do nothing
            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = previewObectToPlace.transform.localScale;
            }
            else // if touch is moved
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                //if accidentally touched or pinch movement is very very small
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return; // do nothing if it can be ignored where inital distance is very close to zero
                }

                var factor = currentDistance / initialDistance;
                previewObectToPlace.transform.localScale = initialScale * factor; // scale multiplied by the factor we calculated

                scaleObject = previewObectToPlace.transform.localScale;
            }
        }
    }

    private void RotateObject()
    {
        if (Input.touchCount == 1 && spawnObject == null)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startingPointRotate = touch.position.x;
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (startingPointRotate > touch.position.x)
                {
                    previewObectToPlace.transform.Rotate(Vector3.up, -180 * Time.deltaTime);
                }
                else if (startingPointRotate < touch.position.x)
                {
                    previewObectToPlace.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
                }
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                startingPointRotate = touch.position.x;
            }
            rotateObject = previewObectToPlace;
        }
    }
}