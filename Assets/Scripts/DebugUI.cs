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
    private Button cactusButton;
    private Button voidButton;
    private Button sandButton;
    private Button rockButton;
    private Button barnButton;
    private Button pauseSimButton;


    private Button pauseButton;
    private Button saveButton;
    private Button loadButton;
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
        cactusButton = rootVisualElement.Q<Button>("cactus-button");
        voidButton = rootVisualElement.Q<Button>("void-button");
        sandButton = rootVisualElement.Q<Button>("sand-button");
        rockButton = rootVisualElement.Q<Button>("rock-button");
        barnButton = rootVisualElement.Q<Button>("barn-button");
        pauseSimButton = rootVisualElement.Q<Button>("pause-sim-button");


        pauseButton = rootVisualElement.Q<Button>("pause-button");
        saveButton = rootVisualElement.Q<Button>("save-button");
        loadButton = rootVisualElement.Q<Button>("load-button");
        saveTextField = rootVisualElement.Q<TextField>("save-text-field");

        cornButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Corn));
        flowerButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Flower));
        dirtButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Dirt));
        pestButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Pest));
        fireButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Fire));
        cactusButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Cactus));
        voidButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Void));
        sandButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Sand));
        rockButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Rock));
        barnButton.RegisterCallback<ClickEvent>(ev => SetUserCellType(CellType.Barn));
        pauseSimButton.RegisterCallback<ClickEvent>(ev => PlayPauseSim());


        pauseButton.RegisterCallback<ClickEvent>(ev => PauseLevel());
        saveButton.RegisterCallback<ClickEvent>(ev => SaveLevel());
        loadButton.RegisterCallback<ClickEvent>(ev => LoadLevel());
    }

    void PlayPauseSim()
    {
        cellManager.gameIsPaused = !cellManager.gameIsPaused;
    }

    void SetUserCellType(CellType cellType)
    {
        gameController.cellType = cellType;
    }

    void SaveLevel()
    {
        cellManager.SaveLevel(saveTextField.text);
    }

    void LoadLevel()
    {
        cellManager.LoadLevel(saveTextField.text);
    }

    void PauseLevel()
    {
        gameController.PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
