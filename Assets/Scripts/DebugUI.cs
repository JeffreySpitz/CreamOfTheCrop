using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DebugUI : MonoBehaviour
{
    private Button cornButton;
    private Button flowerButton;
    private Button dirtButton;
    private Button pestButton;
    private Button fireButton;
    private Button saveButton;
    private TextField saveTextField;

    public Game gameController;
    public CellManager cellManager;
    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        cornButton = rootVisualElement.Q<Button>("corn-button");
        flowerButton = rootVisualElement.Q<Button>("flower-button");
        dirtButton = rootVisualElement.Q<Button>("dirt-button");
        pestButton = rootVisualElement.Q<Button>("pest-button");
        fireButton = rootVisualElement.Q<Button>("fire-button");


        saveButton = rootVisualElement.Q<Button>("save-button");
        saveTextField = rootVisualElement.Q<TextField>("save-text-field");

        cornButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Corn));
        flowerButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Flower));
        dirtButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Dirt));
        pestButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Pest));
        fireButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Fire));
        saveButton.RegisterCallback<ClickEvent>(ev => SaveLevel());
    }

    void SetUserCellType(CellType cellType)
    {
        gameController.cellType = cellType;
    }

    void SaveLevel()
    {
        cellManager.SaveLevel(saveTextField.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
