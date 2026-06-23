# 🎰 Slot Game Assignment (Unity)

A Unity slot machine project with smooth reel animations, weighted/fair RNG, win + payout logic, and WebGL deployment workflow.

## Contents
- [Quick Start](#quick-start)
- [How to Play](#how-to-play)
- [Project Highlights](#project-highlights)
- [Architecture](#architecture)
- [Running WebGL](#running-webgl)
- [Documentation](#documentation)

## Quick Start

### Run in Unity Editor
1. Open the project in Unity.
2. Load `Assets/Scenes/SlotMachine.unity`.
3. Press **Play**.
4. Click **Spin**.

### Controls
- **Spin**: costs $10 and spins the 3 reels
- **Add Balance**: adds credits (for testing)
- **Reset**: returns to initial state
- **Test Win**: forces a winning combination (for verifying payout/UI)

## How to Play
- A **win** happens when **all 3 reels show the same symbol**.
- Payout is calculated immediately after the reels stop.

## Project Highlights
- **Winning logic**: 3-of-a-kind match check
- **Reel animations**: coroutine-driven spinning with staggered timing
- **Fair RNG**: **weighted probability** symbol selection
- **Event-driven UI**: core logic updates UI via events (loose coupling)
- **Stats & UI feedback**: balance/win info + win tracking

## Architecture
- **Core** (`Assets/Scripts/Core/`): symbol, RNG, reel, slot machine logic (game rules)
- **UI** (`Assets/Scripts/UI/`): presentation and UI state updates
- **Managers** (`Assets/Scripts/Managers/`): orchestration (game/audio)

Core gameplay logic publishes events (e.g., balance change / spin complete) and UI subscribes.

## Running WebGL
WebGL build output is in `Build/WebGL`.

```bash
cd Build/WebGL
python -m http.server 8000
```

Open: http://localhost:8000

## Documentation
- `QUICK_START.md` — editor + WebGL quick run steps
- `PROJECT_STRUCTURE.md` — folder-by-folder explanation
- `DEVELOPMENT_APPROACH.md` — design decisions and architecture rationale
- `WEBGL_BUILD_GUIDE.md` — WebGL build configuration details

