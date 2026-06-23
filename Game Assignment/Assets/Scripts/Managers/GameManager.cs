using UnityEngine;
using SlotGame.Core;
using SlotGame.UI;

namespace SlotGame
{
    /// <summary>
    /// Main game controller that initializes and manages the overall game flow.
    /// Acts as the central hub connecting all game systems.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SlotMachine slotMachine;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameInfoDisplay gameInfoDisplay;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            // Singleton pattern - ensure only one GameManager exists
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitializeGame();
        }

        /// <summary>
        /// Initialize all game systems.
        /// </summary>
        private void InitializeGame()
        {
            Debug.Log("🎮 Slot Game Initialized!");
            Debug.Log($"Initial Balance: ${slotMachine.CurrentBalance}");
            Debug.Log("Game Ready - Press Spin to start!");
        }

        /// <summary>
        /// Get the current slot machine instance.
        /// </summary>
        public SlotMachine GetSlotMachine()
        {
            return slotMachine;
        }

        /// <summary>
        /// Pause the game.
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0f;
            Debug.Log("Game Paused");
        }

        /// <summary>
        /// Resume the game.
        /// </summary>
        public void ResumeGame()
        {
            Time.timeScale = 1f;
            Debug.Log("Game Resumed");
        }

        /// <summary>
        /// Check if game is currently paused.
        /// </summary>
        public bool IsGamePaused()
        {
            return Time.timeScale == 0f;
        }
    }
}
