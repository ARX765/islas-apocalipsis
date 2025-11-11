# Islas Apocalipsis

A post-apocalyptic 3D action-adventure game featuring floating islands, combining advanced technology with medieval fantasy and magic. Inspired by The Legend of Zelda series with emphasis on exploration, combat, puzzles, and a comprehensive weapon and magic system.

## 🎮 Game Features

### Core Gameplay
- **Zelda-Style Exploration**: Large open world with interconnected floating islands
- **Combat Focus**: Real-time action combat with combo system and weapon variety
- **Puzzle Elements**: Environmental puzzles using magic, tools, and physics
- **Magic System**: 8 schools of magic with various spell types
- **Weapon Variety**: 12+ weapon types with unique characteristics

### Key Mechanics
- **Climbing System**: Stamina-based climbing on most surfaces
- **Weather System**: Dynamic weather affecting gameplay (rain makes climbing harder, storms damage player)
- **Combat Combos**: 3-hit light combos, heavy attacks, magic combos
- **Perfect Dodge**: Slow-motion counter window
- **Parry System**: Timed blocks create openings
- **Lock-on Targeting**: Target enemies for focused combat

### Content
- **40 Enemy Types**: 
  - 20 Common enemies
  - 10 Elite enemies  
  - 10 Special enemies
- **10 Boss Encounters**: Multi-phase boss fights with unique mechanics
- **Large World**: 5-8 km² explorable area across 15-20 major islands
- **3D Graphics**: PC-optimized with URP (Universal Render Pipeline)
- **Save System**: 3 save slots with auto-save functionality
- **Responsive UI**: Scales with resolution, supports controller and keyboard/mouse

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Core/           # Game managers and core systems
│   ├── Player/         # Player controller, stats, climbing
│   ├── Combat/         # Combat system, weapons
│   ├── Magic/          # Magic system and spells
│   ├── Enemies/        # Enemy AI and health
│   ├── Bosses/         # Boss controllers
│   ├── Items/          # Weapon and item data
│   ├── UI/             # UI managers and menus
│   ├── Systems/        # Weather, save system
│   └── Puzzles/        # Puzzle elements
├── Scenes/             # Game scenes
├── Prefabs/            # Prefabs for characters, enemies, items
├── Materials/          # Materials and shaders
├── Textures/           # Texture assets
├── Models/             # 3D models
├── Audio/              # Music and sound effects
└── Animation/          # Animation controllers
```

## 🎯 Systems Overview

### Player Systems
- **PlayerController**: Movement, jumping, running
- **PlayerStats**: Health, stamina, mana management
- **ClimbingSystem**: Stamina-based climbing mechanics
- **CombatSystem**: Attack combos, parrying, lock-on
- **MagicSystem**: Spell casting and mana consumption

### World Systems
- **WeatherSystem**: Dynamic weather with gameplay effects
- **SaveSystem**: Game state persistence
- **GameManager**: Global game state management
- **CameraController**: Third-person camera with lock-on

### Enemy Systems
- **EnemyAI**: Behavior types (aggressive, defensive, patrol, etc.)
- **EnemyHealth**: Health management and resistances
- **BossController**: Multi-phase boss encounters

### UI Systems
- **UIManager**: HUD, menus, notifications
- Health/Stamina/Mana bars
- Weapon durability display
- Combo counter
- Boss health bars

## 🚀 Getting Started

### Requirements
- Unity 2022.3 LTS or newer
- Universal Render Pipeline (URP)
- TextMeshPro
- Input System (new)

### Setup
1. Clone the repository
2. Open in Unity 2022.3 LTS
3. Import required packages (URP, TextMeshPro, Input System)
4. Open the main scene
5. Configure project settings as needed

### Controls (Default)
- **WASD**: Movement
- **Mouse**: Camera control
- **Shift**: Sprint
- **Space**: Jump
- **E**: Interact/Climb
- **Left Click**: Light Attack
- **Right Click**: Heavy Attack / Magic Cast
- **Q**: Parry
- **Tab**: Lock-on Toggle
- **1-8**: Quick cast spells
- **I**: Inventory
- **M**: Map
- **ESC**: Pause

## 📚 Documentation

- [GAME_DESIGN.md](GAME_DESIGN.md) - Complete game design document
- [ENEMIES.md](ENEMIES.md) - All 40 enemy types with stats and drops
- [BOSSES.md](BOSSES.md) - All 10 boss encounters with strategies
- [ProjectSettings.json](ProjectSettings.json) - Technical specifications

## 🎨 Game Theme

**Post-Apocalyptic Floating Islands**
- The world has been shattered into floating islands
- Mix of medieval fantasy and advanced technology
- Magic coexists with tech
- Corrupted creatures and powerful bosses
- Ancient ruins and mysteries to uncover

## 🎵 Audio Design

### Music
- Main theme
- Region-specific exploration tracks
- Dynamic combat music
- Boss battle themes
- Ambient soundscapes

### Sound Effects
- Weapon sounds per type
- Magic spell effects
- Enemy vocalizations
- Environmental sounds
- UI feedback
- Weather effects

## 🎯 Target Performance
- **Resolution**: 1920x1080 (scalable to 4K)
- **Target FPS**: 60 FPS
- **Platform**: PC (Windows/Linux/Mac)

## 🔧 Development Status

This project implements:
- ✅ Core player movement and controls
- ✅ Stamina-based climbing system
- ✅ Combat system with combos
- ✅ Magic system with 8 elements
- ✅ Weapon management
- ✅ Enemy AI system (40 types documented)
- ✅ Boss system (10 bosses documented)
- ✅ Weather system with gameplay effects
- ✅ Save/Load system
- ✅ UI framework
- ✅ Puzzle elements
- ✅ Game design documentation

## 📝 License

This project is created as a game development framework for "Islas Apocalipsis".

## 🤝 Contributing

This is a structured game project. Follow the existing code patterns and architecture when adding new features.

---

**Islas Apocalipsis** - A journey through shattered skies
