using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Players;
using Assets.Scripts.Core;
using Zenject;
using System;

namespace Assets.Scripts.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("Player UI Elements")]
        public TMP_Text playerHealthText;
        public TMP_Text playerManaText;
        public Image playerHealthBar;
        public Image playerManaBar;

        [Header("Enemy UI Elements")]
        public TMP_Text enemyHealthText;
        public TMP_Text enemyManaText;
        public Image enemyHealthBar;
        public Image enemyManaBar;

        private GameManager gameManager;

        private Player player;
        private Enemy enemy;

        [Inject]
        public void Construct(GameManager _gameManager)
        {
            gameManager = _gameManager;
            gameManager.OnGameStarted += OnGameStarted;
        }

        private void OnGameStarted()
        {
            player = gameManager.player;
            enemy = gameManager.enemy;
            RegisterEvents();
        }

        private void RegisterEvents(){
            player.OnHealthChanged += UpdateHealthUI;
            player.OnManaChanged += UpdateManaUI;
            enemy.OnHealthChanged += UpdateEnemyHealthUI;
            enemy.OnManaChanged += UpdateEnemyManaUI;
            UpdateManaUI(player.mana);
            UpdateHealthUI(player.health);
            UpdateEnemyManaUI(enemy.mana);
            UpdateEnemyHealthUI(enemy.health);
        }

        private void OnDestroy()
        {
            player.OnHealthChanged -= UpdateHealthUI;
            player.OnManaChanged -= UpdateManaUI;
            enemy.OnHealthChanged -= UpdateEnemyHealthUI;
            enemy.OnManaChanged -= UpdateEnemyManaUI;
        }

        private void UpdateEnemyManaUI(int mana)
        {
            enemyManaText.text = $"Mana: {mana}";
            enemyManaBar.fillAmount = (float)mana / enemy.maxMana;
        }

        private void UpdateEnemyHealthUI(int health)
        {
            enemyHealthText.text = $"HP: {health}";
            enemyHealthBar.fillAmount = (float)health / enemy.maxHealth;
        }

        private void UpdateManaUI(int mana)
        {
            playerManaText.text = $"Mana: {mana}";
            playerManaBar.fillAmount = (float)mana / player.maxMana;
        }

        private void UpdateHealthUI(int health)
        {
            playerHealthText.text = $"HP: {health}";
            playerHealthBar.fillAmount = (float)health / player.maxHealth;
        }
    }
}

