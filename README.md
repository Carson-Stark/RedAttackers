# Red Attackers

Single-player 3D tower defense game where the player defends against waves of red cubes using a variety of placeable defenses. The game features a first-person character controller, enemy waypoint system, wave spawning with progressive difficulty, and 8 unique tower types.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Installation](#installation)
- [Controls](#controls)
- [Project Structure](#project-structure)
- [Demo Video](#demo-video)
- [License](#license)

---

## Overview

**Red Attackers** is a **Unity 2020.3 LTS** project developed between **May 2015 - August 2016** as a learning exercise in game development and 3D asset pipelines. The game blends FPS and tower defense mechanics with the intention of targeting mobile platforms. This was one of my earliest game development projects, built while I was learning Unity and C# from the ages of 11-12.

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

---

**Built with:**
- **C#**, **Unity 2020.3 LTS**, **Blender**, **Photoshop**

---

**Enjoy defending against waves of red attackers!**