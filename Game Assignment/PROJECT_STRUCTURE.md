# Slot Game - Project Structure Documentation

## Overview
This is a complete Unity Slot Machine game implementation with clean architecture, proper RNG, and full game mechanics.

## Project Structure

```
Assets/
├── Scripts/
│   ├── Core/                 # Core game logic
│   │   ├── Symbol.cs         # Symbol definition and payouts
│   │   ├── GameRNG.cs        # Fair random number generator
│   │   ├── Reel.cs           # Individual reel controller
│   │   ├── SlotMachine.cs    # Main slot machine manager
│   │   ├── AudioManager.cs   # Audio system
│   │   ├── GameUtilities.cs  # Utility functions
│   │   └── SlotGame.Core.asmdef  # Assembly definition
│   │
│   ├── UI/                   # UI and display systems
│   │   ├── UIManager.cs      # Main UI controller
│   │   ├── GameInfoDisplay.cs # Game statistics display
│   │   └── SlotGame.UI.asmdef   # Assembly definition
│   │
│   ├── Managers/             # Game managers
│   │   ├── GameManager.cs    # Central game controller
│   │   └── SlotGame.Managers.asmdef # Assembly definition
│   │
├── Prefabs/                  # Game object prefabs
│   ├── Reel.prefab           # Reel prefab
│   ├── SlotMachine.prefab    # Main slot machine
│   └── UI/                   # UI prefabs
│
├── Animations/               # Animation files
│   ├── ReelSpin.anim         # Reel spinning animation
│   └── SymbolDisplay.anim    # Symbol display animation
│
├── Sprites/                  # Graphics assets
│   ├── Symbols/              # Symbol graphics
│   │   ├── cherry.png
│   │   ├── lemon.png
│   │   ├── orange.png
│   │   ├── plum.png
│   │   ├── bell.png
│   │   ├── bar.png
│   │   ├── seven.png
│   │   └── diamond.png
│   └── UI/                   # UI graphics
│
├── UI/                       # UI layouts and elements
│   ├── MainCanvas.prefab     # Main game canvas
│   └── Panels/
│
└── Scenes/
    └── SlotMachine.unity     # Main game scene
```

## Core Systems

### 1. Symbol System (Symbol.cs)
- Defines all symbol types with enum
- Manages payout values for each symbol
- Provides symbol naming and identification

### 2. RNG System (GameRNG.cs)
- **Weighted random symbol generation** for fair gameplay
- Configurable probability distribution
- Testing methods for winning symbols
- Probability query methods for statistics

### 3. Reel System (Reel.cs)
- Individual reel spinning with smooth animations
- Symbol display and management
- Coroutine-based animation system
- Support for deterministic results (for testing)

### 4. Slot Machine Manager (SlotMachine.cs)
- Central game flow controller
- Spin management and state handling
- Win detection logic (all 3 reels match)
- Payout calculation system
- Event system for UI updates

### 5. UI System (UIManager.cs, GameInfoDisplay.cs)
- Real-time balance updates
- Win/loss status display
- Button state management
- Game statistics tracking
- Probability information display

### 6. Audio System (AudioManager.cs)
- Background music management
- Sound effects (spin, win, lose)
- Volume control
- Singleton pattern for global access

## Game Flow

1. **Initialization**: GameManager initializes all systems
2. **Player Action**: Player presses Spin button
3. **Deduction**: Spin cost deducted from balance
4. **Animation**: Reels spin with staggered timing
5. **Result**: Random symbols generated via weighted RNG
6. **Win Check**: All 3 symbols compared for match
7. **Payout**: If win, calculate and apply payout
8. **UI Update**: UI displays result and new balance

## Winning Logic

A **winning combination** occurs when all three reels display the **same symbol**.

### Payout Calculation
```
Total Payout = (Symbol Base Payout × Number of Reels) × Spin Cost
```

**Example:**
- Symbol: Bell (base payout 25x)
- All 3 reels show Bell
- Spin cost: $10
- Payout: (25 × 3) × 10 = $750

## Symbol Payouts

| Symbol | Base Payout | Probability | Rarity |
|--------|-------------|------------|--------|
| Cherry | 10x | 20% | Common |
| Lemon | 15x | 15% | Common |
| Orange | 15x | 15% | Common |
| Plum | 20x | 12% | Uncommon |
| Bell | 25x | 12% | Uncommon |
| Bar | 50x | 10% | Rare |
| Seven | 100x | 4% | Very Rare |
| Diamond | 250x | 2% | Ultra Rare |

## RNG Implementation

The RNG system uses **weighted random selection**:
- Each symbol has a defined probability weight
- Random value selected uniformly between 0 and total weight
- Symbol selected based on which weight range contains the random value
- Fair and unpredictable outcomes
- Configurable probabilities for game balance

## Code Quality Features

✅ **Object-Oriented Design** - Clear class hierarchy and responsibilities
✅ **Clean Code** - Meaningful names, DRY principle, single responsibility
✅ **Documentation** - XML comments on all public methods
✅ **Error Handling** - Null checks and validation
✅ **Event System** - Decoupled communication between systems
✅ **Singleton Pattern** - Proper global managers
✅ **Assembly Definitions** - Organized script compilation

## Testing Features

- **Test Win Button**: Force a winning combination for testing
- **Probability Display**: Shows real-time probability data
- **Statistics Tracking**: Accumulates win rate and payout data
- **Debug Logging**: Comprehensive console logging

## Performance Considerations

- Coroutine-based animations prevent frame blocking
- Object pooling potential for reels
- UI update optimization through event system
- Lightweight RNG calculations
- No frame rate dependency for game logic

## How to Run

1. Open Unity project
2. Load `Assets/Scenes/SlotMachine.unity`
3. Press Play in Editor
4. Click Spin button to start spinning reels
5. Watch for winning combinations
6. Add balance to continue playing

## WebGL Build

This project is configured for WebGL builds:
- Canvas scaling for responsive design
- Touch input compatible UI
- Optimized rendering for web
- Build output to `/Build/WebGL` folder

## Bonus Features

- Configurable symbol probabilities
- Win rate statistics tracking
- Test win button for development
- Dynamic payout calculations
- Staggered reel animations for visual appeal
- Audio system with background music
