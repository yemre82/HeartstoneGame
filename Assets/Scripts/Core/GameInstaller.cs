using UnityEngine;
using Zenject;
using Assets.Scripts.Players;

namespace Assets.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TurnManager turnManager;

        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject playerPrefab;

        public override void InstallBindings()
        {
            if (enemyPrefab == null || playerPrefab == null)
            {
                Debug.LogError("GameInstaller: enemyPrefab or playerPrefab is null");
            }

            Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
            Container.Bind<TurnManager>().FromInstance(turnManager).AsSingle();


            Container.BindFactory<Enemy, EnemyFactory>()
                .FromComponentInNewPrefab(enemyPrefab)
                .AsSingle();

            Container.BindFactory<Player, PlayerFactory>()
                .FromComponentInNewPrefab(playerPrefab)
                .AsSingle();
        }
    }
}
