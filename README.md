# AR Room Planner

I built this Android AR app to solve a problem I actually ran into: moving furniture around a room to see what works, then moving it all back because nothing fit right. AR Room Planner lets you place, move, rotate, scale, and delete virtual furniture in your real space using your phone camera, so you can test layouts before lifting a single piece of furniture.

I built this entire project from zero.

## Table of Contents

- [Features](#features)
- [How It Works](#how-it-works)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [License](#license)

## Features

- **AR Plane Detection** - Detects flat real-world surfaces in real time and shows a crosshair where furniture will land.
- **Furniture Selection UI** - A scrollable panel with furniture thumbnails, built dynamically at runtime from ScriptableObject data.
- **Tap to Select and Deselect** - Tap any placed object to select it. Tap empty space to deselect.
- **Drag to Move** - Drag a selected object across the detected plane in real time.
- **Two-Finger Rotation** - Twist two fingers to rotate the selected object.
- **Pinch to Scale** - Pinch to resize the selected object, clamped within sensible limits so it cannot get absurdly small or large.
- **Delete Button** - Appears only when something is selected, so you always know exactly what you are about to remove.
- **Instruction Overlay** - A full-screen guide shown at launch explaining every gesture, with a Start button that begins the AR session.

## How It Works

1. The app opens with an instruction overlay explaining how everything works.
2. You tap Start, and the AR session begins.
3. Point your camera at a flat surface and the app detects it within seconds.
4. Pick a furniture item from the bottom panel.
5. Line up the crosshair and tap to place it.
6. Tap any placed object to select it, then move, rotate, scale, or delete it.

## Tech Stack

| Tool / Package | What It Does Here |
|---|---|
| Unity 6 LTS | Core engine running the whole project |
| C# | Primary programming language for all game logic |
| AR Foundation | Detects planes and handles AR raycasting |
| XR Subsystems | Provides the trackable types behind the AR detection |
| Google ARCore | AR tracking and motion sensing on Android |
| Unity Input System (new) | Handles all touch input through Touchscreen.current |
| Google Cloud Storage | Hosts and stores furniture 3D models and images |
| DOTween | Powers the smooth button scale animations in the UI |
| TextMeshPro | Renders all the UI text |
| ScriptableObjects | Stores furniture data so new items can be added without writing code |
| Android Logcat | Used to debug directly on the phone during development |

## Project Structure

Assets/
- Scripts/
  - PlaceOnPlane.cs (Plane detection, crosshair, furniture spawning)
  - SelectionManager.cs (Tracks whatever object is currently selected)
  - DragObject.cs (Handles dragging the selected object)
  - RotateObject.cs (Two-finger rotation gesture)
  - ScaleObject.cs (Pinch-to-scale gesture)
  - DeleteObject.cs (Delete button logic)
  - DataHandler.cs (Loads furniture items and builds the selection UI)
  - ButtonManager.cs (Behaviour for each individual furniture button)
  - InstructionOverlay.cs (Onboarding screen and Start button)
- Resources/
  - Items/ (Furniture ScriptableObject assets)
- Prefabs/ (Furniture prefabs, each with a collider attached)
- Scenes/
  - SampleScene.unity

## Getting Started

### What You Need

- Unity 6 LTS or a compatible version
- An Android phone with ARCore support
- USB debugging turned on if you want to build and run directly to the device

### Setup

1. Clone this repo: git clone https://github.com/devExponent/AR-Room-Planner.git
2. Open the project in Unity Hub.
3. Open Assets/Scenes/SampleScene.unity.
4. To test on your own phone, connect it via USB and go to File then Build and Run.
5. If you just want the installable file without connecting a device, go to File then Build Settings, click Build, and save the APK. You can then transfer that file to any Android phone and install it directly.



## License

MIT License. See the LICENSE file for details.
