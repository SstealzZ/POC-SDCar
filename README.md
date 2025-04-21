# Self-Driving Car Simulation

## Overview
This project implements a 2D self-driving car simulation using Unity. It demonstrates fundamental autonomous vehicle behaviors such as lane detection, obstacle avoidance, adaptive speed control, and decision-making algorithms.

## Key Features
- **Intelligent Lane Changing**: The vehicle analyzes traffic patterns and makes optimal lane change decisions
- **Dynamic Obstacle Detection**: Uses raycasting to detect and respond to obstacles in real-time
- **Adaptive Speed Control**: Vehicle speed adjusts based on traffic density and obstacle proximity
- **Simulated Traffic Environment**: Random traffic generation creates unpredictable scenarios
- **Performance Metrics**: Real-time statistics including distance traveled, speed, and collision risk

## Technical Architecture
The simulation is built on the following components:

- **CarIA.cs**: Core self-driving AI that handles decision-making logic
- **SpawnManager.cs**: Controls the spawning of enemy vehicles to create dynamic traffic scenarios
- **EnemyCar.cs**: Manages behavior of obstacle vehicles
- **CarStatsUI.cs**: Provides real-time telemetry and performance data
- **RouteScroller.cs**: Handles environment scrolling to create movement illusion

## Getting Started
1. Open the project in Unity 2021.3 or newer
2. Load the GameScene located in the Assets/Scenes directory
3. Press Play to run the simulation
4. Observe how the self-driving car navigates through traffic

## Development Guidelines
- All code is documented using standard XML documentation comments
- Variables and methods use descriptive names to enhance readability
- Complex algorithms are broken down into smaller, manageable functions
- Core AI logic is separated from visualization and UI components

## Future Enhancements
- Machine learning integration for improved decision-making
- Additional sensor types (LIDAR simulation, camera vision)
- Variable weather and road conditions
- Multiple vehicle types with different behavior characteristics
- More complex road networks including intersections and merging lanes

## Project Structure
```
Assets/
├── Scenes/               # Contains the main game scene and scripts
│   ├── CarIA.cs          # Self-driving car AI logic
│   ├── SpawnManager.cs   # Traffic generation system
│   ├── EnemyCar.cs       # Obstacle vehicle behavior
│   ├── CarStatsUI.cs     # Telemetry and UI management
│   ├── RouteScroller.cs  # Environment movement control
│   └── GameScene.unity   # Main simulation scene
├── Prefabs/              # Reusable game objects
└── Settings/             # Project configuration files
``` 