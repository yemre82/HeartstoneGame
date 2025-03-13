using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Core;
using Assets.Scripts.CardSystem;
using System;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        public GameObject menuUI;
        public GameObject gameUI;
        public Button startGameButton;
        public Button pullCardButton;

        public GameOverUI gameOverUI;

        void Start()
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        public void StartGame()
        {
            gameManager.StartGame();
            this.gameManager.OnGameOver += OnGameOver;
            menuUI.SetActive(false);
            gameUI.SetActive(true);
        }

        private void OnGameOver(bool isPlayerWin)
        {
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.OnGameOver(isPlayerWin);
        }

        public void PullCard()
        {
            gameManager.PullCard();
        }

        void OnDestroy()
        {
            gameManager.OnGameOver -= OnGameOver;
        }
    }
}