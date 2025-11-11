# Guía de Assets Placeholder - Islas Apocalipsis

## 🎯 Objetivo
Esta guía te ayudará a configurar el proyecto con assets placeholder gratuitos del Unity Asset Store para empezar a desarrollar y probar el juego inmediatamente.

## 📦 Assets Recomendados del Unity Asset Store (GRATIS)

### 1. Personaje del Jugador
**Opción 1: Starter Assets - Third Person Character Controller**
- URL: https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526
- Incluye: Personaje, animaciones, controles
- **Cómo usar**: Reemplaza el PlayerController con el personaje incluido

**Opción 2: Mixamo Characters** (Requiere cuenta gratuita)
- URL: https://www.mixamo.com/
- Incluye: Personajes rigged y animaciones
- Descarga formato: FBX for Unity

### 2. Enemigos Placeholder
**Low Poly Enemies**
- URL: https://assetstore.unity.com/packages/3d/characters/creatures/quirky-series-free-animals-pack-178235
- Alternativa: https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-duo-pbl-hp-polyart-157762

**Simple Capsule Enemies** (Sin Asset Store)
- Usa primitivos de Unity (Capsules con colores)
- Rápido para prototipar

### 3. Entorno y Mundo
**Nature Starter Kit 2**
- URL: https://assetstore.unity.com/packages/3d/environments/nature-starter-kit-2-52977
- Incluye: Árboles, rocas, terreno, agua

**Low Poly Simple Nature Pack**
- URL: https://assetstore.unity.com/packages/3d/environments/lowpoly-environment-pack-99479

### 4. Efectos de Magia y Partículas
**Cartoon FX Free**
- URL: https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-free-109565
- Incluye: Efectos de fuego, hielo, explosiones

**Magic Effects FREE**
- URL: https://assetstore.unity.com/packages/vfx/particles/spells/magic-effects-free-247933

### 5. Armas
**Low Poly Weapons**
- URL: https://assetstore.unity.com/packages/3d/props/weapons/low-poly-weapons-73339

**Simple Weapon Models** (Sin Asset Store)
- Usa primitivos: Capsules para espadas, Cubes para escudos

### 6. UI
**UI Samples**
- URL: https://assetstore.unity.com/packages/2d/gui/icons/simple-button-set-01-153979
- Incluye: Botones, iconos básicos

**Alternativa**: Usa UI Toolkit de Unity (incluido por defecto)

### 7. Sonido y Música
**Free Sound Effects Pack**
- URL: https://assetstore.unity.com/packages/audio/sound-fx/free-sound-effects-pack-155776

**Free Music Pack**
- URL: https://assetstore.unity.com/packages/audio/music/free-music-pack-104396

---

## 🚀 Configuración Rápida (Sin Asset Store)

### Opción 1: Usando Primitivos de Unity

Crea esta estructura en Unity:

#### Player
```
1. GameObject → 3D Object → Capsule
2. Nombrar: "Player"
3. Agregar componentes:
   - Character Controller
   - PlayerController script
   - PlayerStats script
   - ClimbingSystem script
   - CombatSystem script
4. Crear hijo: Capsule pequeño para "GroundCheck"
```

#### Enemigo Básico
```
1. GameObject → 3D Object → Capsule (color rojo)
2. Nombrar: "CorruptedScout"
3. Agregar componentes:
   - NavMesh Agent
   - EnemyAI script
   - EnemyHealth script
4. Crear Material rojo para diferenciarlo
```

#### Arma Simple
```
1. GameObject → 3D Object → Cube (escalar: 0.1, 1.0, 0.1)
2. Nombrar: "BasicSword"
3. Posicionar en mano del jugador
```

---

## 📋 Guía de Instalación de Assets

### Paso 1: Abrir Unity Asset Store
1. Abre Unity
2. Ve a Window → Asset Store (o usa el navegador)
3. Busca el asset que necesitas

### Paso 2: Importar Asset
1. Click en "Add to My Assets"
2. Abre Package Manager (Window → Package Manager)
3. Cambia a "My Assets"
4. Encuentra el asset y click "Download"
5. Click "Import"

### Paso 3: Configurar Asset
1. Lee el README del asset (si existe)
2. Busca las escenas de ejemplo
3. Extrae los prefabs que necesitas
4. Adapta los scripts si es necesario

---

## 🎮 Configuración de Escena Inicial

### Crear Primera Escena Jugable

#### 1. Crear Escena
```
File → New Scene → 3D (URP)
Guardar como: Assets/Scenes/TestScene.unity
```

#### 2. Configurar Iluminación
```
GameObject → Light → Directional Light
Rotar: (50, -30, 0)
Intensidad: 1
```

#### 3. Crear Terreno Simple
```
GameObject → 3D Object → Plane
Escalar: (10, 1, 10)
Nombrar: "Ground"
```

#### 4. Agregar Jugador
```
GameObject → Create Empty → "Player"
Agregar: Character Controller
Agregar: PlayerController script
Posición: (0, 2, 0)

Hijo: Capsule para representación visual
Hijo: Empty para "GroundCheck" (0, -1, 0)
```

#### 5. Agregar Cámara
```
GameObject → Create Empty → "CameraSystem"
Agregar: CameraController script
Hijo: Main Camera
Configurar target → Player
```

