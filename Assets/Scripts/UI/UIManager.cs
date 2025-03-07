using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Core;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        public GameObject menuUI;
        public GameObject gameUI;
        public Button startGameButton;

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
    }
}