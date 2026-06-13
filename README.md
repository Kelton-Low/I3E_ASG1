# I3E ASG1

A first-person game built in Unity 6 with a brainrot meme theme, featuring Italian Brainrot creatures, surreal environments, and a variety of gameplay mechanics.
I am not a brainrotted child I swear. Have fun grading this teacher

---

## Game Overview

The player explores two distinct environments — the eerie, infinite **Backrooms** and a **Crystal Wall** — collecting 67 Kids, interacting with doors, avoiding hazards, and using a gun to shoot crystals. The world is populated by Italian Brainrot creatures such as **Tung Tung Tung Sahur**, which roam the environment and chase the player on sight.

---

## Environments

- **The Backrooms** — A liminal, infinite indoor space with an unsettling atmosphere and grid lighting
- **War-torn Desert Ruins** — An outdoor environment with crumbling structures and hazards

---
## Gameplay Mechanics

### Player
- First-person movement using Unity's Starter Assets `FirstPersonController`
- Interact system using raycasts to collect coins, open doors, and shoot crystals
- Grappling hook — press a button to fire a grapple toward a surface and launch toward it
- Health system with damage from hazards and enemies
- External velocity system for push effects from enemy contact

### Collectibles
- **Coins** — Collected via raycast interaction, each with a configurable point value
- **Gun** — A special collectible that enables the shooting mechanic

### Doors
- **Swing Doors** — Open toward the player depending on which side they approach from, with an auto-close timer
- **Sliding Doors** — Trigger-based, open when the player enters and close when they leave

### Shooting
- Fires a raycast from the camera when the player has collected the gun
- Destroys crystals on hit

### Hazards
- Damage the player on contact using `OnTriggerStay`
- Configurable damage amount and damage tick rate

### Grid Lighting
- Procedurally spawned point lights in a grid pattern across a surface
- Configurable resolution, intensity, and color

### Grappling
- Fires raycast to point
- Moves the player towards that point
- Draws a line to visualise the grappling line

---

## Enemy AI — Tung Tung Tung Sahur

Enemies are physics-based capsule agents with no NavMesh dependency.

| Behaviour | Description |
|---|---|
| Wandering | Moves to random destinations within a set radius |
| Wall Avoidance | Detects walls ahead and turns 90 degrees to navigate around them |
| Vision | Raycast from eye position detects the player within a set range |
| Chasing | Moves toward the player for a set duration after spotting them |
| Push | Applies a force to the player on contact and deals damage |

---

## Scripts

| Script | Description |
|---|---|
| `playerCollider.cs` | Manages player health, shooting, gun beam line renderer, and coin/door interactions |
| `CoinValue.cs` | Collectible coin with configurable point value and gun unlock flag |
| `Hazard.cs` | Damages the player on contact with a configurable tick rate |
| `CloseDoor.cs` | Handles door closing logic and auto-close timer |
| `SwingDoor.cs` | Swing door that opens toward the player depending on approach side |
| `SlidingDoor.cs` | Trigger-based sliding door that opens and closes automatically |
| `TeleportTo.cs` | Teleports the player to a target location on collision or trigger |
| `TripleT.cs` | Enemy AI for Italian Brainrot creatures with wandering, vision, chasing and push |
| `GrapplingBeh.cs` | Raycast-based grapple that pulls the player toward a surface |
| `CreateGridL.cs` | Procedurally spawns a grid of point lights across a surface |
| `Outlin.cs` | Handles outline visual effect and reverses normal|
| `UIManager.cs` | Manages UI elements such as health display and score |

---

## Setup & Requirements

- **Unity Version:** Unity 6 (6000.3.10f1)
- **Input System:** Unity New Input System
- **Dependencies:** Unity Starter Assets (First Person), Cinemachine

### Build Settings
Ensure the following scenes are added under **File → Build Settings:**
- Scene 1 (main level)
- Scene 2 (grapple level)
- Scene 3 (win)

---

## Controls

| Action | Input |
|---|---|
| Move | WASD |
| Look | Mouse |
| Jump | Space |
| Sprint | Left Shift |
| Interact | E |
| Shoot | Left Click |
| Grapple | Left Click |

---

## Known Issues

- Non-convex Mesh Colliders may cause inconsistent collision detection on certain mesh shapes, so hitboxes are very different
- Enemy wall avoidance may occasionally get stuck in corners with multiple walls
- UI Buttons sometimes don't work

---

## Credits

Developed by Kelton — Ngee Ann Polytechnic, I3E Assignment 1
https://sketchfab.com/3d-models/dababy-7caf32915ca340148115c815e63bf67f#download
https://sketchfab.com/3d-models/67-kid-54009a9274264389b64198dc57061f75#download
https://sketchfab.com/3d-models/tung-tung-sahur-8f6cd020736f4510a260694cfd323cf9
