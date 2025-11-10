# Setup Guide - Islas Apocalipsis

## Prerequisites

### Required Software
1. **Unity Hub** (Latest Version)
   - Download: https://unity.com/download
   
2. **Unity 2022.3 LTS**
   - Install via Unity Hub
   - Include: Windows Build Support, Linux Build Support (optional), Mac Build Support (optional)
   
3. **Visual Studio 2022** or **Rider** (Recommended IDEs)
   - Visual Studio: https://visualstudio.microsoft.com/
   - Rider: https://www.jetbrains.com/rider/
   
4. **Git** (Version Control)
   - Download: https://git-scm.com/downloads
   
5. **Git LFS** (Large File Storage)
   - Download: https://git-lfs.github.com/

### Hardware Requirements
See [TECHNICAL_SPECS.md](TECHNICAL_SPECS.md) for detailed requirements.

**Minimum for Development:**
- CPU: Intel Core i5 / AMD Ryzen 5
- RAM: 16 GB (Unity Editor is RAM-intensive)
- GPU: NVIDIA GTX 1060 / AMD RX 580
- Storage: 50 GB free space (SSD recommended)

## Initial Setup

### 1. Clone Repository

```bash
# Clone the repository
git clone https://github.com/ARX765/islas-apocalipsis.git
cd islas-apocalipsis

# Initialize Git LFS
git lfs install
git lfs pull
```

### 2. Open in Unity

1. Open Unity Hub
2. Click "Add" → "Add project from disk"
3. Navigate to the cloned repository folder
4. Select the folder and click "Open"
5. Unity will import all assets (this may take 10-30 minutes first time)

### 3. Install Required Packages

The project should automatically install required packages. If not:

1. Open Unity
2. Go to **Window → Package Manager**
3. Install the following if missing:
   - Universal Render Pipeline (URP)
   - TextMeshPro
   - Input System
   - Cinemachine
   - Post Processing

### 4. Project Settings

Configure project settings (should be automatic):

1. **Edit → Project Settings → Player**
   - Company Name: Your Studio Name
   - Product Name: Islas Apocalipsis
   - Icon: (Add your icon)

2. **Edit → Project Settings → Quality**
   - Set default quality level

3. **Edit → Project Settings → Input System Package**
   - Set to "Both" (old and new input system)

## Project Structure Understanding

```
islas-apocalipsis/
├── Assets/
│   ├── Scenes/              # Game scenes (to be created)
│   ├── Scripts/             # All C# scripts ✓
│   ├── Prefabs/             # Reusable game objects
│   ├── Materials/           # Material assets
│   ├── Textures/            # Texture files
│   ├── Models/              # 3D models
│   ├── Audio/               # Sound and music
│   └── Animation/           # Animation files
├── ProjectSettings/         # Unity project settings
├── Packages/                # Package dependencies
├── Documentation/
│   ├── GAME_DESIGN.md       # Complete game design ✓
│   ├── ENEMIES.md           # Enemy catalog ✓
│   ├── BOSSES.md            # Boss guide ✓
│   ├── WEAPONS.md           # Weapon catalog ✓
│   ├── SPELLS.md            # Spell catalog ✓
│   ├── AUDIO.md             # Audio design ✓
│   ├── WORLD_DESIGN.md      # World layout ✓
│   ├── TECHNICAL_SPECS.md   # Tech specs ✓
│   ├── ROADMAP.md           # Development plan ✓
│   └── SETUP.md             # This file ✓
├── .gitignore               # Git ignore file ✓
├── README.md                # Project overview ✓
└── ProjectSettings.json     # Project config ✓
```

## Quick Start Development

### Creating Your First Scene

1. **Create Main Menu Scene**
```
File → New Scene
Save As: Assets/Scenes/MainMenu.unity
```

2. **Create Game Scene**
```
File → New Scene
Save As: Assets/Scenes/StartingHighlands.unity
```

3. **Add to Build Settings**
```
File → Build Settings
Add Open Scenes (both scenes)
MainMenu should be index 0
```

### Setting Up the Player

1. **Create Player GameObject**
```
Hierarchy → Create Empty → "Player"
Add Tag "Player"
```

2. **Add Components**
```
Add Component → Character Controller
Add Component → Player Controller (script)
Add Component → Player Stats (script)
Add Component → Climbing System (script)
Add Component → Combat System (script)
Add Component → Magic System (script)
Add Component → Weapon Manager (script)
```

3. **Create Ground Check**
```
Create Empty under Player → "GroundCheck"
Position at player's feet
Reference in PlayerController
```

### Setting Up Camera

1. **Create Camera System**
```
Hierarchy → Create Empty → "CameraSystem"
Add Main Camera as child
Add Component → Camera Controller (script)
```

2. **Configure Camera**
```
Set target to Player
Adjust offset and settings
```

### Creating First Enemy

1. **Create Enemy GameObject**
```
Hierarchy → Create Capsule → "CorruptedScout"
Add Tag "Enemy"
Add Layer "Enemy"
```

2. **Add Components**
```
Add Component → Nav Mesh Agent
Add Component → Enemy AI (script)
Add Component → Enemy Health (script)
```

3. **Configure Enemy**
```
Set enemy type to CorruptedScout
Adjust stats (health, damage, etc.)
Set detection range and attack range
```

### Setting Up Managers

1. **Create GameManager**
```
Hierarchy → Create Empty → "GameManager"
Add Component → Game Manager (script)
```

