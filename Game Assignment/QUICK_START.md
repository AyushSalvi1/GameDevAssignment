# Slot Game Assignment - Quick Start Guide

## 🚀 Quick Start (5 minutes)

### Running in Unity Editor

1. **Open Unity**
   - Launch Unity Hub
   - Open "Game Assignment" folder

2. **Load Scene**
   - Navigate to `Assets/Scenes/SlotMachine.unity`
   - Double-click to load

3. **Play**
   - Press the Play button (▶)
   - Click "Spin" to start the game

### Building for WebGL

1. **Open Build Settings**
   - File → Build Settings
   - Switch Platform to WebGL

2. **Configure Build**
   - Select `Build/WebGL` as output folder
   - Adjust quality settings if needed
   - Click "Build"

3. **Run Build**
   ```bash
   cd Build/WebGL
   python -m http.server 8000
   # Open http://localhost:8000 in browser
   ```

## 🎮 How to Play

| Action | Result |
|--------|--------|
| Click **Spin** | Reels spin, costs $10 |
| Match 3 Symbols | WIN and get payout |
| **Add Balance** | Add $100 to continue |
| **Test Win** | Force a winning spin |
| **Reset** | Return to start |

## 💡 Tips

- Watch for the rare **Diamond** symbol (250x payout!)
- Minimum balance required to spin: $10
- All symbols are weighted by probability
- Use Test Win button to verify payouts

## 📊 Example Payouts

```
3 × Cherry (10x) = $300 payout
3 × Bell (25x) = $750 payout  
3 × Seven (100x) = $3,000 payout
3 × Diamond (250x) = $7,500 payout
```

## ⚙️ Configuration

### Adjusting Game Settings

Edit `Assets/Scripts/Core/SlotMachine.cs`:

```csharp
[SerializeField] private float spinDuration = 2f;          // How long reels spin
[SerializeField] private int numberOfReels = 3;             // Number of reels
[SerializeField] private int costPerSpin = 10;              // Cost per spin
[SerializeField] private int initialBalance = 1000;         // Starting money
```

### Adjusting Symbol Probabilities

Edit `Assets/Scripts/Core/GameRNG.cs`:

```csharp
private readonly Dictionary<SymbolType, int> _symbolWeights = new()
{
    { SymbolType.Cherry, 20 },    // Change these numbers to adjust
    { SymbolType.Diamond, 2 },    // Higher = more common
};
```

## 📱 Testing

### What to Test

- ✅ Spin button works
- ✅ Balance decreases by $10
- ✅ Reels spin for 2 seconds
- ✅ Winning combinations show matching symbols
- ✅ Payouts are calculated correctly
- ✅ UI updates in real-time
- ✅ Can't spin with low balance

### Using Test Features

- **Test Win Button**: Forces a winning combination
- **Add Balance Button**: Add $100 for testing
- **Reset Button**: Start fresh

## 🐛 Troubleshooting

### Spins not working
- Check balance ≥ $10
- Look for error in Console (Ctrl+Shift+C)

### Reels not animating
- Check Scene has Canvas
- Verify Reel prefab references

### Build issues
- Switch Platform to WebGL in Build Settings
- Check Assets are in correct folders

## 📚 Learn More

- `README.md` - Full documentation
- `PROJECT_STRUCTURE.md` - Folder organization
- `DEVELOPMENT_APPROACH.md` - Architecture details
- Code comments - Implementation details

---

Need help? Check the documentation files!
