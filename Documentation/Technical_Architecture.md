# Self-Driving Car Simulation: Technical Architecture

## Project Overview
This document outlines the technical architecture of the Self-Driving Car Simulation project, explaining how the different components interact and the design patterns implemented.

## Core Architecture

### 1. Component-Based Design
The simulation follows Unity's component-based architecture, with each behavior encapsulated in specialized components:

```
GameObject (Car)
├── Transform
├── CarIA (Intelligence)
└── Collider2D (Physics interaction)

GameObject (UI)
└── CarStatsUI (Telemetry display)

GameObject (Road)
└── RouteScroller (Environment movement)

GameObject (SpawnPoint)
└── SpawnManager (Traffic generation)
```

### 2. Scene Structure
The main scene hierarchy is organized as follows:

```
GameScene
├── Player
│   └── Car (with CarIA component)
├── Environment
│   ├── Road
│   └── Boundaries
├── SpawnSystem
│   └── SpawnPoints
├── UI
│   ├── Speed Display
│   ├── Distance Counter
│   └── Collision Warning
└── GameManager
```

### 3. Data Flow
The data flow between components follows this pattern:

1. **Perception**: CarIA uses raycasting to gather environmental data
2. **Decision**: CarIA processes this data to make movement decisions
3. **Action**: CarIA applies movement to the car transform
4. **Feedback**: CarStatsUI reads data from CarIA to update the UI
5. **Environment**: RouteScroller and SpawnManager create the dynamic environment

## Component Details

### CarIA Component
- **Responsibility**: Core intelligence of the self-driving car
- **Dependencies**: None
- **Provides**: Speed data, position data, decision-making logic

### SpawnManager Component
- **Responsibility**: Generating traffic patterns
- **Dependencies**: EnemyCar prefab
- **Provides**: Dynamic obstacle creation

### RouteScroller Component
- **Responsibility**: Creating illusion of movement
- **Dependencies**: CarIA (for speed synchronization)
- **Provides**: Visual feedback of car movement

### CarStatsUI Component
- **Responsibility**: Displaying telemetry data
- **Dependencies**: CarIA (for data source)
- **Provides**: Visual representation of car performance

### EnemyCar Component
- **Responsibility**: Obstacle vehicle behavior
- **Dependencies**: None
- **Provides**: Moving obstacles for the AI to avoid

## Technical Implementation

### 1. Physics System
The simulation uses Unity's 2D physics system:

- **Colliders**: Box2D colliders for vehicles
- **Raycasting**: Used for obstacle detection
- **Layers**: Custom layers for efficient collision detection
  - Layer 0: Default
  - Layer 8: Player
  - Layer 9: Enemy
  - Layer 10: Environment

### 2. Input Handling
The current implementation is fully autonomous with no user input required during simulation. Future versions may implement:

- Manual override controls
- Simulation parameter adjustment during runtime
- Camera control options

### 3. Performance Optimizations
Several optimizations ensure smooth performance:

- **Object Pooling**: Enemy cars are managed via object pooling for memory efficiency
- **Efficient Raycasting**: Targeted raycasts only in relevant directions
- **Simplified Physics**: 2D physics calculations for better performance
- **Culling**: Objects outside the visible area are destroyed to conserve resources

## Testing Framework

### 1. Scenario Testing
The simulation supports different traffic scenarios:

- Low density traffic
- High density traffic
- Emergency braking scenarios
- Multi-lane congestion

### 2. Performance Metrics
The following metrics are used to evaluate AI performance:

- Average speed
- Lane change frequency
- Near-collision events
- Distance traveled without incidents

## Development Workflow

### 1. Version Control
The project uses Git for version control with the following branches:

- `main`: Stable, release-ready code
- `development`: Active development
- `feature/*`: New features under development
- `bugfix/*`: Bug fixes

### 2. Coding Standards
- Standard C# naming conventions
- Comprehensive XML documentation comments
- Separation of concerns between components
- Minimal coupling between systems

## Deployment

The simulation can be built and deployed for the following platforms:

- Windows (x86, x64)
- macOS
- WebGL (for browser-based demonstrations)
- Mobile platforms (future consideration)

## Integration Points

The architecture allows for future integrations:

- **Machine Learning**: TensorFlow or ML-Agents integration points
- **Data Collection**: Telemetry and decision recording for offline analysis
- **Custom Traffic Scenarios**: Editor tools for creating custom traffic patterns
- **Visual Enhancements**: Post-processing and visual effects pipeline 