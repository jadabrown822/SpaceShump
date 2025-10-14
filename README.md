Space Shump
==============

1. Do the Space SHMUP tutorial from Bond Chapter 31.
2. Points will be given for the finished project as follows:
> a. Increase to 5 enemies as shown in Chapter 32 (5 points)
> 
> b. 2 points if the enemies damage the player (2 points)
> 
> c. 1 point if shooting works correctly (1 point)
>
> d. 1 point for adding a scrolling starfield background (see Chapter 32)
> 
> e. 1 point for making the game cooler in a meaningful way. Be sure to explain the enhancements in the PDF you submit on Moodle.


# Getting Started: _Space SHUMP_
### Set up the project for this chapter
* __Project Template:__ 3D Core
* __Project Name:__ Space SHUMP Part 1
* __Starter UnityPackage:__ Find Chapter 31 at http://book.prototools.net
* __Scene Name:__ (__Scene_0 is included in the import)
* __Project Folders:__ __Scripts (two underscores before "Scripts") and _Prefabs
* __Game Pane Dimentions:__ (will be set in the chapter)
* __Rename:__ (_MainCamera is included inthe imported __Scene_0)


# Setting the Scene
__1.__ Open the __Scene_0 scene

__2.__ Delete the leftover _Scenes_ folder and the default _SampleScene_ withing it

__3.__ Set _Directional Light_ in the Hierarchy and ensure that its transform is:
* P:[20, 20, 0]
* R:[50, -30, 0]
* S:[1, 1, 1]

__4.__ Double-check the __MainCamera_ transform as well:
* P:[0, 0, -10]
* R:[0, 0, 0]
* S:[1, 1, 1]

__5.__ In the _Camera_ component of __MainCamera_, set the following:
* _Clear Flags_ to Solid Color
* _Background_ to black with 25 alpha
* _Projection_ to Orthographic
* _Size_ to 40 (after seting _Projection_)
* _Far Clipping Plane_ to 100

__6.__ Save Scene

__7.__ The game will be a vertical, top-down shooter, need to set an aspect ratio for the Game pane that is in _portrait_ orientation
> __a.__ In the Game pane, click the pop-up menu lis of aspect ratios that probably currently shows either _Full HD (1920x1080)_ of _Free Aspect_
>
> __b.__ At the bottom of the list is a __+__ sumbol. Click it to add a new aspect ratio present


# Making the Hero Ship
