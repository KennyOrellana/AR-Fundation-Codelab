using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public FurnitureDB furnitureDB;
    public MeshRenderer meshRenderer;
    private int selectedOption = 0; // Start is called before the first frame update

    void Start()
    {
        if (PlayerPrefs.HasKey("selectedOption"))
        {
            Load();
        }
        else
        {
            selectedOption = 0;
        }
        UpdateFurniture(selectedOption);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextOption();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BackOption();
        }
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= furnitureDB.FurnitureCount)
        {
            selectedOption = 0;
        }
        Save();
        UpdateFurniture(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = furnitureDB.FurnitureCount - 1;
        }
        Save();
        UpdateFurniture(selectedOption);
    }

    private void UpdateFurniture(int selectedOption)
    {
        // Furniture furniture = furnitureDB.GetFurniture(selectedOption);
        // meshRenderer. = furniture.furnitureModel;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
}
