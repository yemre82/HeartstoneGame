using UnityEngine;
using Zenject;
using Assets.Scripts.Players;

namespace Assets.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject playerPrefab;

        public override void InstallBindings()
        {
            if (enemyPrefab == null || playerPrefab == null)
            {
                Debug.LogError("GameInstaller: enemyPrefab or playerPrefab is null");
            }


            Container.BindFactory<Enemy, EnemyFactory>()
                .FromComponentInNewPrefab(enemyPrefab)
                .AsSingle();

            Container.BindFactory<Player, PlayerFactory>()
                .FromComponentInNewPrefab(playerPrefab)
                .AsSingle();
        }
    }
}
