using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsUI : MonoBehaviour
{
    private Button backButton;


    public MainMenu mainMenu;
    // Start is called before the first frame update
    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        backButton = rootVisualElement.Q<Button>("back-button");

        backButton.RegisterCallback<ClickEvent>(ev => mainMenu.HideCredits());

    }
}
