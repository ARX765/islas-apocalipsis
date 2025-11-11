# Technical Specifications - Islas Apocalipsis

## Engine & Tools

### Unity Configuration
- **Version**: Unity 2022.3 LTS
- **Render Pipeline**: Universal Render Pipeline (URP)
- **Scripting Backend**: IL2CPP
- **API Level**: .NET Standard 2.1
- **Color Space**: Linear

### Required Packages
```
com.unity.render-pipelines.universal (14.0+)
com.unity.textmeshpro (3.0+)
com.unity.inputsystem (1.5+)
com.unity.cinemachine (2.9+)
com.unity.postprocessing (3.2+)
com.unity.ai.navigation (1.1+)
```

## Platform Specifications

### Target Platforms
- **Primary**: Windows 10/11 (64-bit)
- **Secondary**: Linux (Ubuntu 20.04+)
- **Tertiary**: macOS 11+ (Apple Silicon & Intel)

### Minimum System Requirements
- **OS**: Windows 10 64-bit
- **Processor**: Intel Core i5-6600K / AMD Ryzen 5 1600
- **Memory**: 8 GB RAM
- **Graphics**: NVIDIA GTX 960 / AMD RX 470 (4GB VRAM)
- **DirectX**: Version 11
- **Storage**: 15 GB available space
- **Additional**: SSD recommended

### Recommended System Requirements
- **OS**: Windows 10/11 64-bit
- **Processor**: Intel Core i7-8700K / AMD Ryzen 7 2700X
- **Memory**: 16 GB RAM
- **Graphics**: NVIDIA RTX 2060 / AMD RX 5700 XT (8GB VRAM)
- **DirectX**: Version 12
- **Storage**: 15 GB SSD space
- **Additional**: Game controller optional

### Ultra Settings Requirements (4K)
- **Processor**: Intel Core i9-10900K / AMD Ryzen 9 5900X
- **Memory**: 32 GB RAM
- **Graphics**: NVIDIA RTX 3080 / AMD RX 6800 XT (10GB+ VRAM)
- **Storage**: 20 GB NVMe SSD space

## Graphics Settings

### Quality Presets

#### Low (720p, 30+ FPS)
- Shadow Quality: Low
- Texture Quality: Medium
- Anti-Aliasing: None
- Post-Processing: Minimal
- View Distance: 300m
- Particle Quality: Low
- Reflection Quality: Off

#### Medium (1080p, 60 FPS)
- Shadow Quality: Medium
- Texture Quality: High
- Anti-Aliasing: FXAA
- Post-Processing: Medium
- View Distance: 500m
- Particle Quality: Medium
- Reflection Quality: Low

#### High (1080p, 60+ FPS)
- Shadow Quality: High
- Texture Quality: High
- Anti-Aliasing: TAA
- Post-Processing: High
- View Distance: 750m
- Particle Quality: High
- Reflection Quality: Medium

#### Ultra (1440p/4K, 60+ FPS)
- Shadow Quality: Ultra
- Texture Quality: Ultra
- Anti-Aliasing: TAA + MSAA x2
- Post-Processing: Ultra
- View Distance: 1000m
- Particle Quality: Ultra
- Reflection Quality: High
- Ray Tracing: Optional (RTX only)

### Resolution Support
- 1280x720 (HD)
- 1920x1080 (Full HD)
- 2560x1440 (2K)
- 3840x2160 (4K)
- Ultrawide: 21:9, 32:9 support
- Windowed, Borderless, Fullscreen

### Graphics Features
- **Lighting**: Real-time GI with light probes
- **Shadows**: Cascaded shadow maps, 4 cascades
- **Post-Processing**: 
  - Bloom
  - Ambient Occlusion (SSAO)
  - Color Grading
  - Motion Blur (optional)
  - Depth of Field
  - Vignette
  - Lens Flare
- **Particle Systems**: GPU instanced
- **Weather Effects**: Volumetric fog, particle rain/snow
- **Water**: Reflection probes, caustics

## Performance Optimization

