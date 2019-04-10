# Analyzing autostereoscopic environment configurations for the design of videogames

## Description

This project presenters some system configurations that create a autostereoscopic visualization in Unity 
were evaluated in order to obtain a good accommodation of the user experience with the game. The project 
was carried out in the Unity version 5.2.5f1 (64-bit), and the generated contents were visualized on an 
autostereoscopic display with 8-view 46-in xyZ 3D LCD with lenticular lenses.

The type of videogame chosen was an Autostereoscopic Shooter game over two scenarios where three configurations 
of cameras were tested:  Parallel Optical Axes (POA), Static Converging Optical Axes (SCOA) and Dynamic Converging 
optical axes (DCOA). 

The avatar that represents the player is a spacecraft. In the first scenario, the player moves inside a tube-shaped 
enclosure. At the same time, a set of asteroids appear in the videogame, emerging from the background and moving 
inside the tube-shaped enclosure. In the second scenario, the spacecraft flies over the terrain trying to avoid 
hitting the asteroids.

## Contents of the executable

The executable file contents seven scenes of which the first is configuration scene and the rest represents a configuration 
of cameras on a stage. Each scene has a static mode where the activity of the player in the game is passive and 
he/she only observes the game without interacting with it. After this visualization period, the player interacts 
with the game, moving the spacecraft to avoid the asteroids that are going to crash into it.

The instructions of keyboard for each scene are:
- F1: Scene of configuration to measure initial parameters.
- F2: Experiment 1 (DCOA) with tube-shaped enclosure.
- F3: Experiment 2 (SCOA) with tube-shaped enclosure.
- F4: Experiment 3 (POA) with tube-shaped enclosure.
- F5: Experiment 1 (DCOA) with procedurally generated terrain.
- F6: Experiment 2 (SCOA) with procedurally generated terrain.
- F7: Experiment 3 (POA) with procedurally generated terrain.

The instructions of keyboard for spacecraft movement are:
- W and S: Front and behind.
- A and D: Left and right.
- E and Q: Forward and backward.

## Other contents

In folder code, we have put the code in c# of the project and in folder Shader the interlaced shader code. In folder 
code, we have put the code in c# of the project and in folder Shader the interlaced shader code. Moreover, we have 
added the opinions of the users when they proved the game in the different camera configurations. The users score 
for the different camera configuration appears in the excel file "Form about autostereoscopic environment (answers).xlsx", 
answering the following questions:

- Playability
	- P1. Things seem to be unpredictable
	- P2. Playing seems automatic
	- P3. I lose track of where I am
	- P4. I play without thinking about how to play
	- P5. I really get into the game
	- P6. I feel like I just can't stop playing

- Annoyance
	- A1. How much seasickness do you feel?
	- A2. How tired and sore your neck and back?
	- A3. How do you eyes feel after playing?
	- A4. Which session was most fatiguing?
	- A5. Which session gave you more headache?

- Preference of configuration
	- C1. Which session did you prefer?
	- C2. How clear is you 3D sensation?