2. **Create UIManager**
```
Hierarchy → Create Empty → "UIManager"
Add Component → UI Manager (script)
Add Canvas as child for UI elements
```

3. **Create SaveSystem**
```
Hierarchy → Create Empty → "SaveSystem"
Add Component → Save System (script)
```

4. **Create WeatherSystem**
```
Hierarchy → Create Empty → "WeatherSystem"
Add Component → Weather System (script)
```

## Testing Your Setup

### Test Player Movement

1. Enter Play Mode (Ctrl/Cmd + P)
2. Use WASD to move
3. Space to jump
4. Shift to run
5. Check console for any errors

### Test Combat

1. Create a test enemy
2. Enter Play Mode
3. Approach enemy
4. Left Click to attack
5. Verify damage and combo system

### Test Save System

1. Enter Play Mode
2. Press F5 to quick save (add this input)
3. Exit Play Mode
4. Check save file location
5. Verify data saved correctly

## Development Workflow

### Daily Workflow

1. **Pull Latest Changes**
```bash
git pull origin main
```

2. **Create Feature Branch**
```bash
git checkout -b feature/your-feature-name
```

3. **Make Changes**
   - Write code
   - Test frequently
   - Commit often

4. **Commit Changes**
```bash
git add .
git commit -m "Description of changes"
```

5. **Push to Remote**
```bash
git push origin feature/your-feature-name
```

6. **Create Pull Request**
   - Via GitHub interface
   - Request review
   - Merge after approval

### Best Practices

1. **Test Before Committing**
   - Enter Play Mode
   - Test your changes
   - Check console for errors
   - Exit cleanly

2. **Keep Scenes Clean**
   - Delete test objects
   - Organize hierarchy
   - Use prefabs for reusable objects

3. **Document Your Code**
   - Add XML comments
   - Update documentation
   - Comment complex logic

4. **Performance Awareness**
   - Profile regularly
   - Optimize as you go
   - Test on target hardware

## Common Issues & Solutions

### Unity Won't Open Project

**Solution:**
- Check Unity version (must be 2022.3 LTS)
- Delete Library folder and reopen
- Reimport all assets

### Scripts Won't Compile

**Solution:**
- Check for syntax errors
- Verify namespace imports
- Regenerate project files (Assets → Open C# Project)

### Git LFS Issues

**Solution:**
```bash
git lfs install
git lfs fetch
git lfs pull
```

### Performance Issues in Editor

**Solution:**
- Close unnecessary windows
- Reduce scene complexity
- Use Build and Run for better performance
- Increase Edit → Preferences → GI Cache size

### NavMesh Not Working

**Solution:**
- Bake NavMesh: Window → AI → Navigation
- Mark surfaces as walkable
- Set NavMesh Agent properly

## Editor Extensions (Optional)

### Recommended Assets

1. **ProBuilder** (Free)
   - For level design prototyping
   
2. **Odin Inspector** (Paid)
   - Better inspector attributes
   
3. **DOTween** (Free)
   - Animation and tweening
   
4. **Amplify Shader Editor** (Paid)
   - Custom shader creation

## Testing Checklist

Before committing major changes:

- [ ] Project compiles without errors
- [ ] All scenes load correctly
- [ ] Player movement works
- [ ] Combat system functional
- [ ] Magic system works
- [ ] No console errors in Play Mode
- [ ] Performance is acceptable (60+ FPS)
- [ ] Save/Load works
- [ ] UI responds correctly
- [ ] Documentation updated

## Getting Help

### Resources

1. **Project Documentation**
   - Read all .md files in project root
   - Check code comments

2. **Unity Documentation**
   - https://docs.unity3d.com/

3. **Unity Forums**
   - https://forum.unity.com/

4. **Unity Answers**
   - https://answers.unity.com/

### Contact

For project-specific questions:
- Create GitHub Issue
- Check existing documentation
- Ask in project Discord (if available)

## Next Steps

After setup is complete:

1. **Read Documentation**
   - Review GAME_DESIGN.md
   - Understand system architecture
   - Study enemy and boss designs

2. **Follow Roadmap**
   - Check ROADMAP.md
   - Start with Phase 2 (Asset Creation)
   - Create in priority order

3. **Create First Playable**
   - Implement Starting Highlands
   - Add 5 basic enemies
   - Create first boss
   - Build vertical slice

4. **Iterate and Improve**
   - Get feedback
   - Refine systems
   - Add content progressively

## Build Instructions

### Development Build

```
File → Build Settings
Platform: PC, Mac & Linux Standalone
Target Platform: Windows
Architecture: x86_64
Development Build: ✓
Script Debugging: ✓
Build
```

### Release Build

```
File → Build Settings
Platform: PC, Mac & Linux Standalone
Target Platform: Windows
Architecture: x86_64
Development Build: ✗
Script Debugging: ✗
Compression: LZ4
Build
```

## Troubleshooting Build

### Build Fails

**Check:**
- All scenes added to build
- No compile errors
- Platform modules installed
- Enough disk space

### Build Crashes

**Check:**
- Quality settings appropriate
- Graphics API compatible
- No null references
- All assets included

---

**You're now ready to start developing Islas Apocalipsis!**

For detailed game design, see: [GAME_DESIGN.md](GAME_DESIGN.md)  
For development timeline, see: [ROADMAP.md](ROADMAP.md)  
For technical details, see: [TECHNICAL_SPECS.md](TECHNICAL_SPECS.md)