### Rendering Optimization
- **Occlusion Culling**: Enabled, baked per scene
- **LOD System**: 3-4 LOD levels per model
  - LOD0: 0-30m (full detail)
  - LOD1: 30-60m (reduced 50%)
  - LOD2: 60-100m (reduced 75%)
  - LOD3: 100m+ (simple geometry)
- **Draw Call Batching**: Static + Dynamic batching
- **Texture Streaming**: Mipmap streaming enabled
- **Mesh Combining**: Static objects merged

### Memory Management
- **Texture Compression**: BC7 (PC), ASTC (mobile prep)
- **Audio Compression**: Vorbis OGG
- **Asset Bundles**: Streamed loading for large areas
- **Object Pooling**: Enemies, projectiles, particles
- **Garbage Collection**: Incremental GC enabled

### CPU Optimization
- **Job System**: Multithreaded calculations
- **Burst Compiler**: Performance-critical code
- **NavMesh**: Baked navigation meshes
- **Physics**: Fixed timestep 50Hz
- **AI Update Rate**: Variable based on distance

### GPU Optimization
- **Shader Variants**: Only necessary variants
- **Texture Atlases**: Combined textures
- **GPU Instancing**: Enabled for repeated objects
- **Vertex Count**: <50k per model
- **Texture Resolution**: 
  - Characters: 2048x2048
  - Props: 512-1024x512-1024
  - Environment: 2048-4096x2048-4096

## Asset Specifications

### 3D Models
- **Format**: FBX, OBJ
- **Polygon Budget**:
  - Player: 15,000 tris
  - Common Enemy: 5,000-8,000 tris
  - Elite Enemy: 10,000-15,000 tris
  - Boss: 20,000-40,000 tris
  - Environment Props: 500-5,000 tris
- **Rigging**: Humanoid rig for characters
- **Animation**: 30-60 FPS keyframes

### Textures
- **Format**: PNG (source), BC7/DXT5 (compressed)
- **Resolutions**:
  - Albedo: 1024-2048px
  - Normal: 1024-2048px
  - Metallic/Roughness: 512-1024px
  - Emission: 512-1024px
- **Texture Types**: 
  - Albedo (RGB)
  - Normal (RGB)
  - Metallic (R), Occlusion (G), Roughness (B), Height (A)
  - Emission (RGB)

### Audio
- **Music**: 
  - Format: OGG Vorbis
  - Bitrate: 192 kbps
  - Stereo, 44.1kHz
- **Sound Effects**:
  - Format: WAV (source), OGG (runtime)
  - Bitrate: 128 kbps
  - Mono (3D sounds), Stereo (2D sounds)
  - 44.1kHz

### UI
- **Format**: PNG with alpha
- **Resolution**: 1024-2048px for atlas
- **TextMeshPro**: Vector fonts
- **Sprites**: Multiple resolutions (1x, 2x, 4x)

## Code Architecture

### Project Structure
```
Assets/
├── Scripts/
│   ├── Core/              # Singleton managers
│   ├── Player/            # Player-specific scripts
│   ├── Combat/            # Combat mechanics
│   ├── Magic/             # Magic system
│   ├── Enemies/           # Enemy AI
│   ├── Bosses/            # Boss controllers
│   ├── Items/             # Items and equipment
│   ├── UI/                # UI controllers
│   ├── Systems/           # Game systems
│   ├── Puzzles/           # Puzzle mechanics
│   └── Utilities/         # Helper scripts
```

### Coding Standards
- **Language**: C# 9.0+
- **Naming Convention**: PascalCase for public, camelCase for private
- **Namespaces**: `IslasApocalipsis.{Category}`
- **Comments**: XML documentation for public APIs
- **Architecture Pattern**: Component-based with managers

### Performance Guidelines
- Avoid `Update()` when possible, use events
- Cache component references in `Awake()`
- Use `CompareTag()` instead of string comparison
- Minimize allocations in hot paths
- Use object pooling for frequently spawned objects
- Profile regularly with Unity Profiler

## Input System

