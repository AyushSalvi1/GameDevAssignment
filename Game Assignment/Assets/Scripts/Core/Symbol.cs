using UnityEngine;

namespace SlotGame.Core
{
    /// <summary>
    /// Enumeration of all possible symbols in the slot machine.
    /// </summary>
    public enum SymbolType
    {
        Cherry = 0,
        Lemon = 1,
        Orange = 2,
        Plum = 3,
        Bell = 4,
        Bar = 5,
        Seven = 6,
        Diamond = 7
    }

    /// <summary>
    /// Represents a single symbol with its type and payout value.
    /// </summary>
    public class Symbol
    {
        public SymbolType Type { get; private set; }
        public int PayoutMultiplier { get; private set; }

        /// <summary>
        /// Initialize a new Symbol with its type and payout value.
        /// </summary>
        /// <param name="type">The type of symbol</param>
        public Symbol(SymbolType type)
        {
            Type = type;
            PayoutMultiplier = GetPayoutForSymbol(type);
        }

        /// <summary>
        /// Determines the payout multiplier for a given symbol type.
        /// Higher value symbols have higher payouts.
        /// </summary>
        private int GetPayoutForSymbol(SymbolType type)
        {
            return type switch
            {
                SymbolType.Cherry => 10,
                SymbolType.Lemon => 15,
                SymbolType.Orange => 15,
                SymbolType.Plum => 20,
                SymbolType.Bell => 25,
                SymbolType.Bar => 50,
                SymbolType.Seven => 100,
                SymbolType.Diamond => 250, // Rare symbol with highest payout
                _ => 0
            };
        }

        /// <summary>
        /// Get a human-readable name for the symbol type.
        /// </summary>
        public string GetSymbolName()
        {
            return Type.ToString();
        }
    }
}
