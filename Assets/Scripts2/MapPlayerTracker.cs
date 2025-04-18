using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;
        public Text gameOverText;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            if (mapManager.CurrentMap.path.Count == 0)
            {
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                Vector2Int currentPoint = mapManager.CurrentMap.path.Last();
                Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence()
                .AppendInterval(enterNodeDelay)
                .OnComplete(() => EnterNode(mapNode));
        }

        private void EnterNode(MapNode mapNode)
        {
            Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);

            // Вычисляем стоимость провизии в зависимости от типа узла
            int provisionCost = GetProvisionCost(mapNode.Node.nodeType);

            var playerResources = GameObject.FindObjectOfType<PlayerResources>();
            var resourcesUI = GameObject.FindObjectOfType<ResourcesUI>();

            if (playerResources == null || playerResources.provision < provisionCost)
            {
                Debug.Log($"Не хватает провизии: требуется {provisionCost}, осталось {playerResources?.provision}");
                GameOver();
                return;
            }

            playerResources.Consume(provisionCost);
            Debug.Log($"Потрачено: Провизия {provisionCost}");

            if (resourcesUI != null)
                resourcesUI.UpdateUI();

            // Просто логируем событие для типа узла
            switch (mapNode.Node.nodeType)
            {
                case NodeType.MinorEnemy:
                    Debug.Log("Бой с обычным врагом.");
                    break;
                case NodeType.EliteEnemy:
                    Debug.Log("Бой с элитным врагом.");
                    break;
                case NodeType.RestSite:
                    Debug.Log("Переход к сцене отдыха.");
                    UnityEngine.SceneManagement.SceneManager.LoadScene("RestSiteScene");
                    return;
                case NodeType.Treasure:
                    Debug.Log("Найдено сокровище.");
                    break;
                case NodeType.Store:
                    Debug.Log("Посещение магазина.");
                    UnityEngine.SceneManagement.SceneManager.LoadScene("HeroSelection");
                    break;
                case NodeType.Boss:
                    Debug.Log("Бой с боссом.");
                    break;
                case NodeType.Mystery:
                    Debug.Log("Таинственное событие.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Locked = false;
        }

        private int GetProvisionCost(NodeType type)
        {
            switch (type)
            {
                case NodeType.MinorEnemy:
                    return UnityEngine.Random.Range(30, 50); // 1–2
                case NodeType.EliteEnemy:
                    return UnityEngine.Random.Range(10, 30); // 3–4
                case NodeType.Boss:
                    return UnityEngine.Random.Range(50, 70); // 5–6
                default:
                    return UnityEngine.Random.Range(10, 40); // 1–5 по умолчанию
            }
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Этот узел недоступен.");
        }

        public void GameOver()
        {
            Debug.Log("Game Over!");
            if (gameOverText != null)
            {
                gameOverText.enabled = true;
            }
            Time.timeScale = 0f;
        }
    }
}

