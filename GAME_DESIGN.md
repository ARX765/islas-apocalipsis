# Islas Apocalipsis - Game Design Document

## Overview
A post-apocalyptic 3D action-adventure game featuring floating islands with a blend of advanced technology and medieval fantasy magic. Inspired by The Legend of Zelda series with a focus on exploration, combat, puzzles, and a robust magic and weapon system.

## Setting
The world has been shattered into floating islands suspended in the sky. Ancient civilizations have fallen, leaving behind remnants of both advanced technology and powerful magic. Players must navigate these islands, uncovering the secrets of the apocalypse while battling corrupted creatures and powerful bosses.

## Core Gameplay Pillars

### 1. Exploration
- Large open world composed of interconnected floating islands
- Zelda-style discovery and progression
- Hidden secrets, treasures, and lore scattered throughout
- Vertical exploration using climbing mechanics
- Environmental storytelling

### 2. Combat System
- Real-time action combat
- Combo system for chaining attacks
- Dodge/parry mechanics
- Lock-on targeting
- Weapon variety (melee and ranged)
- Magic integration in combat

### 3. Puzzles
- Environmental puzzles using magic and tools
- Ancient technology puzzles
- Boss-specific mechanics
- Optional challenge dungeons

### 4. Character Progression
- Weapon upgrades and crafting
- Magic spell unlocking and upgrading
- Stamina and health increases
- Special abilities and techniques

## Core Mechanics

### Climbing System
- **Stamina-based**: Climbing consumes stamina over time
- **Surface Detection**: Can climb most vertical surfaces
- **Ledge Grabbing**: Automatic ledge detection and pull-up
- **Stamina Management**: Jump consumes more stamina while climbing
- **Recovery**: Stamina regenerates when not climbing

### Weather System
- **Dynamic Weather**: Rain, storms, clear skies, fog
- **Gameplay Impact**: 
  - Rain makes surfaces slippery (climbing harder)
  - Storms create dangerous conditions
  - Lightning can be redirected with metal weapons
  - Weather affects enemy behavior

### Combat Combos
- **Light Attack Chain**: 3-hit basic combo
- **Heavy Attack**: Charged powerful strike
- **Aerial Attacks**: Jump attacks with slam damage
- **Magic Combos**: Combine weapon attacks with spells
- **Perfect Dodge**: Slow-motion counter window
- **Parry System**: Timed blocks create openings

## Content Specifications

### Enemies (40 Types)
**Common Enemies (20 types)**
1. Corrupted Scout - Basic melee enemy
2. Sky Raider - Aerial swooping enemy
3. Tech Drone - Hovering ranged attacker
4. Void Crawler - Fast melee creature
5. Stone Golem - Heavy slow tank
6. Crystal Spider - Web-shooting enemy
7. Shadow Wraith - Teleporting attacker
8. Flame Imp - Fire-based caster
9. Ice Sprite - Freezing projectiles
10. Lightning Wisp - Electric attacks
11. Poison Bloat - Area denial enemy
12. Rusty Sentinel - Shield-bearing guard
13. Wild Beast - Animal-type enemy
14. Corrupted Mage - Magic user
15. Tech Soldier - Ranged firearms
16. Blade Dancer - Quick combo attacker
17. Siege Breaker - Heavy weapons
18. Sky Sniper - Long-range attacker
19. Void Hound - Pack hunter
20. Crystal Construct - Magic resistant

**Elite Enemies (10 types)**
21. Ancient Guardian - Mini-boss tier
22. Sky Captain - Commander unit
23. Tech Overseer - Shield + summons
24. Void Titan - Large berserker
25. Frost Giant - Ice powers + high HP
26. Storm Caller - Weather manipulation
27. Shadow Assassin - Stealth attacks
28. Fire Demon - Area burning attacks
29. Crystal Wyrm - Flying serpent
30. Corrupted Champion - Weapon master

**Special Enemies (10 types)**
31. Mimic Chest - Treasure disguise
32. Explosive Carrier - Suicide bomber
33. Healer Priest - Supports other enemies
34. Summoner Cultist - Spawns minions
35. Berserker Brute - Rage mode
36. Shielded Knight - Must break guard
37. Dual Blade Ronin - Parry-focused
38. Elemental Core - Weak spot combat
39. Necromancer - Revives fallen
40. Time Warper - Slows player

### Bosses (10 Types)
1. **The Fallen Sky Lord** - Aerial combat, flying arena
2. **Corrupted Titan Golem** - Multi-phase, destroy body parts
3. **The Shadow King** - Clone mechanics, shadow realm phase
4. **Ancient Tech Colossus** - Weak point targeting, laser attacks
5. **Ice Empress** - Freezing environment, ice prison attacks
6. **Fire Drake** - Flying fire-breathing dragon
7. **Storm Sovereign** - Weather manipulation, lightning strikes
8. **The Void Entity** - Reality warping, dimensional shifts
9. **Crystal Hivemind** - Multiple forms, swarm mechanics
10. **The Last Guardian** - Final boss, all mechanics combined

