using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Core;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public TurnManager turnManager;
        public GameObject menuUI;
        public GameObject gameUI;
        public Button startGameButton;

        void Start()
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        public void StartGame()
        {
            turnManager.StartGame();
            menuUI.SetActive(false);
            gameUI.SetActive(true);
        }
    }
}