### Input Mapping
```
Keyboard & Mouse:
- WASD: Movement
- Mouse: Camera
- Space: Jump
- Shift: Sprint
- E: Interact/Climb
- Q: Parry
- Tab: Lock-on
- 1-8: Quick spells
- I: Inventory
- M: Map
- ESC: Pause
- Mouse1: Light Attack
- Mouse2: Heavy Attack/Cast

Controller (Xbox layout):
- Left Stick: Movement
- Right Stick: Camera
- A: Jump
- B: Dodge
- X: Light Attack
- Y: Heavy Attack
- LB: Parry
- RB: Lock-on
- LT: Block
- RT: Cast Spell
- D-Pad: Quick items
- Start: Pause
- Back: Map
```

### Input Customization
- All controls rebindable
- Multiple input profiles
- Sensitivity adjustments
- Dead zone settings
- Vibration intensity

## Networking (Future)
Not implemented in current version, but structured for:
- **Protocol**: Unity Netcode
- **Max Players**: 4 (co-op)
- **Connection**: P2P or dedicated server
- **Sync**: Player position, combat, world state

## Save System

### Save File Structure
```json
{
  "version": "1.0",
  "saveSlot": 0,
  "timestamp": "ISO-8601",
  "playTime": 3600,
  "player": {
    "position": [x, y, z],
    "rotation": [x, y, z, w],
    "stats": {...},
    "inventory": [...],
    "equipment": {...}
  },
  "world": {
    "currentScene": "scene_name",
    "defeatedBosses": [...],
    "discoveredLocations": [...],
    "flags": {...}
  }
}
```

### Save Locations
- **Windows**: `%USERPROFILE%/AppData/LocalLow/CompanyName/IslasApocalipsis/Saves/`
- **Linux**: `~/.config/unity3d/CompanyName/IslasApocalipsis/Saves/`
- **macOS**: `~/Library/Application Support/CompanyName/IslasApocalipsis/Saves/`

### Save Features
- 3 manual save slots
- Auto-save slot (slot 0)
- Cloud save support (Steam, Epic)
- Save backup system
- Corruption detection

## Build Configuration

### Build Settings
- **Compression**: LZ4 (faster load) or LZMA (smaller size)
- **Scripting Backend**: IL2CPP (performance)
- **Stripping Level**: Medium
- **Script Debugging**: Disabled in release
- **Development Build**: Disabled in release

### Build Sizes (Estimated)
- **Executable**: 50-100 MB
- **Game Data**: 2-5 GB
- **Total Install**: ~15 GB (with decompression)

## Testing Requirements

### Performance Testing
- Target: 60 FPS @ 1080p on recommended specs
- Memory: <4GB RAM usage
- Load times: <30s for any scene
- No frame drops during combat

### Compatibility Testing
- Windows 10/11 (different hardware configs)
- Various graphics cards (NVIDIA, AMD, Intel)
- Different resolutions and aspect ratios
- Controller compatibility (Xbox, PS, Switch Pro)

### Quality Assurance
- All 40 enemy types spawn correctly
- All 10 bosses function properly
- Save/load works reliably
- No game-breaking bugs
- Performance within targets

## Version Control

### Git Configuration
- **Ignore**: Unity Library, Temp, generated files
- **LFS**: Large assets (models, textures, audio)
- **Branching**: 
  - `main`: Stable releases
  - `develop`: Active development
  - `feature/*`: New features
  - `bugfix/*`: Bug fixes

### Asset Management
- Unity Asset Database
- Addressables for runtime loading
- Version control for all assets
- Asset bundle building for DLC

## Future Scalability

### Potential Expansions
- Additional regions (5-10 new islands)
- More bosses (5-10 additional)
- New enemy types (20+ more)
- Additional weapon types
- New magic schools
- Multiplayer support
- Mod support (Unity Mod Manager)
- DLC content structure

### Technical Debt Prevention
- Code reviews
- Documentation maintenance
- Regular refactoring
- Performance profiling
- Automated testing (where possible)
