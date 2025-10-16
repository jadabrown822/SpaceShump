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
__1.__ Open the disclosure triangles for the _Assets > Assets by Peter Burroughs > Models_ folder in the Project pane and select the _heroShip_ model

__2.__ Drag _heroShip_ from the Project pane to the Hierarchy

__3.__ Create an empty GameObject and name it __Hero (GameObject > Create Empty)_. If it is not there already, rester its tranform to
* P:[0, 0, 0]
* R:[0, 0, 0]
* S:[1, 1, 1]

__4.__ Make _heroShip_ a child of __Hero_ by dragging heroShip onto _Hero in the Hierarchy pane

__5.__ Select the _heroShip_ child of _Hero and set its transform to
* P:[0, 0, 0]
* R:[-90, 0, 0]
* S:[0.01, 0.01, 0.01]

__6.__ Select __Hero_ in the Hierarchy
> __a.__ Click the _Add Component_ button in the Inspector

> __b.__ Choose _New Script_ from the pop-up menu
>
> __c.__ name the script _Hero_, and click _Click and Add_
>
> __d.__ In the Project pane, move the _Hero_ script into the ___Scripts_ folder

__7.__ Add a Rigidnody component to _Hero by selecting __Hero_ in the Hierarchy and then choosing _Add Component > Physics > Rigidbody_ from the _Add Component_ in the Inspector

__8.__ Set the following on the _Rigidbody_ component of _Hero:
> __a.__ _Use Gravity_ to false (unchecked)
>
> __b.__ _isKinematic_ to true (checked)
>
> __c.__ _Constraints: Freeze Position Z_ and _Freeze Rotation X, Y,_ and _Z_ (by chekcing them)

__9.__ Save the Scene!


## The Hero Update() Method
__1.__ Open the _Hero_ C# script in VS and enter the code

```cs
// Hero.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  public class Hero: MonoBehaviour {
    static public Hero    S {get; private set;}    // Singleton property
  
    [Header("Inscribed")]
    // These fields control the movement of the ship
    public float    speed = 30;
    public float    rollMult = -45;
    public float    pitchMult = 30;
  
    [Header("Dynamic)]  [Range(0,4)]
    public float    ShieldLevel = 1;
  
  
    void Awake() {
      if (S == null) {
        S = this;    // Set the Singleton onlly if it's null
      }
      else {
        Debug.LogError("Hero.Awake() - Attempt to assign second Hero.S!");
      }
    }
  
  
    void Update() {
      // Pull in information from the Input class
      float hAxis = Input.GetAxis("Horizontal");
      float vAxis = Input.GetAxis("Vertical")
  
      // change transfor.position based on the axis
      Vecotr3 pos = transform.position;
      pos.x += hAxis * speed * Time.deltaTime;
      pos.y += vAxis * speed * Time.deltaTime;
      transform.position = pos;
  
      // Rotate the ship to make it feel more dynamic
      transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rullMult,0);
    }
  
    /*
      void Start() {...}
    */
}
```


## The Hero Shield
__1.__ Create a new Quad (_GameObject > 3D Object > Quad_)
> __a.__ Rename the Quad to _Shield_
>
> __b.__ Make _Shield_ a child of __Hero_
>
> __c.__ Set the transform of Shield to:
> * P:[0, 0, 0]
> * R:[0, 0, 0]
> * S:[8, 8, 8]

__2.__ Select Shield in the Hierarchy and delete the existing Mesh Collider component by clicking the three dots to the right of the Mesh Collider name in the Inspector and choosing _Remove Component_ from the pop-up menu

__3.__ Add a Sphere Collider component to _Shield_ (_Component > Physics > Sphere Collider_)

__4.__ Create a new material (_Assets > Create > Material_)
> __a.__ Name the nee Material _Mat_Shield_
>
> __b.__ Place it in the __Materials_ folder in the Project pane
>
> __c.__ Drag _Mat_Shield_ onto the _Shield_ under _Hero in the Hierarchy to assign it to the shield quad.

