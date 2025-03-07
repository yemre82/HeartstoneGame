using UnityEngine;
using Zenject;
using Assets.Scripts.Players;
using System;

namespace Assets.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public Action<Enemy> OnEnemyCreated;
        public Action<Player> OnPlayerCreated;
        public Action OnGameStarted;


        private EnemyFactory _enemyFactory;
        private PlayerFactory _playerFactory;

        [Inject]
        public void Construct(EnemyFactory enemyFactory, PlayerFactory playerFactory)
        {
            _enemyFactory = enemyFactory;
            _playerFactory = playerFactory;
        }

        public void StartGame()
        {
            Debug.Log("Game Started! Creating Player and Enemy...");

            Player player = _playerFactory.Create();
            Enemy enemy = _enemyFactory.Create();

            OnPlayerCreated?.Invoke(player);
            OnEnemyCreated?.Invoke(enemy);

            OnGameStarted?.Invoke();

            Debug.Log("Player and Enemy Created Successfully.");
        }
    }
}
