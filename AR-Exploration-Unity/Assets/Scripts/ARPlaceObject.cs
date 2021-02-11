using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject signToPlace;
    //public Camera ArCamera;

    private GameObject placedObject;
    private GameObject placedSign;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

	void Start()
	{
        _arRaycastManager = GetComponent<ARRaycastManager>();
	}

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }
    // wait for 10 seconds. 
    IEnumerator PausePlacement()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 1 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }


	// Update is called once per frame
	void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            var cameraLookAt = GameObject.Find("AR Camera");

            //StartCoroutine(PausePlacement());
            //if (placedObject == null)
            //{
            //    placedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
            //}

            if (placedSign == null || placedObject == null)
            {
                placedSign = Instantiate(signToPlace, hitPose.position, hitPose.rotation);
                placedSign.transform.position = hitPose.position;

                placedObject = Instantiate(objectToPlace, hitPose.position, cameraLookAt.transform.rotation);
                placedObject.transform.position = hitPose.position;
                //yield return new WaitForSeconds(1);
            }
            else
            {
                //placedObject = Instantiate(objectToPlace, hitPose.position, cameraLookAt.transform.rotation);
                //placedObject.transform.position = hitPose.position;
                //yield StartCoroutine(PausePlacement());
                //yield waitforseconds(1f);
                //wait
                Debug.Log("Placed object.");
                //yield return StartCoroutine(PausePlacement());
            }
        }
    }
}
