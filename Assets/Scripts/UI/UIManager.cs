using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Core;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public TurnManager turnManager;
        public GameObject menuUI;
        public GameObject gameUI;
        public Button startGameButton;

/*************  ✨ Codeium Command ⭐  *************/
        /// <summary>
        /// Listens for the start game button to be clicked and starts the game when it is.
        /// </summary>
/******  bfb2a538-fa9e-4ec0-8d26-a46e6dfdea09  *******/
        void Start()
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        public void StartGame()
        {
            turnManager.StartGame();
            menuUI.SetActive(false);
            gameUI.SetActive(true);
        }
    }
}