## Systems

### Weapon System
**Weapon Types:**
- One-Handed Swords
- Two-Handed Swords
- Axes and Hammers
- Spears and Lances
- Bows and Crossbows
- Daggers and Knives
- Magic Staves
- Tech Guns

**Weapon Attributes:**
- Base Damage
- Attack Speed
- Durability
- Special Effects (elemental, tech)
- Upgrade Slots

### Magic System
**Schools of Magic:**
1. **Fire Magic**: Offensive burn damage
2. **Ice Magic**: Freeze and slow effects
3. **Lightning Magic**: Chain damage, stun
4. **Earth Magic**: Defense and area control
5. **Wind Magic**: Movement and projectile manipulation
6. **Shadow Magic**: Stealth and debuffs
7. **Light Magic**: Healing and holy damage
8. **Time Magic**: Slow and speed effects

**Spell Types:**
- Projectile Spells
- Area of Effect
- Self Buffs
- Debuffs
- Summons
- Environmental Interaction

### Save System
- **Auto-save**: At checkpoints and key moments
- **Manual Save**: At save crystals throughout world
- **Multiple Slots**: 3 save file slots
- **Save Data Includes**:
  - Player position and stats
  - Inventory and equipment
  - Quest progress
  - Defeated bosses and enemies
  - Discovered locations
  - Collected items

### UI System
**HUD Elements:**
- Health Bar
- Stamina Bar
- Magic/Mana Bar
- Weapon Durability
- Mini-map
- Quest Tracker
- Quick-item Slots
- Combo Counter

**Menus:**
- Inventory
- Equipment
- Magic Spells
- Map
- Quest Log
- Settings
- Character Stats

**Responsive Design:**
- Scales with resolution
- Controller and Keyboard/Mouse support
- Accessibility options
- Customizable layouts

### Audio Design
**Music:**
- Main Theme
- Exploration Tracks (per region)
- Combat Music (dynamic intensity)
- Boss Battle Themes
- Town/Safe Area Music
- Ambient Soundscapes

**Sound Effects:**
- Weapon sounds (per type)
- Magic spell sounds
- Enemy sounds (attacks, deaths)
- Environmental sounds
- UI feedback sounds
- Footsteps (per surface)
- Climbing sounds
- Weather effects

## World Design

### World Size
- **Large Open World**: 5-8 km² explorable area
- **Multiple Islands**: 15-20 major floating islands
- **Vertical Design**: Heavy use of vertical space
- **Interconnected**: Bridges, teleports, and flight paths

### Regions
1. **Starting Highlands** - Tutorial area, grasslands
2. **Tech Ruins** - Ancient civilization remnants
3. **Crystal Caverns** - Underground crystal caves
4. **Scorched Wastes** - Fire and lava region
5. **Frozen Peaks** - Ice and snow mountains
6. **Storm Islands** - Constant lightning storms
7. **Shadow Realm** - Dark corrupted area
8. **Sky Gardens** - Floating gardens and nature
9. **Void Rifts** - Dimensional tears
10. **The Core** - Final area, mixed elements

## Graphics & Performance

### Graphics
- **Style**: Semi-realistic with stylized elements
- **Render Pipeline**: URP for optimization
- **Lighting**: Dynamic time of day, weather lighting
- **Post-Processing**: Bloom, ambient occlusion, color grading
- **Particle Effects**: Magic, weather, combat effects
- **LOD System**: Performance optimization for large world

### Target Performance
- **PC Target**: 60 FPS @ 1080p (Medium settings)
- **High-end PC**: 60 FPS @ 4K (Ultra settings)
- **Scalable Settings**: Low to Ultra presets

## Technical Implementation

### Core Technologies
- Unity Engine 2022.3 LTS
- Universal Render Pipeline (URP)
- C# Scripting
- Unity Input System (new)
- TextMeshPro for UI
- Cinemachine for camera
- Post-Processing Stack

### Code Architecture
- **Modular Design**: Separate systems
- **ScriptableObjects**: For data management
- **Object Pooling**: For performance
- **Event System**: For decoupled communication
- **Save System**: JSON serialization
- **Scene Management**: Additive scene loading

## Development Roadmap

### Phase 1: Core Systems ✓
- Player movement and controls
- Camera system
- Basic combat
- Climbing mechanic
- Stamina system

### Phase 2: Combat & Magic
- Full combat system with combos
- Magic system implementation
- Weapon variety
- Enemy AI basics

### Phase 3: Content Creation
- All 40 enemy types
- 10 boss encounters
- World building
- Puzzle mechanics

### Phase 4: Systems Integration
- Weather system
- Save/Load system
- UI implementation
- Audio integration

### Phase 5: Polish & Optimization
- Graphics optimization
- Bug fixing
- Balancing
- Final testing
