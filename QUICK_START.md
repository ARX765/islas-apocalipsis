# Inicio Rápido con Placeholders - Islas Apocalipsis

## 🎮 Objetivo: Tener el juego funcionando en 30 minutos

Este tutorial te llevará desde cero hasta tener un nivel jugable con placeholders.

---

## ⚡ Setup Ultra Rápido (15 minutos)

### 1. Abrir Unity (2 min)
```
1. Abre Unity Hub
2. Agrega el proyecto: "Add" → Selecciona carpeta islas-apocalipsis
3. Haz clic para abrir (primera vez tarda 5-10 min importando)
```

### 2. Crear Primera Escena (3 min)
```
1. File → New Scene → 3D (URP)
2. Guardar: Assets/Scenes/TestScene.unity
```

### 3. Usar el Helper Automático (5 min)

**IMPORTANTE**: Unity debe estar abierto y la escena creada.

En el menú de Unity:
```
Islas Apocalipsis → Setup → Create Basic Test Scene
```

Esto crea automáticamente:
- ✅ Suelo (Plane verde de 100x100m)
- ✅ Luz direccional
- ✅ Jugador con todos los scripts
- ✅ Sistema de cámara
- ✅ Managers del juego
- ✅ Un enemigo de prueba

### 4. Configurar NavMesh (3 min)
```
1. Window → AI → Navigation
2. Selecciona "Ground" en Hierarchy
3. En Navigation window: Object tab → ✓ Navigation Static
4. Bake tab → Click "Bake"
```

### 5. Configurar Referencias (2 min)

**En Player**:
1. Selecciona "Player" en Hierarchy
2. En Inspector, busca "Player Controller":
   - Ground Check: Arrastra "GroundCheck" (hijo de Player)
   - Camera Transform: Arrastra "Main Camera"

**En CameraSystem**:
1. Selecciona "CameraSystem"
2. En Inspector, busca "Camera Controller":
   - Target: Arrastra "Player"

**En CombatSystem (en Player)**:
1. Attack Point: Arrastra "AttackPoint"

### 6. ¡Jugar! (Inmediato)
```
Presiona Play ▶️
Usa WASD para moverte
```

---

## 🎯 Setup Completo (30 minutos)

### Paso 1: Agregar UI Básica (5 min)

#### Crear Health Bar

1. **Selecciona Canvas** (dentro de UIManager)

2. **Crear Panel para HUD**:
```
Click derecho en Canvas → UI → Panel
Nombre: "HUD"
Anchor: Stretch (esquinas)
Color: Transparente o semi-transparente
```

3. **Crear Health Bar**:
```
Click derecho en HUD → UI → Slider
Nombre: "HealthBar"
Anchor: Top Left
Position: (100, -50)
Size: (200, 20)
```

4. **Configurar Health Bar**:
```
- Slider:
  - Min Value: 0
  - Max Value: 1
  - Value: 1
  
- Fill Area → Fill:
  - Color: Rojo (#FF0000)

- Background:
  - Color: Gris oscuro (#333333)
```

5. **Conectar con UIManager**:
```
Selecciona UIManager
En Inspector:
- Health Bar: Arrastra el Slider "HealthBar"
```

Repite para Stamina Bar (Color verde) y Mana Bar (Color azul).

### Paso 2: Agregar Más Enemigos (3 min)

```
1. Selecciona el enemigo "CorruptedScout"
2. Ctrl+D para duplicar
3. Mover a diferentes posiciones (7, 1, 3), (-5, 1, 8), etc.
4. Cambiar enemyType en cada uno para variedad
```

### Paso 3: Agregar Armas Visuales (5 min)

#### Crear Espada Simple

```
Islas Apocalipsis → Setup → Create Simple Weapon
```

Esto crea un cubo como espada. Ahora:

```
1. Selecciona "BasicSword"
2. Arrástralo como hijo de Player → RightHandSlot
3. Posición local: (0, 0, 0)
4. Rotación: (0, 0, 45)
```

### Paso 4: Agregar Efectos de Partículas (5 min)

#### Efecto de Fuego para Fireball

```
1. Hierarchy → Click derecho → Effects → Particle System
2. Nombre: "FireballEffect"
3. Configurar:
   - Start Color: Rojo/Naranja
   - Start Size: 0.5
   - Start Lifetime: 2
   - Emission: Rate over Time: 20
   
4. Guardar como Prefab: Arrastrar a Assets/Prefabs/
5. Desactivar en scene
```

### Paso 5: Crear Zona Escalable (5 min)

```
1. GameObject → 3D Object → Cube
2. Nombre: "ClimbableWall"
3. Escala: (10, 5, 1)
4. Posición: (0, 2.5, 10)
5. Material: Marrón
6. Layer: Crear nuevo layer "Climbable"
7. En PlayerController/ClimbingSystem:
   - Climbable Mask: Seleccionar "Climbable"
```

### Paso 6: Agregar Puzzle Simple (5 min)

#### Crear Pressure Plate

```
1. GameObject → 3D Object → Cylinder
2. Nombre: "PressurePlate"
3. Escala: (2, 0.1, 2)
4. Agregar: Box Collider → Is Trigger ✓
5. Agregar: Puzzles/PressurePlate script
6. Configurar: Required Weight: 50
```

