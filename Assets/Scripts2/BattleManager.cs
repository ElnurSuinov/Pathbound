using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Пример метода, вызываемого при победе в бою
    public void OnBattleVictory()
    {
        // Например, начисляем 5 единиц денег за победу
        var playerResources = GameObject.FindObjectOfType<PlayerResources>();
        if (playerResources != null)
        {
            playerResources.EarnMoney(5);
        }

        // Также можно вызвать альтернативный метод для добавления новой карты в колоду
        // Например: DeckManager.Instance.AddCard(newCard);
    }
}
