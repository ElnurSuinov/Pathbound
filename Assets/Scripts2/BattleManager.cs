using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // ������ ������, ����������� ��� ������ � ���
    public void OnBattleVictory()
    {
        // ��������, ��������� 5 ������ ����� �� ������
        var playerResources = GameObject.FindObjectOfType<PlayerResources>();
        if (playerResources != null)
        {
            playerResources.EarnMoney(5);
        }

        // ����� ����� ������� �������������� ����� ��� ���������� ����� ����� � ������
        // ��������: DeckManager.Instance.AddCard(newCard);
    }
}
