using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestSiteActions : MonoBehaviour
{
    public Button RestoreProvisionButton;
    public Button RestoreHPButton;
    public Button UpgradeCardButton;
    public Button ReturnToMapButton;
    public Button QuitGameButton;

    private void Start()
    {
        // Добавляем обработчики для кнопок выбора героя
        RestoreProvisionButton.onClick.AddListener(RestoreProvision);
        RestoreHPButton.onClick.AddListener(RestoreHP);
        UpgradeCardButton.onClick.AddListener(UpgradeCard);

        // Обработчик для кнопки восстановления ресурсов
        ReturnToMapButton.onClick.AddListener(ReturnToMap);

        QuitGameButton.onClick.AddListener(QuitGame);


    }
    public void RestoreProvision()
    {
        var player = FindObjectOfType<PlayerResources>();
        if (player != null)
        {
            int missing = 150 - player.provision;
            int restore = missing <= 50 ? missing : 50;
            player.provision += restore;
        }
        ReturnToMap();
    }

    public void RestoreHP()
    {
        var playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            int missing = playerStats.maxHP - playerStats.currentHP;
            int restore = missing <= 50 ? missing : 50;
            playerStats.currentHP += restore;
        }
        ReturnToMap();
    }

    public void UpgradeCard()
    {
        // Здесь открытие UI для улучшения карты или выполнение логики улучшения
        Debug.Log("Прокачка карты...");
        ReturnToMap();
    }

    public void ReturnToMap()
    {
        SceneManager.LoadScene("MapScene");
    }

    // 📌 ВЫХОД ИЗ ИГРЫ
    public void QuitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();

        // Эта строка поможет увидеть результат в редакторе Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
