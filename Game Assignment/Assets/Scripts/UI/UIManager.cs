using UnityEngine;
using UnityEngine.UI;
using SlotGame.Core;

namespace SlotGame.UI
{
    /// <summary>
    /// Manages the UI for the slot machine game.
    /// Handles display updates, button interactions, and game state reflection.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private SlotMachine slotMachine;
        [SerializeField] private Text balanceText;
        [SerializeField] private Text winningsText;
        [SerializeField] private Text statusText;
        [SerializeField] private Button spinButton;
        [SerializeField] private Button addBalanceButton;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button testWinButton;
        [SerializeField] private int addBalanceAmount = 100;

        [SerializeField] private Color winColor = Color.green;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color loseColor = Color.red;

        private void OnEnable()
        {
            // Subscribe to slot machine events
            if (slotMachine != null)
            {
                slotMachine.OnBalanceChanged += UpdateBalanceDisplay;
                slotMachine.OnSpinComplete += OnSpinComplete;
                slotMachine.OnGameStateChanged += UpdateButtonStates;
            }

            // Subscribe to button clicks
            if (spinButton != null)
                spinButton.onClick.AddListener(OnSpinButtonPressed);
            if (addBalanceButton != null)
                addBalanceButton.onClick.AddListener(OnAddBalancePressed);
            if (resetButton != null)
                resetButton.onClick.AddListener(OnResetPressed);
            if (testWinButton != null)
                testWinButton.onClick.AddListener(OnTestWinPressed);
        }

        private void OnDisable()
        {
            // Unsubscribe from slot machine events
            if (slotMachine != null)
            {
                slotMachine.OnBalanceChanged -= UpdateBalanceDisplay;
                slotMachine.OnSpinComplete -= OnSpinComplete;
                slotMachine.OnGameStateChanged -= UpdateButtonStates;
            }

            // Unsubscribe from button clicks
            if (spinButton != null)
                spinButton.onClick.RemoveListener(OnSpinButtonPressed);
            if (addBalanceButton != null)
                addBalanceButton.onClick.RemoveListener(OnAddBalancePressed);
            if (resetButton != null)
                resetButton.onClick.RemoveListener(OnResetPressed);
            if (testWinButton != null)
                testWinButton.onClick.RemoveListener(OnTestWinPressed);
        }

        private void Start()
        {
            // Initialize UI displays
            UpdateBalanceDisplay(slotMachine.CurrentBalance);
            UpdateButtonStates(slotMachine.CanSpin);
        }

        /// <summary>
        /// Update the balance display text.
        /// </summary>
        private void UpdateBalanceDisplay(int newBalance)
        {
            if (balanceText != null)
            {
                balanceText.text = $"Balance: ${newBalance}";
            }
        }

        /// <summary>
        /// Update button availability based on game state.
        /// </summary>
        private void UpdateButtonStates(bool canSpin)
        {
            if (spinButton != null)
            {
                spinButton.interactable = canSpin && slotMachine.CurrentBalance >= 10; // Assuming 10 cost per spin
            }
        }

        /// <summary>
        /// Handle spin button press.
        /// </summary>
        private void OnSpinButtonPressed()
        {
            if (slotMachine.RequestSpin())
            {
                statusText.text = "Spinning...";
                statusText.color = normalColor;
            }
            else
            {
                statusText.text = "Insufficient balance!";
                statusText.color = loseColor;
            }
        }

        /// <summary>
        /// Handle completion of a spin.
        /// </summary>
        private void OnSpinComplete(bool isWin, int winnings)
        {
            if (isWin)
            {
                statusText.text = $"WIN! +${winnings}";
                statusText.color = winColor;
                if (winningsText != null)
                    winningsText.text = $"Last Win: ${winnings}";
            }
            else
            {
                statusText.text = "No win this time!";
                statusText.color = loseColor;
                if (winningsText != null)
                    winningsText.text = "Last Win: $0";
            }
        }

        /// <summary>
        /// Handle add balance button press.
        /// </summary>
        private void OnAddBalancePressed()
        {
            slotMachine.AddBalance(addBalanceAmount);
            statusText.text = $"+${addBalanceAmount} added!";
            statusText.color = normalColor;
        }

        /// <summary>
        /// Handle reset button press.
        /// </summary>
        private void OnResetPressed()
        {
            slotMachine.ResetGame();
            statusText.text = "Game reset!";
            statusText.color = normalColor;
            if (winningsText != null)
                winningsText.text = "Last Win: $0";
        }

        /// <summary>
        /// Handle test win button press (for development/testing).
        /// </summary>
        private void OnTestWinPressed()
        {
            slotMachine.TestSetWinningCombination();
            statusText.text = "Test win initiated...";
            statusText.color = normalColor;
        }

        /// <summary>
        /// Display game over message.
        /// </summary>
        public void ShowGameOver()
        {
            statusText.text = "Game Over! No more balance.";
            statusText.color = loseColor;
        }

        /// <summary>
        /// Clear all status messages.
        /// </summary>
        public void ClearStatus()
        {
            statusText.text = "";
        }
    }
}
