using UnityEngine;
using UnityEngine.UI;
using SlotGame.Core;

namespace SlotGame.UI
{
    /// <summary>
    /// Displays information about the current game state and winning conditions.
    /// Shows reel values, probabilities, and game statistics.
    /// </summary>
    public class GameInfoDisplay : MonoBehaviour
    {
        [SerializeField] private SlotMachine slotMachine;
        [SerializeField] private Text symbolInfoText;
        [SerializeField] private Text probabilityInfoText;
        [SerializeField] private Text statisticsText;

        private GameRNG _rng;
        private int _totalSpins;
        private int _totalWins;
        private int _totalWinnings;

        private void Start()
        {
            _rng = new GameRNG();
            DisplaySymbolProbabilities();
            UpdateStatistics();
        }

        private void OnEnable()
        {
            if (slotMachine != null)
            {
                slotMachine.OnSpinComplete += OnSpinCompleted;
            }
        }

        private void OnDisable()
        {
            if (slotMachine != null)
            {
                slotMachine.OnSpinComplete -= OnSpinCompleted;
            }
        }

        /// <summary>
        /// Display the current symbols on all reels.
        /// </summary>
        private void DisplayCurrentSymbols()
        {
            if (symbolInfoText == null || slotMachine == null)
                return;

            var symbols = slotMachine.GetCurrentSymbols();
            string symbolInfo = "Current Reels: ";

            foreach (var symbol in symbols)
            {
                symbolInfo += symbol.ToString() + " | ";
            }

            symbolInfoText.text = symbolInfo;
        }

        /// <summary>
        /// Display the probability distribution of all symbols.
        /// </summary>
        private void DisplaySymbolProbabilities()
        {
            if (probabilityInfoText == null)
                return;

            string probabilityInfo = "Symbol Probabilities:\n";
            probabilityInfo += "Cherry: " + _rng.GetSymbolProbability(SymbolType.Cherry).ToString("F1") + "%\n";
            probabilityInfo += "Lemon: " + _rng.GetSymbolProbability(SymbolType.Lemon).ToString("F1") + "%\n";
            probabilityInfo += "Orange: " + _rng.GetSymbolProbability(SymbolType.Orange).ToString("F1") + "%\n";
            probabilityInfo += "Plum: " + _rng.GetSymbolProbability(SymbolType.Plum).ToString("F1") + "%\n";
            probabilityInfo += "Bell: " + _rng.GetSymbolProbability(SymbolType.Bell).ToString("F1") + "%\n";
            probabilityInfo += "Bar: " + _rng.GetSymbolProbability(SymbolType.Bar).ToString("F1") + "%\n";
            probabilityInfo += "Seven: " + _rng.GetSymbolProbability(SymbolType.Seven).ToString("F1") + "%\n";
            probabilityInfo += "Diamond: " + _rng.GetSymbolProbability(SymbolType.Diamond).ToString("F1") + "%";

            probabilityInfoText.text = probabilityInfo;
        }

        /// <summary>
        /// Update game statistics display.
        /// </summary>
        private void UpdateStatistics()
        {
            if (statisticsText == null)
                return;

            float winRate = _totalSpins > 0 ? (_totalWins / (float)_totalSpins) * 100f : 0f;
            string stats = $"Total Spins: {_totalSpins}\n";
            stats += $"Total Wins: {_totalWins}\n";
            stats += $"Win Rate: {winRate:F1}%\n";
            stats += $"Total Winnings: ${_totalWinnings}";

            statisticsText.text = stats;
        }

        /// <summary>
        /// Called when a spin completes.
        /// </summary>
        private void OnSpinCompleted(bool isWin, int winnings)
        {
            _totalSpins++;

            if (isWin)
            {
                _totalWins++;
                _totalWinnings += winnings;
            }

            DisplayCurrentSymbols();
            UpdateStatistics();
        }
    }
}