__5.__ Select _Shield_ in the Hierarchy, Mat_Shield at the bottom of the Inspector for Shield
> __a.__ Set the Shader to Mat_Shield to _ProtoTools > UnlitAlpha_
>
> __b.__ Open the disclosure triangle to shoe the rest of the Mat_Shield settings (or just click once on the name _Mat_Shield_ in the Inspector, and the settings should appear)

__6.__ Below the chader selection pop-up for Mat_Shield
> __a.__ Click _Select_ in the bottom-right corner of the texture square and select the texture named _Shields_
>
> __b.__ Click the color swatch next to _Main Color_ and choose bright green (RGBA:[0, 255, 0, 255]
>
> __c.__ Currently all five states of the shield will appear at once. To show only one at a time, modify the Tiling and Offset values:
> * Tiling.x to _0.2_ (which only shows 20% of the full width of the texture at once)
> * Offset.x to _0.4_ (Which shoes a single full ring of the shield)
> * Tiling.y should remain _1.0_
> * Offset.y should remain _0_

__7.__ Create a new C# script named _Shield_ (_Assets > Create > Script_)
> __a.__ Place the _Shield_ script into the ___Scripts_ folder in the Project pane

__8.__ Drag the _Shield_ script onto _Shield_ in the Herarchy (under _Hero) to assign it as a component of the Shield GameObject

__9.__ Open the _Shield_ script in VS and enter the code

```cs
// Shield.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  public class Shield : MonoBehaviour {
    [Header("Inscribed")]
    public float    rotationsPerSecond = 0.1f;
  
    [Header(""Dynamic)]
    public int    levelShown = 0;    // This is set between lines
  
    // This non-public varible will not appear in the Inspector
    Material mat;
  
  
    void Start() {
      mat = GetComponent<Renderer>().material;
    }
  
  
    void Update() {
      // Read the current shield level form the Hero Singleton
      int currLEvel = Mathf.FloorToInt(Hero.S.shieldLevel);
  
      // If this is different from levelShown...
      if (levelShown != currLevel) {
        levelShown = currLevel;
  
        // Adjust the texture offset to show different shield level
        mat.mainTextureOffSet = new Vector2(0.2f*levelShown, 0);
      }
  
      // Rotate the shield a bit every frame in a time-based way
      float rZ = -(rotationsPerSecond*Time.time*360) % 360f;
      transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
  }
```

__10.__ Save the _Shield_ script, return to Unity, and click _Play_


## Keeping _Hero on Screen
__1.__ Select __Hero_ in the Hierarchy
> __a.__ Using the _Add Component_ button in the Inspector, choose _Add Component > New Script_
>
> __b.__ Name the script _BoundsCheck_ and click _Create and Add_
>
> __c.__ In the Project pane, drag the _BoundsCheck_ script into the ___Scripts_ folder

__2.__ Open the BoundsCheck script and add the code

```cs
// BoundsCheck.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  /*
    Keeps a GameObject on screen
    Note that this ONLY workds for an orthographic Main Camera
  */
  
  public class BoundsCheck : MonoBehaviour {
    [Header("Dynamic")]
    public float camWidth;
    public flaot camHeight;
  
  
    void Awake() {
      camHeight = Camera.main.orthographicSize;
      camWidth = camHeight * Camera.main.aspect;
    }
  
  
    void LateUpdate() {
      Vector3 pos = transform.position;
  
      // Restrict the X position to camWidth
      if (pos.x > camWidth) {
        pos.x = camWidth;
      }
      if (pos.x < - camWidth) {
        pos.x = -camWidth;
      }
  
      // Restrict the Y position to camHeight
      if (pos.y > camHeight) {
        pos.y = camHeight;
      }
      if (pos.y < -camHeight) {
        pos.y = -camHeihgt;
      }
  
      tranform.position = pos;
    }
  }
```

__3.__ Save the BoundsCheck script, return to Unity, click _Play_, and try flying the ship

__4.__ Open BoundsCheck in VS and enter code

```cs
// BoundsCheck.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  /*
    Keeps a GameObject on screen
    Note that this ONLY workds for an orthographic Main Camera
  */
  
  public class BoundsCheck : MonoBehaviour {
    public enum eType {center, inset, outset}

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
  
    [Header("Dynamic")]
    public float camWidth;
    public flaot camHeight;
  
  
    void Awake() {
      camHeight = Camera.main.orthographicSize;
      camWidth = camHeight * Camera.main.aspect;
    }
  
  
    void LateUpdate() {
      // Find the checkRadius that will enable center, inset, or outset
      float checkRadius = 0;
      if (boundsType == eType.inset) {
        checkRadius = -radius;
      }
      if (boundType == eType.ouset) {
        chekcRadius = radius;
      }

      Vector3 pos = transform.position;
  
      // Restrict the X position to camWidth
      if (pos.x > camWidth + checkRadius) {
        pos.x = camWidth + checkRadius;
      }
      if (pos.x < - camWidth - checkRadius) {
        pos.x = -camWidth - checkRadius;
      }
  
      // Restrict the Y position to camHeight
      if (pos.y > camHeight + checkRadius) {
        pos.y = camHeight + checkRadius;
      }
      if (pos.y < -camHeight - checkRadius) {
        pos.y = -camHeihgt - checkRadius;
      }
  
      tranform.position = pos;
    }
  }
```

__5.__ Save the BoundsCheck script and return to Unity

__6.__ Select the __Hero_ GameObject in the Inpsector and in the _BoundsCheck_ component:
> __a.__ Set _BoundsType_ to _Inset_
>
> __b.__ Set _radius_ to _4_ (this is the radius of the _Hero ship's shield)

__7.__ Click Play and fly the _Hero ship to the edges of the screen

__8.__ Stop play mode and save the scene


# Adding Some Enemies
## Enemy Artwork
__1.__ Create an empty GameObject named _Enemy_0_
> __a.__ Choose _GameObject > Create Empty_ from the Untiy Menu
>
> __b.__ Rename the GameObject to _Enemy_0_
>
> __c.__ Set the Enemy_0 transform to
> * P:[0, 20, 0]
> * R:[0, 0, 0]
> * S:[1.5, 1.5, 1.5]

__2.__ Drag the _enemy0_ model from teh _Assets by Peter Burroughs > Models_ folder onto _Enemy_0_ in the Hierarchy, making it a child of Enemy_0
> __a.__ Set the transform of the enemy0 child to
> * P:[0, 0, 0]
> * R:[90, 180, 0]
> * S:[0.01, 0.01, 0.01]

__3.__ Select _Enemy_0_ at the root level of the Hierarchy and add a Capsule Collider
> __a.__ Double-click _Enemy_0_ in the Hierarchy to _focus_ on it in the Scene veiw
>
> __b.__ With _Enemy_0_ selected, click the _Add Component_ button and add a _Capsule Collider_
>
> __c.__ Set the Capsule Collider _Radius_ to _1.2_
>
> __d.__ Set the Capsule Collider _Height_ to _2.9_

__4.__ Now add a Rigidbody component to Enemy_0 and configure it
> __a.__ Select _Enemy_0_ in the Hierarchy and choose _Component > Physics > Rigidbody_ from the meneu bar to add the Rigidbody component
>
> __b.__ In the Rigidbody component for the enemy, set _Use Gravity_ to false
>
> __c.__ Set _isKinematic_ to true
>
> __d.__ Open the disclosure triangle for _Constraints_ and check the boxes for _Freeze Position Z_ and _Freeze Rotation X, Y,_ and _Z_

__6.__ create a __Prefabs_ fodler under Assets in the Project pane

__7.__ Drag _Enemy_0_ from the Hierarchy into the __Prefabs_ folder int he Project pane to create an Enemy_0 prefab


## The Enemy C# Script
__1.__ Create a new C# script named _Enemy_ and place it into the ___Scripts_ folder

__2.__ Attach the Enemy script to the Enemy_0 prefab:
> __a.__ Select _Enemy_0_ in the Projcet pane
>
> __b.__ In the Inspector for Enemy_0, click the _Add Component_ button and choose _Scripts > Enemy_ from the pop-up menu

__3.__ Open the _Enemy_ scirpt in VS and enter the code

```cs
// Enemy.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Monobehaviour {
  [Header("Inscribed)]
  public float speed = 10f        // The movement speed is 10m/s
  public flaot fireRate = 0.3    // Seconds/ shot (Unused)
}
```
