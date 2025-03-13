using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Players;
using Assets.Scripts.Core;
using Zenject;
using System;

namespace Assets.Scripts.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private GameManager gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void OnGameOver(bool isPlayerWin)
        {
            _text.text = isPlayerWin ? "You Win!" : "You Lose!";
        }

        public void OnRestartButtonClicked()
        {
            gameManager.StartGame();
        }
    }
}