using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private Button resumeButton;
    private Button mainmenuButton;

    public Game gameController;
    // Start is called before the first frame update
    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        resumeButton = rootVisualElement.Q<Button>("resume-button");
        mainmenuButton = rootVisualElement.Q<Button>("main-menu-button");

        resumeButton.RegisterCallback<ClickEvent>(ev => Resume());
        mainmenuButton.RegisterCallback<ClickEvent>(ev => MainMenu());
    }

    // Update is called once per frame
    private void Resume()
    {
        gameController.ResumeGame();
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
