using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Core;
using TMPro;
using Zenject;
using System;

namespace Assets.Scripts.UI
{
    public class TurnUI : MonoBehaviour
    {
        private GameManager gameManager;

        [SerializeField] private TMP_Text turnTimerText;
        [SerializeField] private TMP_Text turnStateText;


        [Inject]
        public void Construct(GameManager _gameManager)
        {
            gameManager = _gameManager;
            gameManager.turnManager.OnTurnTimeChange += UpdateTimeLeft;
            gameManager.turnManager.OnGameStateChange += UpdateGameState;
        }

        private void UpdateGameState(GameState state)
        {
            if (state == GameState.PlayerTurn){
                turnStateText.text = "Your Turn!!";
            }
            else if (state == GameState.EnemyTurn){
                turnStateText.text = "Enemy Turn!!";
            }
            else{
                turnStateText.text = "";
            }
        }

        private void UpdateTimeLeft(float timeLeft)
        {
            turnTimerText.text = timeLeft.ToString();
        }

        void OnDestroy()
        {
            gameManager.turnManager.OnTurnTimeChange -= UpdateTimeLeft;
            gameManager.turnManager.OnGameStateChange -= UpdateGameState;
        }
    }
}