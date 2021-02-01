using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public UIDocument menuDoc;
    public UIDocument creditsDoc;
    public MusicManager2 musicManager;
    // Start is called before the first frame update
    private void Start()
    {
        musicManager.PlayPlaylist();
    }

    // Update is called once per frame
    public void StartGame()
    {
        // Load first level
        
    }

    public void LevelSelect()
    {
        // Load Level Select Screen
    }

    public void LoadSandbox()
    {
        // Load sandbox Level
        SceneManager.LoadScene("SandboxLevel");
    }

    public void ShowCredits()
    {
        menuDoc.gameObject.SetActive(false);
        creditsDoc.gameObject.SetActive(true);
    }

    public void HideCredits()
    {
        creditsDoc.gameObject.SetActive(false);
        menuDoc.gameObject.SetActive(true);
    }
}
