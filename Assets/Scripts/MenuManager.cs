using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    // Main menu panel
    [SerializeField]
    GameObject mainPanel;
    // Credits panel
    [SerializeField]
    GameObject creditsPanel;
    // Join screen
    [SerializeField]
    GameObject joinPanel;
    [SerializeField]
    GameObject controlsPanel;
    // Name of next scene
    [SerializeField]
    string nextScene;

    // Go to the join screen
    public void StartButton()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(false);
        joinPanel.SetActive(true);
    }

    // Show the credits panel, hide the main panel
    public void CreditsButton()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ControlsButton()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    // Show the main panel, hide the credits panel
    public void BackButton()
    {
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // Start the game
    public void JoinStartButton()
    {      
         SceneManager.LoadScene(nextScene);
    }

    // Quit the game
    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