#### Crear Puerta Puzzle

```
1. GameObject → 3D Object → Cube
2. Nombre: "PuzzleDoor"
3. Escala: (4, 5, 0.5)
4. Posición: (0, 2.5, 15)
5. Agregar: Puzzles/PuzzleDoor script
6. En Inspector:
   - Required Elements: Size 1
   - Element 0: Arrastra "PressurePlate"
   - Open Height: 5
```

### Paso 7: Configurar Clima (2 min)

```
1. Selecciona WeatherSystem
2. En Inspector:
   - Current Weather: Rain
   - Weather Intensity: 0.5
   - Random Weather: ✓
   - Affects Gameplay: ✓
```

---

## 🎨 Mejorar Visuales (Opcional, 10 min)

### Agregar Skybox

```
1. Window → Rendering → Lighting
2. Environment tab
3. Skybox Material: Selecciona uno predeterminado o descarga gratis
```

### Mejorar Iluminación

```
1. Selecciona Directional Light
2. Intensity: 1.5
3. Color: Ligeramente amarillo para luz de día
4. Shadows: Soft Shadows
```

### Post-Processing

```
1. Crear: GameObject → Volume → Global Volume
2. Profile: New
3. Add Override → Bloom
4. Add Override → Color Adjustments
5. Ajustar al gusto
```

---

## 🎮 Controles del Juego

### Teclado y Mouse
```
WASD         - Moverse
Mouse        - Mirar alrededor
Espacio      - Saltar
Shift        - Correr
E            - Interactuar / Escalar
Q            - Parar (cuando escalando)
Click Izq    - Ataque ligero
Click Der    - Ataque pesado
1-8          - Lanzar hechizos
Tab          - Lock-on
I            - Inventario (si UI existe)
M            - Mapa
ESC          - Pausa
```

---

## ✅ Checklist de Verificación

Antes de continuar, verifica que funcione:

### Jugador
- [ ] Se mueve con WASD
- [ ] Salta con Espacio
- [ ] Corre con Shift (barra de stamina baja)
- [ ] Health bar se ve en pantalla

### Cámara
- [ ] Sigue al jugador
- [ ] Rota con mouse
- [ ] No atraviesa objetos

### Enemigo
- [ ] Se mueve hacia el jugador
- [ ] Ataca cuando está cerca
- [ ] Recibe daño (usa debug log)

### Sistemas
- [ ] GameManager existe y funciona
- [ ] UIManager muestra HUD
- [ ] No hay errores en Console

---

## 🐛 Solución de Problemas Comunes

### "Player no se mueve"
```
✓ Verificar que Character Controller esté añadido
✓ Verificar que PlayerController script esté asignado
✓ Verificar que cameraTransform esté referenciado
✓ Comprobar Console por errores
```

### "Enemigo no persigue"
```
✓ NavMesh debe estar baked (verde en Scene view)
✓ Enemy debe tener NavMesh Agent
✓ Player debe tener tag "Player"
✓ Enemy layer debe estar en "Enemy" layer
```

### "UI no se ve"
```
✓ Canvas debe estar en Screen Space - Overlay
✓ EventSystem debe existir
✓ Canvas Scaler configurado
✓ Elementos UI dentro de Canvas
```

### "Scripts tienen errores"
```
✓ Esperar que Unity termine de importar
✓ Cerrar y reabrir Unity
✓ Assets → Reimport All (último recurso)
```

---

## 📚 Siguientes Pasos

### Para esta semana:
1. **Practica los controles**: Muévete, salta, ataca
2. **Experimenta**: Cambia colores, posiciones, valores
3. **Agrega contenido**: Más enemigos, armas, áreas
4. **Lee la documentación**: GAME_DESIGN.md, ENEMIES.md

### Para próxima semana:
1. **Importar assets del Asset Store** (ver PLACEHOLDER_ASSETS_GUIDE.md)
2. **Crear segunda área** con diferentes enemigos
3. **Implementar un boss simple**
4. **Agregar más spells y armas**

---

## 🎯 Metas del Primer Mes

- [ ] Área jugable completa (500m²)
- [ ] 5 tipos de enemigos funcionando
- [ ] 3 armas diferentes
- [ ] 3 hechizos funcionando
- [ ] 1 boss básico
- [ ] UI completa (health, stamina, mana)
- [ ] Sistema de guardado funcionando
- [ ] Clima dinámico activo

---

## 💡 Tips Finales

1. **Guarda frecuentemente**: Ctrl+S
2. **Haz backups**: Copia la carpeta del proyecto
3. **Usa Git**: Commit después de cada característica que funcione
4. **Testa en Play Mode**: Prueba cada cambio
5. **Lee la Console**: Los errores te dicen qué arreglar
6. **Empieza simple**: No intentes todo a la vez
7. **Documenta cambios**: Anota qué funciona y qué no
8. **Pide ayuda**: Usa forums, Discord, Reddit

---

## 🎮 ¡A Jugar!

Ahora tienes un nivel básico funcionando. Experimenta, aprende, y mejora gradualmente.

**Recuerda**: Este es un prototype. Lo importante es que funcione, no que se vea perfecto. Los gráficos vienen después.

**¡Diviértete creando tu juego!** 🚀
