using UnityEngine;

namespace SlotGame.Utilities
{
    /// <summary>
    /// Utility class for common game operations and calculations.
    /// Provides helper methods for the slot game system.
    /// </summary>
    public static class GameUtilities
    {
        /// <summary>
        /// Format a currency value with proper formatting.
        /// </summary>
        public static string FormatCurrency(int amount)
        {
            return $"${amount:N0}";
        }

        /// <summary>
        /// Calculate percentage between two values.
        /// </summary>
        public static float CalculatePercentage(int value, int total)
        {
            if (total == 0) return 0f;
            return (value / (float)total) * 100f;
        }

        /// <summary>
        /// Clamp a value between min and max.
        /// </summary>
        public static int Clamp(int value, int min, int max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        /// <summary>
        /// Get a random bool with given probability.
        /// </summary>
        /// <param name="probability">Probability between 0-1</param>
        public static bool RandomBool(float probability)
        {
            return Random.value < probability;
        }

        /// <summary>
        /// Shuffle an array of items.
        /// </summary>
        public static void ShuffleArray<T>(T[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);

                // Swap
                T temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }
    }
}
