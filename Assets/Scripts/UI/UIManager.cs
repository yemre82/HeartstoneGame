using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Core;
using Assets.Scripts.CardSystem;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        public GameObject menuUI;
        public GameObject gameUI;
        public Button startGameButton;
        public Button pullCardButton;

        void Start()
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        public void StartGame()
        {
            gameManager.StartGame();
            menuUI.SetActive(false);
            gameUI.SetActive(true);
        }

        public void PullCard()
        {
            gameManager.PullCard();
        }
    }
}