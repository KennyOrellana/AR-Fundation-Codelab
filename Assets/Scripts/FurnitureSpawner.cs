using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    public GameObject FurniturePrefab;

    public FurnitureBehaviour Furniture;
    public FurnitureSurfaceManager FurnitureSurfaceManager;

    public FurnitureBehaviour FixedFurniture;

    private void Update()
    {
        if (FixedFurniture == null && WasTapped() && Furniture.CurrentPlane != null)
        {
            // Spawn our car at the reticle location.
            var obj = GameObject.Instantiate(FurniturePrefab);
            FixedFurniture = obj.GetComponent<FurnitureBehaviour>();
            // Furniture.Reticle = Reticle;
            FixedFurniture.transform.position = Furniture.transform.position;
            FurnitureSurfaceManager.LockPlane(FixedFurniture.CurrentPlane);
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
}