#### 6. Agregar Game Managers
```
GameObject → Create Empty → "GameManager"
Agregar: GameManager script

GameObject → Create Empty → "UIManager"
Agregar: UIManager script
Hijo: Canvas para UI
```

#### 7. Agregar Enemigo de Prueba
```
GameObject → 3D Object → Capsule → "Enemy"
Color: Rojo
Agregar: NavMesh Agent
Agregar: EnemyAI script
Agregar: EnemyHealth script
Posición: (5, 1, 5)
```

#### 8. Configurar NavMesh
```
Window → AI → Navigation
Selecciona "Ground"
En Navigation window: Object → Navigation Static ✓
Bake tab → Bake
```

---

## 🎨 Materiales Placeholder

Crea materiales básicos para diferenciar objetos:

### En Unity:
```
Assets → Create → Material

Jugador: Material azul (0, 0.5, 1)
Enemigos: Material rojo (1, 0, 0)
Suelo: Material verde (0.3, 0.6, 0.3)
Paredes escalables: Material marrón (0.5, 0.3, 0.1)
```

---

## 📝 Lista de Verificación de Setup

### Básico (Prioridad Alta)
- [ ] Player con Character Controller
- [ ] Cámara siguiendo al jugador
- [ ] Terreno básico para moverse
- [ ] 1 enemigo con IA básica
- [ ] NavMesh configurado
- [ ] UI básica (Health bar)

### Intermedio (Prioridad Media)
- [ ] 3-5 tipos de enemigos
- [ ] Arma equipada visible
- [ ] Efectos de partículas para magia
- [ ] Sonidos básicos
- [ ] Sistema de guardado funcional

### Avanzado (Prioridad Baja)
- [ ] 10+ enemigos
- [ ] 1 boss funcional
- [ ] Múltiples áreas/regiones
- [ ] Sistema de clima
- [ ] Puzzles implementados

---

## 🔧 Configuración de Prefabs

### Crear Prefabs Reutilizables

#### Player Prefab
```
1. Configura completamente el Player en la escena
2. Arrastra Player a Assets/Prefabs/Characters/
3. Ahora puedes instanciarlo en cualquier escena
```

#### Enemy Prefab
```
1. Configura un enemigo completamente
2. Arrastra a Assets/Prefabs/Enemies/
3. Duplica y modifica para crear variantes
```

#### Weapon Prefab
```
1. Crea arma visual
2. Agregar colliders si necesario
3. Guardar en Assets/Prefabs/Items/
```

---

## 🎯 Próximos Pasos

### Semana 1: Setup Básico
1. Importar Starter Assets o crear con primitivos
2. Configurar primera escena jugable
3. Probar movimiento del jugador
4. Agregar 1 enemigo funcional

### Semana 2: Mecánicas Core
1. Implementar combate básico
2. Agregar un spell simple (fireball)
3. Configurar UI básica
4. Sistema de guardado funcionando

### Semana 3: Contenido
1. Agregar 5 enemigos diferentes
2. Crear arma con stats
3. Implementar clima básico
4. Crear área de inicio

### Semana 4: Pulido
1. Efectos visuales
2. Sonidos básicos
3. Balance inicial
4. Primera versión jugable

---

## 💡 Consejos Importantes

### Performance
- Usa Level of Detail (LOD) para modelos complejos
- Limita partículas activas simultáneas
- Usa Object Pooling para enemigos
- Profile regularmente con Unity Profiler

### Organización
- Nombra todo consistentemente
- Usa prefabs para todo lo reutilizable
- Mantén la jerarquía limpia
- Comenta tu código

### Testing
- Prueba en Play Mode frecuentemente
- Guarda antes de hacer cambios grandes
- Usa Git para control de versiones
- Build el juego regularmente

---

## 🆘 Solución de Problemas

### "Character Controller no se mueve"
- Verifica que el script PlayerController esté asignado
- Comprueba que la cámara esté referenciada
- Asegúrate que el Input System esté configurado

### "Enemigo no persigue al jugador"
- Verifica que el NavMesh esté baked
- Comprueba que el enemigo tenga NavMesh Agent
- Asegura que el Player tenga tag "Player"

### "UI no se ve"
- Verifica que Canvas esté en modo Screen Space
- Comprueba que EventSystem exista en la escena
- Asegura que los elementos UI tengan RectTransform

### "Scripts no compilan"
- Revisa errores en Console
- Verifica que todos los scripts tengan namespace correcto
- Asegura que las referencias sean correctas

---

## 📚 Recursos Adicionales

### Tutoriales Unity
- Unity Learn: https://learn.unity.com/
- Brackeys (YouTube): Tutoriales básicos excelentes
- GameDev.tv: Cursos completos

### Comunidad
- Unity Forums: https://forum.unity.com/
- Reddit r/Unity3D: Comunidad activa
- Discord de Unity

### Assets Gratuitos
- itch.io: Miles de assets gratuitos
- OpenGameArt: Sprites y modelos
- Freesound: Efectos de sonido

---

**¡Listo para empezar!** 🎮

Comienza con la configuración básica usando primitivos, y gradualmente reemplázalos con assets más detallados según tu progreso.
