using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public int provision = 150;
    public int money = 1500;

    void Start()
    {
        money = PlayerPrefs.GetInt("Money", 1500);
    }

    public bool CanTravel(int provisionCost)
    {
        return provision >= provisionCost;
    }

    public void Consume(int provisionCost)
    {
        provision -= provisionCost;
        provision = Mathf.Max(provision, 0);
    }

    public void Buy(int moneyCost)
    {
        money -= moneyCost;
    }

    public void EarnMoney(int amount)
    {
        money += amount;
        Debug.Log("Получено денег: " + amount + ". Всего денег: " + money);
    }
}
