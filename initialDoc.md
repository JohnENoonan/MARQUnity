# Documentation
This file serves as the documentation for the work done by John Noonan from May to September 2018. This is not an API guide but rather a description of program structure and important and not obvious details of the game's implementation. The following key points are not presented in a hierarchical order.

## Scenes
The game is made of 3 scenes: startGameScene, mainMenuScene, and cameraScene.
### startGameScene
This scene serves as the first thing the player sees. It is used to assign a team number which is used to order the super searchers which will allow for multiple teams to play at once without bumping into each other. Unfortunately for testing purposes that functionality hasn't been implemented yet (see todo.md) and currently just defaults to 1.
### mainMenuScene
This scene is where the player does almost all interaction. In this scene are many menus that are activated on nav clicks.
### cameraScene
This scene is where the player will scan qr codes and perform all AR actions. The user camera is through Vuforia.

## Classes
Not all classes are described. Every class should have a description at the top of its definition. Classes detailed here are the ones considered not obvious or of high importance.
### GameControl
GameControl is a singleton object that serves as the data controller for the player. Initialized in mainMenuScene, this object will allow for progression in the game, most notably in the dialogue. GameControl is a permanent object that must be present in all scenes. This is the object where additional data flow programming is like to go. Canvas objects are children of this so as to preserve the classes of mainMenuScene when entering the cameraScene
### GameEvent
This is a serialized object that represents each entry in the JSON script. This object will be interacted with in order to progress the dialogue and accept answers to questions.
### GameEventCollection
Object that stores the entire script and can be used for iteration.
### jsonHelper
Helper class used to read script object on Android and PC.
### menuDropdownControls
Miscellaneous call functions that are used from the main menu.
### BioButton
Class with functions to display correct image for the super searcher buttons
### cameraControl
cameraScene equivalent of GameControl. Gets badge info from badges.txt. This is most likely the class to edit for adding functionality to cameraScene that is not a UI interaction.
### ClickBadge
Allows a AR badge to be clicked to show the bio page. Every badge geometry must have a collider.
### puzzleController
controller used for accepting the final answer. This is currently unfinished.
