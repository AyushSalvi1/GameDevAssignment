using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SlotGame.Core
{
    /// <summary>
    /// Manages a single reel's spinning animation and symbol display.
    /// Handles the visual representation and animation of a slot machine reel.
    /// </summary>
    public class Reel : MonoBehaviour
    {
        [SerializeField] private RectTransform reelContent; // The container for symbols
        [SerializeField] private float spinDuration = 0.5f; // Duration of spin animation
        [SerializeField] private float spinSpeed = 2000f; // Speed of spinning motion
        [SerializeField] private int symbolsPerReel = 3; // Number of visible symbols

        private Symbol _currentSymbol;
        private GameRNG _rng;
        private bool _isSpinning;
        private Coroutine _spinCoroutine;

        // Symbol prefab will be assigned from SlotMachine manager
        private GameObject _symbolPrefab;

        public Symbol CurrentSymbol => _currentSymbol;
        public bool IsSpinning => _isSpinning;

        /// <summary>
        /// Initialize the reel with necessary references.
        /// </summary>
        public void Initialize(GameRNG rng, GameObject symbolPrefab)
        {
            _rng = rng;
            _symbolPrefab = symbolPrefab;

            // Set initial symbol
            SetRandomSymbol();
        }

        /// <summary>
        /// Set the reel to display a random symbol.
        /// </summary>
        private void SetRandomSymbol()
        {
            SymbolType symbolType = _rng.GenerateRandomSymbol();
            _currentSymbol = new Symbol(symbolType);
            DisplaySymbol(_currentSymbol);
        }

        /// <summary>
        /// Set the reel to display a specific symbol.
        /// </summary>
        public void SetSymbol(Symbol symbol)
        {
            _currentSymbol = symbol;
            DisplaySymbol(symbol);
        }

        /// <summary>
        /// Display a symbol on the reel.
        /// </summary>
        private void DisplaySymbol(Symbol symbol)
        {
            // Clear existing symbols
            foreach (Transform child in reelContent)
            {
                Destroy(child.gameObject);
            }

            // Create text display for the symbol (placeholder)
            if (reelContent != null)
            {
                var textObj = new GameObject($"Symbol_{symbol.Type}");
                textObj.transform.SetParent(reelContent, false);
                
                var text = textObj.AddComponent<TextMesh>();
                text.text = symbol.GetSymbolName();
                text.anchor = TextAnchor.MiddleCenter;
                text.alignment = TextAlignment.Center;
                text.fontSize = 40;
            }
        }

        /// <summary>
        /// Spin the reel and land on a random symbol.
        /// </summary>
        /// <param name="duration">How long the spin should last</param>
        public IEnumerator SpinReel(float duration)
        {
            if (_isSpinning) yield break;

            _isSpinning = true;
            float elapsedTime = 0f;

            // Animate spinning
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // Rotate the reel during spin
                if (reelContent != null)
                {
                    reelContent.Rotate(0, 0, spinSpeed * Time.deltaTime);
                }

                yield return null;
            }

            // Set final random symbol
            SetRandomSymbol();

            // Reset rotation to clean state
            if (reelContent != null)
            {
                reelContent.rotation = Quaternion.identity;
            }

            _isSpinning = false;
        }

        /// <summary>
        /// Spin the reel and land on a specific symbol.
        /// Used for testing winning combinations.
        /// </summary>
        public IEnumerator SpinReelWithResult(float duration, Symbol targetSymbol)
        {
            if (_isSpinning) yield break;

            _isSpinning = true;
            float elapsedTime = 0f;

            // Animate spinning
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                if (reelContent != null)
                {
                    reelContent.Rotate(0, 0, spinSpeed * Time.deltaTime);
                }

                yield return null;
            }

            // Set the target symbol
            SetSymbol(targetSymbol);

            if (reelContent != null)
            {
                reelContent.rotation = Quaternion.identity;
            }

            _isSpinning = false;
        }

        /// <summary>
        /// Stop spinning immediately and display the current symbol.
        /// </summary>
        public void StopSpinning()
        {
            if (_spinCoroutine != null)
            {
                StopCoroutine(_spinCoroutine);
            }

            _isSpinning = false;

            if (reelContent != null)
            {
                reelContent.rotation = Quaternion.identity;
            }

            DisplaySymbol(_currentSymbol);
        }
    }
}
