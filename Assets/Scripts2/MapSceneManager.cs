using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Map
{
    public class MapSceneManager : MonoBehaviour
    {
        public Button BackButton;
        public Button QuitGameButton;
        public void Start()
        {
            BackButton.onClick.AddListener(BackToMainMenu);
            QuitGameButton.onClick.AddListener(QuitGame);
        }

        public void BackToMainMenu()
        {
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
}
