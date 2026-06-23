using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SlotGame.Core
{
    /// <summary>
    /// Main slot machine controller.
    /// Manages the game flow, reels, winning logic, and payouts.
    /// </summary>
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private int numberOfReels = 3;
        [SerializeField] private float spinDuration = 2f;
        [SerializeField] private float spinDelayBetweenReels = 0.3f;
        [SerializeField] private int initialBalance = 1000;
        [SerializeField] private int costPerSpin = 10;

        private List<Reel> _reels = new();
        private GameRNG _rng;
        private int _currentBalance;
        private int _lastWinnings;
        private bool _canSpin;
        private bool _isSpinning;

        // Prefab references
        private GameObject _reelPrefab;
        private GameObject _symbolPrefab;

        // Events for UI updates
        public delegate void BalanceChangedDelegate(int newBalance);
        public delegate void SpinCompleteDelegate(bool isWin, int winnings);
        public delegate void GameStateChangedDelegate(bool canSpin);

        public event BalanceChangedDelegate OnBalanceChanged;
        public event SpinCompleteDelegate OnSpinComplete;
        public event GameStateChangedDelegate OnGameStateChanged;

        public int CurrentBalance => _currentBalance;
        public int LastWinnings => _lastWinnings;
        public bool IsSpinning => _isSpinning;
        public bool CanSpin => _canSpin;

        private void Start()
        {
            InitializeGame();
        }

        /// <summary>
        /// Initialize the slot machine game.
        /// </summary>
        private void InitializeGame()
        {
            _rng = new GameRNG();
            _currentBalance = initialBalance;
            _canSpin = true;
            _isSpinning = false;

            // Create reels
            CreateReels();

            OnBalanceChanged?.Invoke(_currentBalance);
            OnGameStateChanged?.Invoke(_canSpin);
        }

        /// <summary>
        /// Create the reel objects for the slot machine.
        /// </summary>
        private void CreateReels()
        {
            // Destroy existing reels
            foreach (var reel in _reels)
            {
                if (reel != null)
                {
                    Destroy(reel.gameObject);
                }
            }
            _reels.Clear();

            // Create new reels in a horizontal layout
            for (int i = 0; i < numberOfReels; i++)
            {
                GameObject reelObject = new GameObject($"Reel_{i}");
                reelObject.transform.SetParent(transform);
                reelObject.transform.localPosition = new Vector3(i * 150f, 0, 0);

                // Create reel container
                GameObject reelContent = new GameObject("Content");
                reelContent.transform.SetParent(reelObject.transform);
                reelContent.transform.localPosition = Vector3.zero;

                var rectTransform = reelContent.AddComponent<RectTransform>();
                rectTransform.anchoredPosition = Vector3.zero;
                rectTransform.sizeDelta = new Vector2(100, 300);

                // Create and initialize Reel component
                var reel = reelObject.AddComponent<Reel>();
                var reelScript = reelObject.GetComponent<Reel>();

                // Use reflection or a setter method to initialize
                // For now, we'll initialize through Start or a public method
                _reels.Add(reel);
            }

            // Initialize all reels
            foreach (var reel in _reels)
            {
                reel.Initialize(_rng, _symbolPrefab);
            }
        }

        /// <summary>
        /// Attempt to spin the slot machine.
        /// </summary>
        public bool RequestSpin()
        {
            // Check if can spin
            if (!_canSpin || _isSpinning)
            {
                Debug.LogWarning("Cannot spin: Game is not in a valid state");
                return false;
            }

            // Check if player has enough balance
            if (_currentBalance < costPerSpin)
            {
                Debug.LogWarning($"Insufficient balance. Need {costPerSpin}, have {_currentBalance}");
                return false;
            }

            // Deduct spin cost
            _currentBalance -= costPerSpin;
            OnBalanceChanged?.Invoke(_currentBalance);

            // Start spinning
            StartCoroutine(PerformSpin());

            return true;
        }

        /// <summary>
        /// Coroutine to handle the complete spin sequence.
        /// </summary>
        private IEnumerator PerformSpin()
        {
            _isSpinning = true;
            _canSpin = false;
            OnGameStateChanged?.Invoke(_canSpin);

            // Spin each reel with a staggered delay
            for (int i = 0; i < _reels.Count; i++)
            {
                StartCoroutine(_reels[i].SpinReel(spinDuration));
                yield return new WaitForSeconds(spinDelayBetweenReels);
            }

            // Wait for all reels to finish spinning
            yield return new WaitForSeconds(spinDuration + (spinDelayBetweenReels * numberOfReels));

            // Wait a moment before checking results
            yield return new WaitForSeconds(0.5f);

            // Check for win and apply payout
            bool isWin = CheckForWin();
            if (isWin)
            {
                _lastWinnings = CalculateWinnings();
                _currentBalance += _lastWinnings;
                OnBalanceChanged?.Invoke(_currentBalance);
            }
            else
            {
                _lastWinnings = 0;
            }

            // Allow spinning again
            _isSpinning = false;
            _canSpin = true;
            OnGameStateChanged?.Invoke(_canSpin);

            // Fire completion event
            OnSpinComplete?.Invoke(isWin, _lastWinnings);
        }

        /// <summary>
        /// Check if the current reel combination is a winning combination.
        /// </summary>
        private bool CheckForWin()
        {
            if (_reels.Count < 2)
                return false;

            // All reels must show the same symbol
            Symbol firstSymbol = _reels[0].CurrentSymbol;

            foreach (var reel in _reels)
            {
                if (reel.CurrentSymbol.Type != firstSymbol.Type)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calculate the winnings based on the current symbols.
        /// </summary>
        private int CalculateWinnings()
        {
            if (_reels.Count == 0)
                return 0;

            Symbol symbol = _reels[0].CurrentSymbol;
            int baseMultiplier = symbol.PayoutMultiplier;

            // Bonus multiplier based on number of reels matching
            int bonusMultiplier = _reels.Count;

            // Total payout = (symbol payout * number of reels) * spin cost
            int totalPayout = baseMultiplier * bonusMultiplier * costPerSpin;

            return totalPayout;
        }

        /// <summary>
        /// Get information about the current reel symbols.
        /// </summary>
        public List<SymbolType> GetCurrentSymbols()
        {
            List<SymbolType> symbols = new();
            foreach (var reel in _reels)
            {
                symbols.Add(reel.CurrentSymbol.Type);
            }
            return symbols;
        }

        /// <summary>
        /// Force a specific combination for testing/development.
        /// </summary>
        public void TestSetWinningCombination()
        {
            StartCoroutine(SpinWithWinningResult());
        }

        private IEnumerator SpinWithWinningResult()
        {
            _isSpinning = true;
            _canSpin = false;

            // Deduct spin cost
            _currentBalance -= costPerSpin;
            OnBalanceChanged?.Invoke(_currentBalance);

            // Generate a winning symbol
            SymbolType winningSymbol = _rng.GenerateWinningSymbol();
            Symbol symbol = new Symbol(winningSymbol);

            // Spin each reel with the winning symbol
            for (int i = 0; i < _reels.Count; i++)
            {
                StartCoroutine(_reels[i].SpinReelWithResult(spinDuration, symbol));
                yield return new WaitForSeconds(spinDelayBetweenReels);
            }

            yield return new WaitForSeconds(spinDuration + (spinDelayBetweenReels * numberOfReels));
            yield return new WaitForSeconds(0.5f);

            // Calculate and apply winnings
            _lastWinnings = CalculateWinnings();
            _currentBalance += _lastWinnings;
            OnBalanceChanged?.Invoke(_currentBalance);

            _isSpinning = false;
            _canSpin = true;
            OnGameStateChanged?.Invoke(_canSpin);

            OnSpinComplete?.Invoke(true, _lastWinnings);
        }

        /// <summary>
        /// Add balance to player (for purchasing more credits).
        /// </summary>
        public void AddBalance(int amount)
        {
            if (amount > 0)
            {
                _currentBalance += amount;
                OnBalanceChanged?.Invoke(_currentBalance);
            }
        }

        /// <summary>
        /// Reset the game to initial state.
        /// </summary>
        public void ResetGame()
        {
            _currentBalance = initialBalance;
            _lastWinnings = 0;
            _isSpinning = false;
            _canSpin = true;
            OnBalanceChanged?.Invoke(_currentBalance);
            OnGameStateChanged?.Invoke(_canSpin);
        }
    }
}
