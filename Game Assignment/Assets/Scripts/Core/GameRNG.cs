using UnityEngine;
using System.Collections.Generic;

namespace SlotGame.Core
{
    /// <summary>
    /// Manages random number generation for the slot machine.
    /// Ensures fairness by controlling the probability of each symbol.
    /// </summary>
    public class GameRNG
    {
        // Probability weights for each symbol (determines fairness and distribution)
        private readonly Dictionary<SymbolType, int> _symbolWeights = new()
        {
            { SymbolType.Cherry, 20 },    // 20% chance
            { SymbolType.Lemon, 15 },     // 15% chance
            { SymbolType.Orange, 15 },    // 15% chance
            { SymbolType.Plum, 12 },      // 12% chance
            { SymbolType.Bell, 12 },      // 12% chance
            { SymbolType.Bar, 10 },       // 10% chance
            { SymbolType.Seven, 4 },      // 4% chance (rare)
            { SymbolType.Diamond, 2 }     // 2% chance (very rare)
        };

        private int _totalWeight;

        public GameRNG()
        {
            // Calculate total weight for weighted random selection
            _totalWeight = 0;
            foreach (var weight in _symbolWeights.Values)
            {
                _totalWeight += weight;
            }
        }

        /// <summary>
        /// Generate a random symbol based on weighted probabilities.
        /// </summary>
        /// <returns>A random SymbolType</returns>
        public SymbolType GenerateRandomSymbol()
        {
            int randomValue = Random.Range(0, _totalWeight);
            int currentWeight = 0;

            foreach (var symbolWeight in _symbolWeights)
            {
                currentWeight += symbolWeight.Value;
                if (randomValue < currentWeight)
                {
                    return symbolWeight.Key;
                }
            }

            // Fallback (should never reach here)
            return SymbolType.Cherry;
        }

        /// <summary>
        /// Generate a winning symbol that matches all reels (for testing or special outcomes).
        /// </summary>
        public SymbolType GenerateWinningSymbol()
        {
            // Bias towards more common symbols for realistic wins
            int chance = Random.Range(0, 100);
            
            if (chance < 40) return SymbolType.Cherry;
            if (chance < 65) return SymbolType.Lemon;
            if (chance < 80) return SymbolType.Plum;
            if (chance < 90) return SymbolType.Bell;
            if (chance < 97) return SymbolType.Bar;
            return SymbolType.Seven; // Rare winning combination
        }

        /// <summary>
        /// Get the probability percentage of a symbol.
        /// </summary>
        public float GetSymbolProbability(SymbolType symbolType)
        {
            if (_symbolWeights.TryGetValue(symbolType, out int weight))
            {
                return (weight / (float)_totalWeight) * 100f;
            }
            return 0f;
        }
    }
}
