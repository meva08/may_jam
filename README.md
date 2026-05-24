# 🕯️ The Light Keeper

> *A top-down survival game where you race against the darkness to collect enough light to power a mysterious prism before your lantern fades forever.*

Play and Download on Itch.io (compatible with Chrome): https://meva08.itch.io/the-light-keeper
---

## About

*Fill the prism in the center with the fireflies that litter the library, brave Keeper. Be careful! Those same fireflies are the lifeblood that keep the light and keep your soul. Without light, you will be lost.*

The Light Keeper is a 2D survival game where you must collect light from fireflies around the map to fill the center prism while taking care not to lose all of your own light. Navigate your way through a dark, mysterious library to fulfill your duty as the Light Keeper.
Your goal: collect enough light to power the ancient prism at the center of the map before your lantern goes out forever.

---

## Features

- **Shrinking light mechanic** — your light radius constantly decreases, creating a tense timer
- **Light collectibles** — hold E near collectibles to absorb their energy and restore your radius
- **Enemy AI** — enemies patrol the darkness and chase you when you enter their range, draining your light on contact
- **Central prism** — hold E near the prism to transfer your light into it; fill it above the threshold to win
- **Quadrant-based map** — a 40x40 tilemap divided into four quadrants, each with randomly spawned collectibles and enemies
- **Firefly particle trail** — atmospheric firefly particles follow your cursor
- **Directional animations** — full 4-directional walk and idle animations for the player and enemies
- **Main menu, win and lose screens** — complete game flow with scene transitions and a death animation

---

## Controls

| Input | Action |
|---|---|
| WASD/Arrow Keys | Move |
| E (hold) | Collect light / Transfer to prism |

---

## Built With

- **Unity 6** (URP 2D)
- **C#**
- **Unity Input System**
- **Unity UI Toolkit / uGUI**
- **Unity Tilemaps**
- **Unity Particle System**

---

## Made By

- **Samuel Johnson-Noya** (Art & Music)
- **Maryeva Gonzalez** (Programming)

---

## How to Play

1. Clone or download the repository
2. Open the project in **Unity 6**
3. Open the `MainMenu` scene from `Assets/Scenes/`
4. Press Play

Or play the web build/download for Windows at: *[https://meva08.itch.io/the-light-keeper]*

---

## Project Structure

```
Assets/
├── Scenes/          # MainMenu, MainScene, WinScene, LoseScene
├── Scripts/         # All C# game scripts
├── Sprites/         # Player, enemy, collectible, prism art
├── Tiles/           # Tilemap tile assets
├── Fonts/           # Cinzel Decorative and other fonts
├── Audio/           # Music and sound effects
├── Animations/      # Animator controllers and animation clips
└── Settings/        # URP renderer and post processing
```

---
