# WebGL Build Configuration Guide

## Overview

This document provides instructions for building the Slot Game for WebGL deployment.

## Prerequisites

- Unity 2020 LTS or higher
- WebGL Build Support module installed
- Project already set up (see README.md)

## Building for WebGL

### Step 1: Open Build Settings

```
File → Build Settings
```

### Step 2: Switch Platform to WebGL

1. In the left panel, select **WebGL**
2. Click **"Switch Platform"** (this may take several minutes)
3. Wait for compilation to complete

### Step 3: Configure Build Settings

1. **Scenes In Build**:
   - Add `Assets/Scenes/SlotMachine.unity` if not already present
   - Ensure it's at index 0

2. **Player Settings** (Edit → Project Settings → Player):
   
   **Resolution and Presentation**:
   - Default Canvas Width: 1920
   - Default Canvas Height: 1080
   - WebGL Template: Default
   - Fullscreen Mode: Windowed
   
   **Graphics**:
   - Anti-aliasing: FXAA (fast)
   - Static Batching: Enabled
   - Dynamic Batching: Enabled
   
   **Publishing Settings**:
   - Enable Exceptions: Explicitly Thrown Exceptions Only
   - Enable Data Caching: Enabled (for faster load times)

### Step 4: Build the Project

1. **Select Build Output Folder**:
   - Click **"Build"** in Build Settings
   - Navigate to `Build/WebGL`
   - Create folder if it doesn't exist
   - Select as output destination

2. **Start Build**:
   - Click **"Build"** button
   - Wait for compilation (typically 2-5 minutes)
   - Result: WebGL build output in `Build/WebGL/`

## Build Output Structure

```
Build/WebGL/
├── index.html           # Main web page
├── Build/
│   ├── SlotGame.loader.js
│   ├── SlotGame.framework.js
│   └── SlotGame.wasm
├── TemplateData/
│   ├── style.css
│   ├── favicon.ico
│   └── unity-logo-dark.png
└── StreamingAssets/     # Any custom game data
```

## Running WebGL Build Locally

### Option 1: Python HTTP Server (Recommended)

```bash
cd Build/WebGL
python -m http.server 8000
```

Then open browser: `http://localhost:8000`

### Option 2: Node.js HTTP Server

```bash
cd Build/WebGL
npx http-server
```

### Option 3: Unity's Built-in Web Server

Some Unity versions include a built-in web server:
```bash
cd Build/WebGL
python -m http.server 5500
```

### Option 4: Direct File Opening

Double-click `Build/WebGL/index.html` to open directly.

⚠️ **Note**: Some features may not work properly due to CORS restrictions when opening directly. Use a local server instead.

## Deployment to Web Server

### Upload to Web Host

1. Compress the `Build/WebGL` folder
2. Upload to your web server
3. Extract on server
4. Configure CORS headers if needed:

```apache
# .htaccess for Apache servers
<FilesMatch "\.(wasm|js)$">
    AddType application/wasm .wasm
    AddType application/javascript .js
    Header add Access-Control-Allow-Origin "*"
</FilesMatch>
```

### Configure Web Server

**For Apache**:
```apache
<IfModule mod_mime.c>
    AddType application/wasm .wasm
</IfModule>
```

**For Nginx**:
```nginx
types {
    application/wasm wasm;
}
```

## Performance Optimization

### Reduce Build Size

1. **Strip Engine Code**: In Player Settings
   - Strip Engine Code: Enabled
   - Stripping Level: Aggressive

2. **Use AssetBundles**: For large games
   - Create asset bundles
   - Load asynchronously

3. **Compression**:
   - Enable gzip compression on server
   - Typical build: ~30MB gzipped to ~8MB

### Faster Load Times

1. **Enable Caching**: In Publishing Settings
   - Enable Data Caching: Enabled
   
2. **Service Workers**: For offline support
3. **CDN**: Use content delivery network for assets

## Troubleshooting

### Build Fails
- **Solution**: Restart Unity, clear Library folder, rebuild
- **Check**: All scripts are properly named and in correct folders

### WebGL Doesn't Load
- **Solution**: Check browser console for errors (F12)
- **Check**: Web server is running and serving files
- **Check**: CORS headers are configured correctly

### Poor Performance
- **Solution**: Reduce graphics quality in Player Settings
- **Solution**: Use lower resolution canvas
- **Solution**: Profile with Chrome DevTools

### White Screen/Loading Hangs
- **Solution**: Check browser console for JavaScript errors
- **Solution**: Increase timeout in Player Settings
- **Solution**: Clear browser cache and reload

## Browser Compatibility

| Browser | Support | Notes |
|---------|---------|-------|
| Chrome | ✅ | Excellent WebGL 2.0 support |
| Firefox | ✅ | Good support, similar to Chrome |
| Safari | ✅ | Requires WebGL 2.0 support (macOS 11+) |
| Edge | ✅ | Chromium-based, excellent support |
| Internet Explorer | ❌ | Not supported |

## Minimum Requirements

- **WebGL 2.0** support required
- **2GB RAM** recommended
- **Broadband internet** recommended (8MB build)
- **Modern GPU** (integrated graphics sufficient)

## Build Optimization Checklist

- ✅ All scenes included in Build Settings
- ✅ Player Settings configured for WebGL
- ✅ Compression enabled
- ✅ Code stripping configured
- ✅ Target 30MB or less (gzipped)
- ✅ Load time under 10 seconds
- ✅ 60 FPS target frame rate
- ✅ Touch input supported (for mobile)

## Advanced Configuration

### Custom Canvas Size

Edit `Build/WebGL/index.html`:

```html
<canvas id="unity-canvas"></canvas>
<script>
    // Adjust canvas size
    var canvas = document.getElementById('unity-canvas');
    canvas.width = 1920;
    canvas.height = 1080;
</script>
```

### Add Custom Styling

Create custom CSS in `Build/WebGL/TemplateData/style.css`:

```css
body {
    background-color: #222;
    font-family: Arial, sans-serif;
}

canvas {
    border-radius: 10px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.5);
}
```

### Loading Screen

Customize loading screen by modifying HTML template before build.

## Additional Resources

- [Unity WebGL Documentation](https://docs.unity3d.com/Manual/webgl.html)
- [WebGL Best Practices](https://docs.unity3d.com/Manual/webgl-bestpractices.html)
- [WASM Support](https://docs.unity3d.com/Manual/webgl-building.html)

---

## Building Success Criteria

✅ Build completes without errors
✅ WebGL folder contains all necessary files
✅ Can load in browser via local server
✅ Game functions identically to editor version
✅ All buttons and interactions work
✅ Animations are smooth
✅ Load time under 30 seconds
✅ No console errors when playing

---

**Last Updated**: June 2024
**Status**: Ready for WebGL deployment
