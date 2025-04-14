using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeroSelectionManager : MonoBehaviour
{
    public Button heroButton1;
    public Button heroButton2;
    public Button heroButton3;
    public Button restoreResourcesButton;
    public Button goToMapButton;
    public Button BackButton;
    public Button QuitGameButton;
    public Text moneyText;

    private int money;
    private HashSet<int> purchasedHeroes = new HashSet<int>();

    private void Start()
    {
        money = PlayerPrefs.GetInt("Money", 1500);

        if (PlayerPrefs.GetInt("SelectedHero1", 0) == 1)
        {
            purchasedHeroes.Add(1);
            heroButton1.interactable = false;
        }
        if (PlayerPrefs.GetInt("SelectedHero2", 0) == 1)
        {
            purchasedHeroes.Add(2);
            heroButton2.interactable = false;
        }
        if (PlayerPrefs.GetInt("SelectedHero3", 0) == 1)
        {
            purchasedHeroes.Add(3);
            heroButton3.interactable = false;
        }

        heroButton1.onClick.AddListener(() => TryBuyHero(100, 300, heroButton1));
        heroButton2.onClick.AddListener(() => TryBuyHero(200, 400, heroButton2));
        heroButton3.onClick.AddListener(() => TryBuyHero(300, 500, heroButton3));

        restoreResourcesButton.onClick.AddListener(RestoreResources);
        goToMapButton.onClick.AddListener(GoToMap);
        BackButton.onClick.AddListener(Back);
        QuitGameButton.onClick.AddListener(QuitGame);

        UpdateMoneyUI();
        goToMapButton.interactable = purchasedHeroes.Count > 0;
    }

    private void TryBuyHero(int heroIndex, int cost, Button button)
    {
        if (purchasedHeroes.Contains(heroIndex)) return;
        if (money < cost)
        {
            Debug.Log("Недостаточно монет!");
            return;
        }

        money -= cost;
        purchasedHeroes.Add(heroIndex);
        PlayerPrefs.SetInt("SelectedHero" + heroIndex, 1);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();

        button.interactable = false;
        UpdateMoneyUI();
        goToMapButton.interactable = true;
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Монеты: {money}";
        }
    }

    private void RestoreResources()
    {
        var playerResources = GameObject.FindObjectOfType<PlayerResources>();
        if (playerResources != null)
        {
            if (playerResources.provision <= 0)
            {
                Debug.Log("Невозможно восстановить ресурсы: провизия уже на нуле (Game Over).");
                return;
            }

            int maxProvision = 150;
            int missing = maxProvision - playerResources.provision;

            if (missing == 0)
            {
                Debug.Log("Провизия уже полная.");
                return;
            }

            int amountToRestore = missing <= 4 ? missing : 5;
            playerResources.provision += amountToRestore;

            Debug.Log($"Восстановлено {amountToRestore} провизии. Текущее значение: {playerResources.provision}");
        }
        else
        {
            Debug.LogWarning("PlayerResources не найден!");
        }
    }

    private void GoToMap()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MapScene");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenuScene");
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
