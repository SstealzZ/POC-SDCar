# Self-Driving Car AI System Documentation

## Overview
The autonomous vehicle AI system implements a rule-based decision-making framework that enables the car to navigate through traffic safely and efficiently. This document provides a comprehensive description of the AI architecture, its components, and the algorithms used for perception, decision-making, and control.

## System Architecture

### 1. Perception System
The perception system uses raycasting to detect obstacles and analyze the environment:

- **Forward Detection**: Raycast in the car's forward direction detects obstacles in the current lane
- **Multi-Lane Analysis**: Additional raycasts in adjacent lanes help evaluate lane-changing opportunities
- **Distance Measurement**: Precise calculation of distances to obstacles for speed control and collision avoidance

### 2. Decision-Making System
The decision-making module analyzes perception data to determine optimal actions:

- **Lane Selection Algorithm**: Evaluates all lanes based on obstacle presence, distance, and lane-changing cost
- **Speed Control Strategy**: Dynamically adjusts speed based on obstacle proximity
- **Risk Assessment**: Quantifies risk of different maneuvers to select safest option

### 3. Control System
The control system translates decisions into vehicle movements:

- **Lane Transition Control**: Smooth interpolation between lanes during lane changes
- **Speed Modulation**: Gradual acceleration and deceleration for comfort and safety
- **Position Reset**: Ensures the vehicle stays within defined track boundaries

## Key Algorithms

### Lane Selection Algorithm
```
FindBestLane():
  1. For each lane:
     a. Cast ray to detect obstacles
     b. If obstacle detected, record distance
     c. If no obstacle, mark lane as clear with maximum distance
  
  2. Calculate score for each lane:
     a. Clear lanes get high base score
     b. Apply penalty for distance from current lane (minimize unnecessary lane changes)
     c. For blocked lanes, score is based on obstacle distance minus lane change penalty
  
  3. Select lane with highest score
```

### Speed Control Algorithm
```
calculateTargetSpeed():
  1. If no obstacles detected:
     a. Target speed = maximum speed
  
  2. If obstacle detected:
     a. Distance ratio = obstacle distance / maximum detection distance
     b. Target speed = linearly interpolate between minimum and maximum speed based on distance ratio
     
  3. Apply smooth transition to target speed using interpolation
```

## Parameter Tuning

The AI system behavior can be fine-tuned through the following parameters:

| Parameter | Description | Default Value | Impact on Behavior |
|-----------|-------------|---------------|-------------------|
| detectionDistance | Maximum forward detection range | 8.0 | Higher values allow earlier detection and smoother speed adjustments |
| distanceChangementVoie | Minimum distance to obstacle for lane change consideration | 5.0 | Lower values make the AI more proactive about lane changes |
| vitesseMax | Maximum vehicle speed | 5.0 | Affects overall simulation speed |
| vitesseMin | Minimum speed when approaching obstacles | 1.0 | Controls how much the car slows down near obstacles |
| ralentissementForce | Speed transition smoothness | 2.0 | Higher values create more responsive speed changes |
| laneChangeSpeed | Lane transition speed | 5.0 | Controls how quickly the vehicle changes lanes |
| delaiMinChangementVoie | Minimum time between lane changes | 1.0 | Prevents erratic lane-changing behavior |

## Performance Considerations

- **Computational Efficiency**: The rule-based system is lightweight and suitable for real-time operation
- **Scalability**: The algorithm can handle varying traffic densities with consistent performance
- **Limitations**: Current implementation focuses on highway-like scenarios and doesn't handle intersections

## Future Development

The current rule-based system provides a foundation for more advanced AI approaches:

- **Sensor Fusion**: Integration of multiple sensor types for enhanced perception
- **Machine Learning Integration**: Implementation of reinforcement learning for improved decision-making
- **Predictive Modeling**: Developing models to anticipate behavior of other vehicles
- **Complex Scenarios**: Extending the AI to handle intersections, merging, and urban environments 