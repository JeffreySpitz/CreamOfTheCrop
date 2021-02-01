using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : MonoBehaviour
{
    private Button startButton;
    private Button levelButton;
    private Button sandboxButton;
    private Button creditsButton;


    public MainMenu mainMenu;
    // Start is called before the first frame update
    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        startButton = rootVisualElement.Q<Button>("start-button");
        levelButton = rootVisualElement.Q<Button>("level-select-button");
        sandboxButton = rootVisualElement.Q<Button>("sandbox-button");
        creditsButton = rootVisualElement.Q<Button>("credits-button");

        startButton.RegisterCallback<ClickEvent>(ev => StartGame());
        levelButton.RegisterCallback<ClickEvent>(ev => LevelSelect());
        sandboxButton.RegisterCallback<ClickEvent>(ev => LoadSandbox());
        creditsButton.RegisterCallback<ClickEvent>(ev => ShowCredits());

    }

    // Update is called once per frame
    private void StartGame()
    {
        // Load first level
        mainMenu.StartGame();
    }

    private void LevelSelect()
    {
        // Load Level Select Screen
        mainMenu.LevelSelect();
    }

    private void LoadSandbox()
    {
        // Load sandbox Level
        mainMenu.LoadSandbox();
    }

    private void ShowCredits()
    {
        mainMenu.ShowCredits();
    }
}
