﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARPlaceObject : MonoBehaviour
{
    public List<GameObject> objectsToPlace;

    private GameObject placedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

	private void Awake()
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

	// Update is called once per frame
	void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;  
            if (placedObject == null)
            {
                int index = objectsToPlace.Count;
                //rand_index = System.Random(index);

                placedObject = Instantiate(objectsToPlace[index], hitPose.position, hitPose.rotation);
            }
            else
            {
                placedObject.transform.position = hitPose.position;    
            }
        }
    }
}