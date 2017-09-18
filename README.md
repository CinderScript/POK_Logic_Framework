## EDIT: This is not a complete Unity project and cannot be imported using the procedure below.  This is for game logic demonstration only.
---

# Pawn Of Kings
Unity 5.5 Project Repository.

## Description
Pawn of Kings is a game inspired by XCOM, StarCraft, Fallen Haven, and a little bit of Magic the Gathering.  It is being developed for Android and Windows in the Unity game engine.  Game features include:
   - Strategy
   - Turn Based
   - Grid Based
   - Single-player

This repository is a full Unity project using Git source control.  The Git repository ignores the Library, Temp, Obj, and Build folders as well as the Visual Studio project and solution files.

## Set-up: Overview
 1. Clone the git repository
 2. Open cloned folder as a Unity project
 3. Generate the visual studio solution in Unity

## Set-up: Visual Studio TFS
1. Open Visual Studio and using Team Services, connect to the Team Foundation Server. Select [Manage Connections] from the Team Explorer or the Team menu.  Right click the project repository in the server (cinderscript.visualstudio.com) and select [clone]. *A new local repository should now be listed (PawnOfKings if the default name was kept).*

2. Open Unity and select [File] -> [Open Project].  Select the folder of the newly cloned repository and load in Unity.  The project will import, and generate a Library and Temp folder.  Select a scene from the Scene folder. *Unity may need to be restarted if terrain lighting looks incorrect.*

3. Have Unity generate the visual studio project and solution by choosing [Assets] -> [Open C# Project]. *If you are still signed into the Team Foundation Server, the current active solution will be the newly created solution from the cloned Unity project.*

## Design Pattern
This code library is a framework for a turn based strategy game.  The scripts are broken down into two categories: \Scripts\Mono and \Scripts\Logic.  Mono is a namespace that contains any Monobehavior (Unit API) extending classes. Most of the Mono classes store game tuning values that are set in the Unity editor and then passed to the game logic at runtime.  The Logic namespace contains all mechanics for controlling the gameplay.

The entry point of the game logic to the Unity API are the Driver classes (Monobehaviour). A chain of updates occur through the game logic that are driven through the Driver's overridden Update().  An example of the update chain during gameplay looks like this:
**BattleStateDriver --> GameStateManager --> BattleState --> ArmyManager<AIController> --> AIController --> IUnitBehavior**

The game logic is based on a Finite State Machine that keeps track of the game's state.  Each state is represented by a class that inherits the IGameState interface.  The current state, tracked by the GameStateManager is called in the manager's update.

Currently, the state the game runs is the BattleState, which holds references to the player and ai armies.  The BattleState keeps track of the turns taken and updates the appropriate army.  Each army (ArmyManager<UnitControllerType>) holds a list of UnitControllers, which can be either AIController or PlayerController. Each controller holds a reference to a Unit. The ArmyManager keeps track of the Units' turns and updates the correct UnitController.

The UnitController also uses a finite state machine.  The states in this case are the possible behaviors/actions (IBehavior) a unit controller can perform.  The controller updates the correct UnitBehavior.

### Licence
Author: Gregory Maynard, <CinderScript@gmail.com>, Copyright (C) 2016 - All Rights Reserved