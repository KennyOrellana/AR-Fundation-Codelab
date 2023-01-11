/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FurnitureBehaviour : MonoBehaviour
{
    public FurnitureSurfaceManager FurnitureSurfaceManager;
    public ARPlane CurrentPlane;

    private bool fixPosition = false; // flag used to fixed current furniture
    private int activePosition = 0;

    // Start is called before the first frame update
    private void Start()
    {
        // Child = transform.GetChild(0).gameObject;
        refreshUI();
    }

    private void Update()
    {
        if (fixPosition)
        {
            return;
        }

        CurrentPlane = null;

        // Perform raycast using the center of the viewport.
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        FurnitureSurfaceManager.RaycastManager.Raycast(
            screenCenter,
            hits,
            TrackableType.PlaneWithinBounds
        );

        ARRaycastHit? hit = null;
        if (hits.Count > 0)
        {
            // If you don't have a locked plane already...
            var lockedPlane = FurnitureSurfaceManager.LockedPlane;
            hit =
                lockedPlane == null
                    // ... use the first hit in `hits`.
                    ? hits[0]
                    // Otherwise use the locked plane, if it's there.
                    : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }

        if (hit.HasValue)
        {
            CurrentPlane = FurnitureSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);
            transform.position = hit.Value.pose.position;
        }
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }

    public void ToggleFurniture()
    {
        fixPosition = !fixPosition;
        GameObject.Find("ToggleFurniture").GetComponentInChildren<Text>().text = fixPosition
            ? "Lift"
            : "Fix";
    }

    public void BackButton()
    {
        activePosition--;
        if (activePosition < 0)
        {
            activePosition = transform.childCount - 1;
        }
        refreshUI();
        // var rendererObject = GetComponent<Renderer>();
        // rendererObject.material.color = new Color(0.777f, 0.8f, 0.604f);
    }

    public void NextButton()
    {
        activePosition++;
        if (activePosition >= transform.childCount)
        {
            activePosition = 0;
        }
        refreshUI();
        // var rendererObject = GetComponent<Renderer>();
        // rendererObject.material.color = new Color(1f, 0f, 1f);
    }

    private void refreshUI()
    {
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(i == activePosition);
        }
    }

    // 1. Investigar como cambiar el mueble
    // 2. Investigar como cambiar el color o textura
    // 3. Revisar los requerimientos
}
