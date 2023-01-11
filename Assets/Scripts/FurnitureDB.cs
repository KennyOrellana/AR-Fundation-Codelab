using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FurnitureDB : ScriptableObject
{
    public Furniture[] furnitures;

    public int FurnitureCount
    {
        get { return furnitures.Length; }
    }

    public Furniture GetFurniture(int index)
    {
        return furnitures[index];
    }
}
