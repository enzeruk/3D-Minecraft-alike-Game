# 3D Minecraft-alike Game
Description: A 3D game similar to Minecraft was created using the game engine ***Unity3D*** in 2019, for the core course "Computer Graphics and Interactive Systems" for my 
undergraduate studies in Computer Science and Engineering at University of Ioannina.

### Before you Download 

- This 3D game is build for **Android devices only**.

- Compatible OS: `Android 4.4 KitKat (API level 19)` **and above**.

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Abstract
The game starts by asking the user to *Enter a number* that will define the dimensions (NxNx1) of the 3D scene. Automatically, the 
Level #1 will be generated, that is just a floor that is created by NxN colorful cubes **randomly**.  

The color of the cubes can be ***blue, yellow, red, green*** and ***cyan***. 
The cube with the ***magenta*** color is unique and defines the starting position of the Player.  

At the beginning, the Player stands on the *magenta cube* with 4 lives and 100 points.
The Player cannot exit the limits of the NxNx1 scene.
The main goal of the game is to create a path so that the Player can reach ***#N-1 Levels*** in ***high***.
The path can be built by using the cubes that consist the floor of Level #1. 

> Each of the cubes on the floor provide specific reserve as seeing below:

	blue cube: 0 cube(s) reserve

	yellow cube: 1 cube reserve

	red cube: 2 cubes reserve

	green cube: 3 cubes reserve

	cyan cube: 1 cylinder reserve

> The high of the cubes and cylinders can be converted to the specific Level high:

	1 cude (1x1x1) : 1 Level high

	1 cylinder (1x1x2) : 2 Levels high

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Controls and Rules
- Use the `joystick` on the left side of the screen to navigate *front, back, left and right*.

- Use the half-right side of the screen to control the view of the camera.

- Tap on the `GET` button to get a cube from the floor with its corresponding reserve.

- Tap on the `PLACE CUBE` button to place a random color cube from your inventory.

- Tap on the `PLACE CYLINDER` button to place a cyan color cylinder from your inventory.

- Tap on the `JUMP` button to jump on the cubes and cylinders that you place in the scene.

- Tap on the `SETTINGS` button to pause or exit the game scene, or return to the main menu.

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

#### Made with `Unity 2020.2.0f1`

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
