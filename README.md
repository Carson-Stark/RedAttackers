# Red Attackers

![hero](https://github.com/user-attachments/assets/522a5feb-c814-4db0-b4ff-66cf985aa474)

## Overview

Red Attackers is a single-player 3D tower defense game where players defend against waves of red cubes using a variety of placeable defenses. The game combines FPS mechanics with classic tower defense gameplay, featuring a first-person character controller, enemy waypoint system, progressive wave spawning, and 8 unique tower types.

This Unity 2020.3 LTS project was developed between **May 2015 and August 2016** as a learning exercise in game development and 3D asset creation. This was one of my earliest game development projects, built while I was learning Unity and C# from the ages of 11-12.

## Features

- First-person 3D character controller for shooting enemies
- Waypoint-based AI movement system for enemies
- Wave spawning with progressive difficulty scaling
- 8 unique towers with enemy tracking and targeting
- In-game economy for purchasing and upgrading towers
- Custom 3D models and textures for towers
- Functional main menu with settings and game save system
- Mobile-optimized performance and controls
- Tutorial for onboarding new players

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/Carson-Stark/RedAttackers.git
    ```
2. Open the project in **Unity 2020.3 LTS** or newer.
3. Load the `mainMenu` scene located in `Assets/Scenes`.
4. Press `Play` to run the game in the editor or build for Android using `File -> Build Settings`.

## Towers Implemented

- **Turret**: A basic machine gun turret with a high fire rate and moderate damage.
- **Cannon Turret**: Fires at a lower rate but deals higher damage per shot.
- **Sniper Turret**: Features infinite range, very high damage, but a very low fire rate.
- **Flamethrower**: Applies continuous damage with a flame effect.
- **Grenade Launcher**: Launches grenades at the nearest enemy, dealing high area-of-effect (AoE) damage.
- **Air Command**: Calls in missile strikes at regular intervals, delivering very high AoE damage.
- **Rocket Launcher**: Fires a volley of four rockets at enemies, offering high range and explosive impact.
- **Sentry**: Combines the attributes of a simple turret and a cannon turret, providing high damage, a high fire rate, and medium range.

## Controls

- `WASD` - Move
- `Mouse` - Look around
- `Left Click` - Shoot
- `B` - Open build menu
- `T` - Enter top-down view
- `F` - Fast forward
- `P` - Pause
- `X` - Destroy defense

## Project Structure

The project is organized as follows:

- **Assets/**: Contains all the game assets, including scripts, scenes, prefabs, textures, and models. This is the main folder where development occurs.
  - **EffectExamples/**: Example effects used in the game.
  - **Modern Weapons Pack/**: Contains weapon models from free asset pack.
  - **Scenes/**: Includes all the game scenes, such as the main menu and gameplay levels.
  - **Standard Assets/**: Unity's standard assets used in the project.
- **Packages/**: Manages Unity packages used in the project. This folder is automatically handled by Unity.
- **ProjectSettings/**: Includes settings for the Unity project, such as input configurations, tags, and layers.
