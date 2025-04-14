using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    public Button StartGameButon;
    public Button QuitGameButton;

    public void Start()
    {
        StartGameButon.onClick.AddListener(StartGame);
        QuitGameButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Money", 1500);
        PlayerPrefs.DeleteKey("SelectedHero1");
        PlayerPrefs.DeleteKey("SelectedHero2");
        PlayerPrefs.DeleteKey("SelectedHero3");
        PlayerPrefs.Save();
        SceneManager.LoadScene("HeroSelection");
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}