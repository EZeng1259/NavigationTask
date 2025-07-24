# Navigation Task 

## Unity Simulation for Human Navigation Research
This repository contains the Unity project and C# scripts for a 3D simulation built for my graduate research in cognitive science. The application is a self-contained virtual environment designed to study how humans learn and optimize routes in a task analogous to the Traveling Salesman Problem (TSP), with a specific focus on spatial and temporal memory.

The primary purpose of this project was to create a robust tool for data collection in a controlled experimental setting.

# Core Features
-Interactive 3D Environment: A fully explorable virtual marketplace containing 19 uniquely named stores, built with the Unity engine.

-First-Person Navigation: Standard first-person mouse and keyboard controls for intuitive movement and interaction within the environment.

-Data Capture & Serialization: Object-oriented C# scripts capture detailed user interaction data, including player coordinates, viewing direction, and event timestamps. This data is structured and serialized into JSON format for data transfer.

-API Integration: Includes a communication module for sending captured JSON data to a backend server via REST API calls, allowing for secure and reliable data collection.

# Task 
A participant navigates the virtual marketplace with the goal of visiting all 19 stores in the shortest possible distance. The partcipant's physical coordinates, camera movements, visited stores, are all recorded to it's separate database/spreadsheet. 

The participant completes this task for a total of 8 trials.

Upon completion, there are two additional tasks: 
- Free recall task to investigate the number of stores names the participants are able to memorize
- Cognitive mapping task to examine each participants' ability to create a bird-eye-view map of the environment from memory

# Technologies Used
- Unity
- C#
- REST APIs
