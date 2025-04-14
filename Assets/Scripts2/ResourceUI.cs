using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    public PlayerResources playerResources;
    public Text provisionText;
    public Text moneyText;

    void Update()
    {
        if (playerResources != null)
        {
            provisionText.text = $"Провизия: {playerResources.provision}";
            moneyText.text = $"Деньги: {playerResources.money}";
        }
    }

    public void UpdateUI()
    {
        if (playerResources != null)
        {
            provisionText.text = $"Провизия: {playerResources.provision}";
            moneyText.text = $"Деньги: {playerResources.money}";
        }
    }
}

