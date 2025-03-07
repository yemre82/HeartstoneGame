using UnityEngine;
using Zenject;
using Assets.Scripts.Players;
using System;

namespace Assets.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public Action<Enemy, Player> OnUsersCreated;
        public Action OnGameStarted;


        public Enemy enemy;
        public Player player;

        private EnemyFactory enemyFactory;
        private PlayerFactory playerFactory;

        [Inject]
        public void Construct(EnemyFactory enemyFactory, PlayerFactory playerFactory)
        {
            this.enemyFactory = enemyFactory;
            this.playerFactory = playerFactory;
        }

        public void StartGame()
        {
            Debug.Log("Game Started! Creating Player and Enemy...");

            player = playerFactory.Create();
            enemy = enemyFactory.Create();

            OnUsersCreated?.Invoke(enemy, player);

            OnGameStarted?.Invoke();

            Debug.Log("Player and Enemy Created Successfully.");
        }
    }
}
