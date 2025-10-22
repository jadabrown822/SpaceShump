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

__3.__ Create an empty GameObject and name it __Hero (GameObject > Create Empty)_. If it is not there already, rester its transform to
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
    public float    shieldLevel = 1;
  
  
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
      float vAxis = Input.GetAxis("Vertical");
  
      // change transform.position based on the axis
      Vector3 pos = transform.position;
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
    public float camHeight;
  
  
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
        pos.y = -camHeight;
      }
  
      transform.position = pos;
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
    public float camHeight;
  
  
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
        pos.y = -camHeight - checkRadius;
      }
  
      transform.position = pos;
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

__2.__ Drag the _enemy0_ model from the _Assets by Peter Burroughs > Models_ folder onto _Enemy_0_ in the Hierarchy, making it a child of Enemy_0
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
> __a.__ Select _Enemy_0_ in the Project pane
>
> __b.__ In the Inspector for Enemy_0, click the _Add Component_ button and choose _Scripts > Enemy_ from the pop-up menu

__3.__ Open the _Enemy_ script in VS and enter the code

```cs
// Enemy.cs
  
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  public class Enemy : MonoBehaviour {
    [Header("Inscribed)]
    public float speed = 10f;        // The movement speed is 10m/s
    public float fireRate = 0.3;    // Seconds/shot (Unused)
    public float health = 10;        // Damage needed to destroy this enemy
    public int score = 100;          // Points earned for destroying this
  
    // This is a Property: A method that acts like a field
    public Vector3 pos {
      get {
        return this.transform.position;
      }
      set {
        this.transform.position = value;
      }
    }


    void Update() {
      Move();
    }
  
  
    public virtual void Move() {
      Vector3 tempPos = pos;
      tempPos.y -= speed * Time.deltaTime;
      pos = tempPos;
    }
  
  
    /*
      void Start() {...}
    */
  }
```

__4.__ Save the script, return to Unity, and click _Play_

__5.__ To attach the BoundsCheck script to the Enemy_0 prefab:
> __a.__ Select the _Enemy_0_ prefab in the Hierarchy
>
> __b.__ In the Inspector, click _Add Component_ and choose _Add Component > Scripts > BoundsCheck_

__6.__ To apply this change made to the Enemy_0 instance back to its prefab:
> __a.__ Click the _Overrides_ button at the top of the Inspector for the Enemy_0 instance in the Hierarchy. A pop-up appears showingthat the BoundsCheck script is an _override_, something that is different in this instance than in the prefab. the plus on the script icon shows that the script was added to this instance
>
> __b.__ Click _Apply All_ to apply this (and all other overrides) back to the prefab
>
> __c.__ Select the _Enemy_0_ prefab in the _Prefabs folder of the Project pane to see that the BoundsCheck script is now attahced to the prefab as well

__7.__ Select the _Enemy_0_ instance in the Hierarchy
> __a.__ Set the __boundsType__ value in the BoundsCheck Inspector to _outset_
>
> __b.__ Set the __radius__ value in the BoundsCheck inspector to _2_
>
> __c.__ Click _Overrides_ at the top of the Inspector again, will see it saying that something about the BoundsCheck script is different from the prefab
>
> __d.__ Click _BoundsCheck (Script)_ in this pop-up to get more info. here ir clearly shows that the radius value is the difference bewteen the two
>
> __e.__ In the pop-up with more information, click _Apply_, and then choose _Apply to Prefab 'Enemy_0' from the menu_

__8.__ Click _Play_, and see than Enemy_0 instance stops right after it has gone off screen

__9.__ To have the option to either allow Enemy_0 to go off screen or restrict it to the screen, open the BoundsCheck script in VS and make code modifications


```cs
// BoundsCheck.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  /*
    Checks whether a GameObject is on screen and can force it to stay on screen
    Note that this ONLY workds for an orthographic Main Camera
  */
  
  public class BoundsCheck : MonoBehaviour {
    public enum eType {center, inset, outset}

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;
  
    [Header("Dynamic")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
  
  
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
      isOnScreen = true;
  
      // Restrict the X position to camWidth
      if (pos.x > camWidth + checkRadius) {
        pos.x = camWidth + checkRadius;
        isOnScreen = false;
      }
      if (pos.x < - camWidth - checkRadius) {
        pos.x = -camWidth - checkRadius;
        isOnScreen = false;
      }
  
      // Restrict the Y position to camHeight
      if (pos.y > camHeight + checkRadius) {
        pos.y = camHeight + checkRadius;
        isOnScreen = false;
      }
      if (pos.y < -camHeight - checkRadius) {
        pos.y = -camHeight - checkRadius;
        isOnScreen = false;
      }
  
      if (keepOnScreen && !isOnScreen) {
        transform.position = pos;
        isOnScreen = true;
      }  
    }
  }
```


### Deleting the Enemy When It Goes Off Screen
__1.__ Select the _Enemy_0_ prefab in the _Prefabs folder of the Project pane

__2.__ Set __keepOnScreen__ to __false__ (unchecked) in the _BoundsCheck (Script)_ component Inspector

__3.__ Add the code to the Enemy script to manage this destruction:


```cs
// Enemy.cs
  
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  [RequireComponent(typeof(BoundsCheck))]
  
  public class Enemy : MonoBehaviour {
    [Header("Inscribed)]
    public float speed = 10f;        // The movement speed is 10m/s
    public float fireRate = 0.3;    // Seconds/shot (Unused)
    public float health = 10;        // Damage needed to destroy this enemy
    public int score = 100;          // Points earned for destroying this

    private BoundsCheck bndCheck;

    void Awake() {
      bndCheck = GetComponent<BoundsCheck>();
    }
  
    // This is a Property: A method that acts like a field
    public Vector3 pos {
      get {
        return this.transform.position;
      }
      set {
        this.transform.position = value;
      }
    }


    void Update() {
      Move();

      // Check whether this Enemy has gone off the bottom of the screen
      if (!bndCheck.isonScreen) {
        if (pos.y < bndCheck.camHeight - bndCheck.radius) {
          // We're off the the bottom, so destroy this GameObject
          Destroy(gameObject);
        }
      }
    }
  
  
    public virtual void Move() {
      Vector3 tempPos = pos;
      tempPos.y -= speed * Time.deltaTime;
      pos = tempPos;
    }
  
  
    /*
      void Start() {...}
    */
  }
```

__4.__ Modify the BoundsCheck script

```cs
// BoundsCheck.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  /*
    Checks whether a GameObject is on screen and can force it to stay on screen
    Note that this ONLY workds for an orthographic Main Camera
  */
  
  public class BoundsCheck : MonoBehaviour {
    [System.Flags]
    public enum eScreenLocs {
      onScreen = 0,    // 0000 in binary
      offRight = 1,    // 0001 in binary
      offLeft = 2,     // 0010 in binary
      offUp = 4,       // 0100 in binary
      offDown = 8      // 1000 in bindry
    }

    public enum eType {center, inset, outset}

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;
  
    [Header("Dynamic")]
    public eSceenLoc screenLocs = eScreenLocs.onScreen;
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
  
  
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
      isOnScreen = true;
  
      // Restrict the X position to camWidth
      if (pos.x > camWidth + checkRadius) {
        pos.x = camWidth + checkRadius;
        isOnScreen = false;
      }
      if (pos.x < - camWidth - checkRadius) {
        pos.x = -camWidth - checkRadius;
        isOnScreen = false;
      }
  
      // Restrict the Y position to camHeight
      if (pos.y > camHeight + checkRadius) {
        pos.y = camHeight + checkRadius;
        isOnScreen = false;
      }
      if (pos.y < -camHeight - checkRadius) {
        pos.y = -camHeight - checkRadius;
        isOnScreen = false;
      }
  
      if (keepOnScreen && !isOnScreen) {
        transform.position = pos;
        isOnScreen = true;
      }  
    }
  }
```

__5.__ Save the _BoundsCheck_ script and return to Unity

__6.__ Modify the BoundsCheck script by adding code

```cs
// BoundsCheck.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  /*
    Checks whether a GameObject is on screen and can force it to stay on screen
    Note that this ONLY workds for an orthographic Main Camera
  */
  
  public class BoundsCheck : MonoBehaviour {
    [System.Flags]
    public enum eScreenLocs {
      onScreen = 0,    // 0000 in binary
      offRight = 1,    // 0001 in binary
      offLeft = 2,     // 0010 in binary
      offUp = 4,       // 0100 in binary
      offDown = 8      // 1000 in bindry
    }

    public enum eType {center, inset, outset}

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;
  
    [Header("Dynamic")]
    public eSceenLoc screenLocs = eScreenLocs.onScreen;
    // public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
  
  
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
      screenLocs = eScreenLocs.onScreen;
      // isOnScreen = true;
  
      // Restrict the X position to camWidth
      if (pos.x > camWidth + checkRadius) {
        pos.x = camWidth + checkRadius;
        screenLocs |= eScreenLocs.offRight;
        // isOnScreen = false;
      }
      if (pos.x < - camWidth - checkRadius) {
        pos.x = -camWidth - checkRadius;
        screenLocs |= eScreenLocs.offLeft;
        // isOnScreen = false;
      }
  
      // Restrict the Y position to camHeight
      if (pos.y > camHeight + checkRadius) {
        pos.y = camHeight + checkRadius;
        screenLocs |= eScreenLocs.offUp;
        // isOnScreen = false;
      }
      if (pos.y < -camHeight - checkRadius) {
        pos.y = -camHeight - checkRadius;
        screenLocs |= eScreenLocs.offDown;
        // isOnScreen = false;
      }
  
      if (keepOnScreen && !isOnScreen) {
        transform.position = pos;
        screenLocs = eScreenLocs.onScreen;
        // isOnScreen = true;
      }  
    }


    public bool isOnScreen {
      get {return (screenLocs == eScreenLocs.onScreen);}
    }
  }
```

__7.__ Save the _BoundsCheck_ script, and return to Unity, and click Play. Enemy_0 should move down the screen as it did before and then Destroy itself when it passes off the bottom of the screen. __Unity will continue to be in Play mode during these lettered steps__
> __a.__ Select _Hero_ in the Hierarchy
>
> __b.__ In the _BoundsCheck (Script)_ component Inspector, set _keepOnScreen_ to _false_ (unchecked, so that Hero can move off screen)
>
> __c.__ Click in the Game pane and move _Hero_ around both on and off screen using the WASD of arrow key
>
> __d.__ Click the Play button again to stop play in Unity, and Hero returns to the center of the screen and _keepOnScreen_ returns to checked

__8.__ Add the __LocIs()__ method code to the end of BoundsCheck

```cs
// BoundsCheck.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  /*
    Checks whether a GameObject is on screen and can force it to stay on screen
    Note that this ONLY workds for an orthographic Main Camera
  */
  
  public class BoundsCheck : MonoBehaviour {
    [System.Flags]
    public enum eScreenLocs {
      onScreen = 0,    // 0000 in binary
      offRight = 1,    // 0001 in binary
      offLeft = 2,     // 0010 in binary
      offUp = 4,       // 0100 in binary
      offDown = 8      // 1000 in bindry
    }

    public enum eType {center, inset, outset}

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;
  
    [Header("Dynamic")]
    public eSceenLoc screenLocs = eScreenLocs.onScreen;
    // public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
  
  
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
      screenLocs = eScreenLocs.onScreen;
      // isOnScreen = true;
  
      // Restrict the X position to camWidth
      if (pos.x > camWidth + checkRadius) {
        pos.x = camWidth + checkRadius;
        screenLocs |= eScreenLocs.offRight;
        // isOnScreen = false;
      }
      if (pos.x < - camWidth - checkRadius) {
        pos.x = -camWidth - checkRadius;
        screenLocs |= eScreenLocs.offLeft;
        // isOnScreen = false;
      }
  
      // Restrict the Y position to camHeight
      if (pos.y > camHeight + checkRadius) {
        pos.y = camHeight + checkRadius;
        screenLocs |= eScreenLocs.offUp;
        // isOnScreen = false;
      }
      if (pos.y < -camHeight - checkRadius) {
        pos.y = -camHeight - checkRadius;
        screenLocs |= eScreenLocs.offDown;
        // isOnScreen = false;
      }
  
      if (keepOnScreen && !isOnScreen) {
        transform.position = pos;
        screenLocs = eScreenLocs.onScreen;
        // isOnScreen = true;
      }  
    }


    public bool isOnScreen {
      get {return (screenLocs == eScreenLocs.onScreen);}
    }


    public bool LocIs(eScreenLocs checkLoc) {
      if (checkedLoc == eScreenLocs.onScreen) return isOnScreen;
      return ((screenLoc & checkeLoc) == checkLoc);
    }
  }
```

__9.__ Make changes to the Enemy script


```cs
// Enemy.cs
  
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  [RequireComponent(typeof(BoundsCheck))]
  
  public class Enemy : MonoBehaviour {
    [Header("Inscribed)]
    public float speed = 10f;        // The movement speed is 10m/s
    public float fireRate = 0.3;      // Seconds/shot (Unused)
    public float health = 10;        // Damage needed to destroy this enemy
    public int score = 100;          // Points earned for destroying this

    private BoundsCheck bndCheck;

    void Awake() {
      bndCheck = GetComponent<BoundsCheck>();
    }
  
    // This is a Property: A method that acts like a field
    public Vector3 pos {
      get {
        return this.transform.position;
      }
      set {
        this.transform.position = value;
      }
    }


    void Update() {
      Move();

      // Check whether this Enemy has gone off the bottom of the screen
      if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown)) {
        Destroy(gameObject);
      }

      /*
        if (!bndCheck.isonScreen) {
          if (pos.y < bndCheck.camHeight - bndCheck.radius) {
            // We're off the the bottom, so destroy this GameObject
            Destroy(gameObject);
          }
        }
      */
    }
  
  
    public virtual void Move() {
      Vector3 tempPos = pos;
      tempPos.y -= speed * Time.deltaTime;
      pos = tempPos;
    }
  
  
    /*
      void Start() {...}
    */
  }
```


# Spawning Enemies at Random
__1.__ Attach a _BoundsCheck_ script to __MainCamera_
> __a.__ Set _keepOnScreen_ to _false_ (unchecked) in the _MainCamera BoundsCheck Inspector

__2.__ Create a new C# script called _Main_
> __a.__ Move the _Main_ script into the ___Scripts_ folder
>
> __b.__ Attach the _Main_ script to __MainCamera_
>
> __c.__ Open the _Main_ script in VS and enter the code

```cs
// Main.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.SceneManagement;    // Enables the loading & reloading of scenes
  
  public class Main : MonoBehaviour {
    static private Main S;    // A private singleton for Main
  
    [Header("Inscribed")]
    public GameObject[] prefabEnemies;          // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f;    // # Enemies spawned/second
    public float enemyInsetDefault = 1.5;       // Inset from the sides
  
    private BoundsCheck bndCheck;
  
    void Awake() {
      S = this;
  
      // Set bndCheck to reference the BoundsCheck component on this GameObject
      bndCheck = GetComponent<BoundsCheck>();
  
      // Invoke SpawnEnemey() once (in 2 seconds, based on default values)
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }
  
  
    public void SpawnEnemy() {
      // Pick a random Enemy prefab to instantiate
      int ndx = Random.Range(0, prefabEnemies.Length);
      GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
  
      // position the Enemy above the screen with a random x position
      float enemyInset = enemyInsetDefault;
      if (go.GetComponent<BoundsCheck>() != null) {
        enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
      }
  
      // Set the initial position for the spawned Enemy
      Vector3 pos = Vector3.zero;
      float xMin = -bndCheck.camWidth + enemyInset;
      float xMax = bndCheck.camWidth - enemyInset;
      pos.x = Random.Range(xMin, xMax);
      pos.y = bndCheck.camHeight + enemyInset;
      go.transform.position = pos;
  
      // Invoke SpawnEnemy() again
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }
  
    /*
      void Start() {...}
  
      void Update() {...}
    */
  }
```

__3.__ Make sure to copy any prefab overrides that were set on the instance of Enemy_0 in the Hierarchy back onto the Enemy_0 prefab
> __a.__ Select _Enemy_0_ in the Hierarchy
>
> __b.__ Click _Overrides_ at the top of the Enemy_0 instance Inspector pane
>
> __c.__ If there are any overrides, click _Apply All_ to apply them back to the Enemy_0 prefab
>
> __d.__ Delete the _Enemy_0_ instance from the Hierarchy

__4.__ Set up the Main sript on _MainCamera to spawn Enemy_0s:
> __a.__Select __MainCamera_ in the Hierarchy
>
> __b.__ In the _Main (Script)_ component of _MainCamera, open the disclosure trangle next to __prefabEnemies__ and set the _Size_ of __prefabEnemies__ to _1_
>
> __c.__ Drag _Enemy_0_ from the _Prefabs folder of the Project pane into _Element 0_ of the prefabEnemies array
>
> __d.__ Save the scene

__5.__ Play the scene. Enemy_0 spawn about once every 2 seconds, travel down to the bottom of the screen, and then disappear after it exists at the bottom of the screen


# Setting Tags, Layers, and Physics
* __Hero:__ The _Hero ship should collide with enemies, enemy projections, and power-ups but should not collide with hero projectiles
* __Enemy:__ Enemies hsould collide with _Hero and hero projectiles but not with power-ups
* __ProjectileHero:__ Projectiles fired by _Hero should only collide with enemies
* __ProjectileEnemy:__ Projectiles fired by enemies should only collide with _Hero
* __PowerUp:__ Power_ups hsould only collide with _Hero

__1.__ Open the _Tags & Layers_ manager in the Project Settings window by selecting _Edit > Project Settings..._ from the Unity menu and then clicking _Tags and Layers_ on the left side. Tags and physics layers are different from each other but are set in th same window

__2.__ OPen th edisclosure triangle next to _Layers_. Builtin Layers 0-2, 4, and 5 are reserved by Unity.

__3.__ Open the Physics Manager by clicking _Physics_ (not Physics 2D) on the left side of the Project Setting window, and set the _Layer Collision Matrix_
* Hero, PowerUp ✔️
* Hero, ProjectileEnemy ✔️
* Hero, Enemy ✔️
* Enemy, ProjectileHero ✔️

__4.__ Close the Proect Settings window


## Assigning the Proper Layers to GameObject
__1.__ Select __Hero_ in the Hierarchy and choose _Hero_ from the _Layer_ pop-up menu in the Inspector. _Yes, change children_

__2.__ Selec the _Enemy_0_ prefab in the _Prefabs folder and set its layer to _Enemy_. When asked, again choose _Yes, change children_

__3.__ Save the scene


# Making the Enemies Damage the Player
__1.__ Make the hero chield a trigger:
> __a.__ Open the disclose triangle next to __Hero_ in the Hierarchy
>
> __b.__ Select the _Hero child _Shield_
>
> __c.__ In the Inspector, set the _Sphere Collider_ of Shield to be a trigger (check the box next to _Is Trigger_)

__2.__ Open the _Hero_ C# Script in VS and add code

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
    public float    shieldLevel = 1;
  
  
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
      float vAxis = Input.GetAxis("Vertical");
  
      // change transform.position based on the axis
      Vector3 pos = transform.position;
      pos.x += hAxis * speed * Time.deltaTime;
      pos.y += vAxis * speed * Time.deltaTime;
      transform.position = pos;
  
      // Rotate the ship to make it feel more dynamic
      transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rullMult,0);
    }


    void OnTriggerEnter(Collider other) {
      Transform rootT = other.gameObject.transform.root;
      GameObject fo = rootT.gameObject;
      Debug.Log("Shield trigger hit by: ") + go.gmaeObject.name;
    }
  
    /*
      void Start() {...}
    */
}
```

__3.__ Open the _Hero_ sript in VS and make code modifications

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
    public float    shieldLevel = 1;
    [Tooltip("This field holds a reference tothe last triggering GameObject")]
    private GameObject lastTriggerGo = null;
  
  
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
      float vAxis = Input.GetAxis("Vertical");
  
      // change transform.position based on the axis
      Vector3 pos = transform.position;
      pos.x += hAxis * speed * Time.deltaTime;
      pos.y += vAxis * speed * Time.deltaTime;
      transform.position = pos;
  
      // Rotate the ship to make it feel more dynamic
      transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rullMult,0);
    }


    void OnTriggerEnter(Collider other) {
      Transform rootT = other.gameObject.transform.root;
      GameObject fo = rootT.gameObject;
      // Debug.Log("Shield trigger hit by: ") +go.gmaeObject.name;

      // Make sure it's not the same trigger fo as last time
      if (go == lastTriggerGo) return;
      lastTriggerGo = go;

      Enemy enemy = go.GetComponent<Enemy>();
      if (enemy != null) {    // If the shield was triggered by an enemy
        shieldLevel--;        // Decreases the level of the shield by 1
        Destroy(go);          // ... and Destroy the enemy
      }
      else {
        Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
      }
    }
  
    /*
      void Start() {...}
    */
}
```

__4.__ Play the Scene and try running into some ships. The shild will loop back around ro full strength after completely drained

__5.__ In the Hero class change code

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
  
    [Header("Dynamic)]    [Range(0,4)]    [SerialField]
    private float _shieldLevel = 1;
    // public float    shieldLevel = 1;
    [Tooltip("This field holds a reference tothe last triggering GameObject")]
    private GameObject lastTriggerGo = null;
  
  
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
      float vAxis = Input.GetAxis("Vertical");
  
      // change transform.position based on the axis
      Vector3 pos = transform.position;
      pos.x += hAxis * speed * Time.deltaTime;
      pos.y += vAxis * speed * Time.deltaTime;
      transform.position = pos;
  
      // Rotate the ship to make it feel more dynamic
      transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rullMult,0);
    }


    void OnTriggerEnter(Collider other) {
      Transform rootT = other.gameObject.transform.root;
      GameObject fo = rootT.gameObject;
      // Debug.Log("Shield trigger hit by: ") +go.gmaeObject.name;

      // Make sure it's not the same trigger fo as last time
      if (go == lastTriggerGo) return;
      lastTriggerGo = go;

      Enemy enemy = go.GetComponent<Enemy>();
      if (enemy != null) {    // If the shield was triggered by an enemy
        shieldLevel--;        // Decreases the level of the shield by 1
        Destroy(go);          // ... and Destroy the enemy
      }
      else {
        Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
      }
    }


    public float shieldLevel {
      get {return (_shieldLevel);}
      private set {
        _shieldLevel = Mathf.Min(value, 4);

        // If the shield is going to be set to less than zero
        if (value < 0) {
          Destroy(this.gameObject);    // Destroy the Hero
        }
      }
    }
  
    /*
      void Start() {...}
    */
}
```


# Restart the Game
__1.__ Add code to Main script

```cs
// Main.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.SceneManagement;    // Enables the loading & reloading of scenes
  
  public class Main : MonoBehaviour {
    static private Main S;    // A private singleton for Main
  
    [Header("Inscribed")]
    public GameObject[] prefabEnemies;          // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f;    // # Enemies spawned/second
    public float enemyInsetDefault = 1.5;       // Inset from the sides
    public float gameRestartDelay = 2;
  
    private BoundsCheck bndCheck;
  
    void Awake() {
      S = this;
  
      // Set bndCheck to reference the BoundsCheck component on this GameObject
      bndCheck = GetComponent<BoundsCheck>();
  
      // Invoke SpawnEnemey() once (in 2 seconds, based on default values)
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }
  
  
    public void SpawnEnemy() {
      // Pick a random Enemy prefab to instantiate
      int ndx = Random.Range(0, prefabEnemies.Length);
      GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
  
      // position the Enemy above the screen with a random x position
      float enemyInset = enemyInsetDefault;
      if (go.GetComponent<BoundsCheck>() != null) {
        enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
      }
  
      // Set the initial position for the spawned Enemy
      Vector3 pos = Vector3.zero;
      float xMin = -bndCheck.camWidth + enemyInset;
      float xMax = bndCheck.camWidth - enemyInset;
      pos.x = Random.Range(xMin, xMax);
      pos.y = bndCheck.camHeight + enemyInset;
      go.transform.position = pos;
  
      // Invoke SpawnEnemy() again
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }


    void DelayRestart() {
      // Invoke the Restart() method in gameRestartDelay seconds
      Invoke(nameof(Restart), gameRestartDelay);
    }


    void Restart() {
      SceneManager.LoadScene("__Scene_0");
    }


    static public void HERO_DIED() {
      S.DelayedRestart();
    }
  
    /*
      void Start() {...}
  
      void Update() {...}
    */
  }
```

__2.__ Add the called to __Main.HERO_DIED()__ to the Hero script

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
  
    [Header("Dynamic)]    [Range(0,4)]    [SerialField]
    private float _shieldLevel = 1;
    // public float    shieldLevel = 1;
    [Tooltip("This field holds a reference tothe last triggering GameObject")]
    private GameObject lastTriggerGo = null;
  
  
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
      float vAxis = Input.GetAxis("Vertical");
  
      // change transform.position based on the axis
      Vector3 pos = transform.position;
      pos.x += hAxis * speed * Time.deltaTime;
      pos.y += vAxis * speed * Time.deltaTime;
      transform.position = pos;
  
      // Rotate the ship to make it feel more dynamic
      transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rullMult,0);
    }


    void OnTriggerEnter(Collider other) {
      Transform rootT = other.gameObject.transform.root;
      GameObject fo = rootT.gameObject;
      // Debug.Log("Shield trigger hit by: ") +go.gmaeObject.name;

      // Make sure it's not the same trigger fo as last time
      if (go == lastTriggerGo) return;
      lastTriggerGo = go;

      Enemy enemy = go.GetComponent<Enemy>();
      if (enemy != null) {    // If the shield was triggered by an enemy
        shieldLevel--;        // Decreases the level of the shield by 1
        Destroy(go);          // ... and Destroy the enemy
      }
      else {
        Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
      }
    }


    public float shieldLevel {
      get {return (_shieldLevel);}
      private set {
        _shieldLevel = Mathf.Min(value, 4);

        // If the shield is going to be set to less than zero
        if (value < 0) {
          Destroy(this.gameObject);    // Destroy the Hero
          Main.HERO_DIED();
        }
      }
    }
  
    /*
      void Start() {...}
    */
}
```

__3.__ Add __Scene_0 to the build settings:
> __a.__ Choose _File > Build Settings..._ from the Unity Menu
>
> __b.__ Int he Build Setting window, if __Scene_0 isn't listed at the top, then click the _Add Open Scenes_ button
>
> __c.__ Close the Build Settings window

__4.__ Save the Scene


# Shooting (Finally)
## ProjectileHero, the Hero's Bullet
__1.__ Create a cube named _ProjectileHero_ in the Hierarchy with the following transform value:
* P:[10, 0, 0]
* R:[0, 0, 0]
* S:[0.25, 1, 0.25]

__2.__ Set the layer of ProjectileHero to _ProjecitleHero_

__3.__ Create a new material named _Mat_Projectile_:
> __a.__ Place _Mat_Projectile_ in the _Materials folder of the Project pane
>
> __b.__ Give _Mat_Projectile_ the _ProtoTools > UnlitAlpha_ shader
>
> __c.__ Assign _Mat_Projectile_ to the _ProjectileHero_ GameObject in the Hierarchy

__4.__ Select _ProjectileHero_ in the Hierarchy and add a _Rigidbody_ component with these settings:
> __a.__ _Use Gravity_ to _false_ (unchecked)
>
> __b.__ _isKinematic_ to _false_ (unchecked)
>
> __c.__ _Constraints_: _Freeze Position Z_ and _Freeze Rotation X, Y_ and _Z_ (by checking them)

__5.__On the PorjectileHero GameObject's Box Collider component, set _Size.Z_ to _10_. This ensures that the projectile is able to hit anything that is slighty off of the XY (i.e. Z = 0) plane

__6.__ Create a new C# script named _ProjectileHero_
> __a.__ Move the _ProjectileHero_ script into the ___Scripts_ folder
>
> __b.__ Attach the _ProjectileHero_ script to the _ProjectileHero_ GameObject

__7.__ Save the Scene

__8.__ Attach a _BoundsCheck_ script component to _ProjectileHero_
> __a.__ Set __keepOnScreen__ to _false_ (unchecked)
>
> __b.__ Set __boundsType__ to _outset_
>
> __c.__ Set __radius__ to _1_

__9.__ Make ProjectileHero into a prefab:
> __a.__Drag _ProjectileHero_ from the Hierarchy into the __Prefabs_ folder in the Project pane
>
> __b.__ Delete the remaining _ProjectileHero_ instance from the Hierarchy

__10.__ Save the scene


## Giving _Hero the Ability to Shoot
__1.__ Open the _Hero_ C# script and add code

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
    public GameObject    projectilePrefab;
    public float    projectileSpeed = 40;
  
    [Header("Dynamic)]    [Range(0,4)]    [SerialField]
    private float _shieldLevel = 1;
    // public float    shieldLevel = 1;
    [Tooltip("This field holds a reference tothe last triggering GameObject")]
    private GameObject lastTriggerGo = null;
  
  
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
      float vAxis = Input.GetAxis("Vertical");
  
      // change transform.position based on the axis
      Vector3 pos = transform.position;
      pos.x += hAxis * speed * Time.deltaTime;
      pos.y += vAxis * speed * Time.deltaTime;
      transform.position = pos;
  
      // Rotate the ship to make it feel more dynamic
      transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rullMult,0);

      // Allow the ship to fire
      if (Input.GetKeyDown(KeyCode.Space)) {
        TempFire();
      }
    }


    void TempFire() {
      GameObject projGO = Instantiate<GameObject>(projectilePrefab);
      projGO.transform.position = transform.position;
      Rigidbody rigidB = projGO.GetComponent<RigidBody>();
      rigidB.velocity = Vector3.up * projectileSpeed;
    }


    void OnTriggerEnter(Collider other) {
      Transform rootT = other.gameObject.transform.root;
      GameObject fo = rootT.gameObject;
      // Debug.Log("Shield trigger hit by: ") +go.gmaeObject.name;

      // Make sure it's not the same trigger fo as last time
      if (go == lastTriggerGo) return;
      lastTriggerGo = go;

      Enemy enemy = go.GetComponent<Enemy>();
      if (enemy != null) {    // If the shield was triggered by an enemy
        shieldLevel--;        // Decreases the level of the shield by 1
        Destroy(go);          // ... and Destroy the enemy
      }
      else {
        Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
      }
    }


    public float shieldLevel {
      get {return (_shieldLevel);}
      private set {
        _shieldLevel = Mathf.Min(value, 4);

        // If the shield is going to be set to less than zero
        if (value < 0) {
          Destroy(this.gameObject);    // Destroy the Hero
          Main.HERO_DIED();
        }
      }
    }
  
    /*
      void Start() {...}
    */
}
```

__2.__ In Unity, select __Hero_ in the Hierarchy and assign _ProjectileHero_ from the Project pane to the _projectilePrefab_ of the Hero script

__3.__ Save and click _Play_


## Scripting the ProjectileHero
__1.__ Open the _ProjectileHero_ C# script and add code

```cs
// ProjectileHero.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  [RequireComponent(typeof(BoundsCheck))]

  public class ProjectileHero : MonoBehaviour {
    private BoundsCheck    bndCheck;

    void Awake() {
      bndCheck = GetComponent<BoundsCheck>();
    }


    void Update() {
      if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp)) {
        Destroy(gameObject);
      }
    }


    /*
      void Start() {...}
    /*
  }
```

__2.__ Save both this script and scene


## Allowing Porjectiles to Destroy Enemies
__1.__ Open the _Enemy_ C# script and make changes

```cs
// Enemy.cs
  
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  [RequireComponent(typeof(BoundsCheck))]
  
  public class Enemy : MonoBehaviour {
    [Header("Inscribed)]
    public float speed = 10f;        // The movement speed is 10m/s
    public float fireRate = 0.3;      // Seconds/shot (Unused)
    public float health = 10;        // Damage needed to destroy this enemy
    public int score = 100;          // Points earned for destroying this

    private BoundsCheck bndCheck;

    void Awake() {
      bndCheck = GetComponent<BoundsCheck>();
    }
  
    // This is a Property: A method that acts like a field
    public Vector3 pos {
      get {
        return this.transform.position;
      }
      set {
        this.transform.position = value;
      }
    }


    void Update() {
      Move();

      // Check whether this Enemy has gone off the bottom of the screen
      if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown)) {
        Destroy(gameObject);
      }

      /*
        if (!bndCheck.isonScreen) {
          if (pos.y < bndCheck.camHeight - bndCheck.radius) {
            // We're off the the bottom, so destroy this GameObject
            Destroy(gameObject);
          }
        }
      */
    }
  
  
    public virtual void Move() {
      Vector3 tempPos = pos;
      tempPos.y -= speed * Time.deltaTime;
      pos = tempPos;
    }


    void OnCollisionEnter(Collision coll) {
      GameObject otherGO = coll.gameObject;
      if (otherGO.GetComponent<ProjectileHero>() != null) {
        Destroy(otherGO);      // Destroy the Projectile
        Destroy(gameObject);    // Destroy this Enemy GameObject
      }
      else {
        Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
      }
    }
  
  
    /*
      void Start() {...}
    */
  }
```


# Enemy to Enemy_0
__1.__ Create an Enemy_0 script and attach it to the Enemy_0 prefab:
> __a.__ Create a new C# script named _Enemy_0_ in the ___Scripts_ folder
>
> __b.__ Drag this _Enemy_0_ script onto the _Enemy_0_ GameObject prefab in the __Prefabs_ folder
>
> __c.__ Select the _Enemy_0_ GameObject prefab in the __Prefabs_ folder and confirm that the _Enemy_0 (Script)_ component was attached (it eill be at the bottom of the Inspector)

__2.__ Remove the existing _Enemy (Script)_ component from the _Enemy_0_ prefab
> __a.__ Select the _Enemy_0_ GameObject prefab in the __Prefabs_ folder
>
> __b.__ In the Inspector, click the three vertical dots in the top-right corner of the _Enemy (Script)_ component and choose _Remove Component_

__3.__ Open the _Enemy_0_ script in VS and make changes

```cs
// Enemy_0.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_0 : Enemy {    // Enemy_0 extends the Enemy class
    /*
      void Start() {...}

      void Update() {...}
    */
  }
```

__4.__ Save the _Enemy_0_ script, return to Unity, and click _Play_. Enemy_0 GameObjects should still act the same


# Programming Other Enemies
__1.__ Inside the _Assets by Peter Burroughs > Other Prefabs folder_ are four Enemy_prefabs
> __a.__ Move the _Enemy_1, Enemy_2, Enemy_3,_ and _Enemy_4_ prefabs into the __Prefabs_ folder of the Project pane

__2.__ Create four new C# scripts named _Enemy_1, Enemy_2, Enemy_3,_ and _Enemy_4_

__3.__ Place these scripts into the ___Scripts_ folder in the Project pane

__4.__ Assign each of these four scripts to their respective _Enemy_#_ prefab in the Project pane

__5.__ Select each _Enemy_#_ prefab in the __Prefabs_ folder of the Project pane and set its Layer to _Enemy_. Always choose "Yes, change children" when asked


## Enemy_1
__1.__ Open the Enemy_1 script in VS and enter code

```cs
// Enemy_1.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_1 : Enemy {    // Enemy_1 also extends the Enemy class
    [Header("Enemy_1 Inscribed Fields")]
    [Tooltip("# of seconds for a fulle sine wave")]
    public float    waveFrequency = 2;
    [Tooltip("Sine wave width in meters")]
    public float    waveWidth = 4;
    [Tooltip("Amount the ship will roll left and right with the sine wave")]
    public float waveRotY = 45;

    private float    x0;    // The initial x value of pos
    private float    birthTime;

    void Start() {
      //  Set x0 to the intitial x position of Enemy_1
      x0 = pos.x

      birthTime = Time.time;
    }

    // Override the Move function on Enemy
    public override void Move() {
      /*
        Because pos is a property, can't directly set pos.x
        so get the pos as an editabel Vector3
      */
      Vector3 tempPos = pos;

      // theta adjust based on time
      float age = Time.time - birthTime;
      float theta = Mathf.PI * 2 * age / waveFrequency;
      float sin = Mathf.Sine(theta);
      tempPos.z = z0 + waveWidth * sin;
      pos = tempPos;

      // rotate a bit about y
      Vector3 rot = new Vector3(0, sin *waveRotY, 0);
      this.transform.rotation = Quaternion.Euler(rot);

      // base.Move() still handles the movement down in y
      base.Move();

      // print(bndCheck.isOnScreen);
    }

    /*
      void Update() {...}
    */
  }
```

__3.__ Back in Unity, select __MainCamera_ in the Hierarchy and in the _Main (Script)_ component change _Element_0_ of __prefabEnemies__ form Enemy_0 to Enemy_1. Allows to test with Enemy_1 instead of Enemy

__4.__ Click _Play_

__5.__ Stop playback in Unity

__6.__ Select the _Enemy_1_ prefab in the Project pane, and in the Inspector for the Enemy_1 _BoundsCheck_ component:
> __a.__ Set __boundsType__ to _outset_
>
> __b.__ Set __radius__ tp _2.5_
>
> __c.__ Set __keepOnScreen__ to _false_ (unchecked)

__7.__ Attach a BoundsCheck (Script) component to the other Enemy_# prefabs
> __a.__ With Enemy_1 prefab still selected, in the Inspector for Enemy_1, click the three vertical dots in the top-right corner of the _BoundsCheck (Script)_ component and choose _Copy Component_
>
> __b.__ Select the _Enemy_2_ prefab in the __Prefabs_ folder of the Project pane
>
> __c.__ Hold the _Shift_ key on the keyboard and click the _Enemy_4_ prefab. This selects all three prefabs that will change
>
> __d.__ In the Inspector for _3 Prefab Assets_, click the three vertical dots in the top-right corner of the _Transform_ component and choose _Paste Component as New_


### When is BoundsCheck bndCheck Being _private_ a Problem?
__1.__ Open the Enemy_1 script and uncommet the __print()__ line

```cs
// Enemy_1.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_1 : Enemy {    // Enemy_1 also extends the Enemy class
    [Header("Enemy_1 Inscribed Fields")]
    [Tooltip("# of seconds for a fulle sine wave")]
    public float    waveFrequency = 2;
    [Tooltip("Sine wave width in meters")]
    public float    waveWidth = 4;
    [Tooltip("Amount the ship will roll left and right with the sine wave")]
    public float waveRotY = 45;

    private float    x0;    // The initial x value of pos
    private float    birthTime;

    void Start() {
      //  Set x0 to the intitial x position of Enemy_1
      x0 = pos.x

      birthTime = Time.time;
    }

    // Override the Move function on Enemy
    public override void Move() {
      /*
        Because pos is a property, can't directly set pos.x
        so get the pos as an editabel Vector3
      */
      Vector3 tempPos = pos;

      // theta adjust based on time
      float age = Time.time - birthTime;
      float theta = Mathf.PI * 2 * age / waveFrequency;
      float sin = Mathf.Sine(theta);
      tempPos.z = z0 + waveWidth * sin;
      pos = tempPos;

      // rotate a bit about y
      Vector3 rot = new Vector3(0, sin *waveRotY, 0);
      this.transform.rotation = Quaternion.Euler(rot);

      // base.Move() still handles the movement down in y
      base.Move();

      print(bndCheck.isOnScreen);
    }

    /*
      void Update() {...}
    */
  }
```

__2.__ Open the Enemy script and change __bndCheck__ from ___private___ to ___protected___

```cs
// Enemy.cs
  
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  [RequireComponent(typeof(BoundsCheck))]
  
  public class Enemy : MonoBehaviour {
    [Header("Inscribed)]
    public float speed = 10f;        // The movement speed is 10m/s
    public float fireRate = 0.3;      // Seconds/shot (Unused)
    public float health = 10;        // Damage needed to destroy this enemy
    public int score = 100;          // Points earned for destroying this

    protected BoundsCheck bndCheck;    // Change bndCheck from private to protected

    void Awake() {
      bndCheck = GetComponent<BoundsCheck>();
    }
  
    // This is a Property: A method that acts like a field
    public Vector3 pos {
      get {
        return this.transform.position;
      }
      set {
        this.transform.position = value;
      }
    }


    void Update() {
      Move();

      // Check whether this Enemy has gone off the bottom of the screen
      if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown)) {
        Destroy(gameObject);
      }

      /*
        if (!bndCheck.isonScreen) {
          if (pos.y < bndCheck.camHeight - bndCheck.radius) {
            // We're off the the bottom, so destroy this GameObject
            Destroy(gameObject);
          }
        }
      */
    }
  
  
    public virtual void Move() {
      Vector3 tempPos = pos;
      tempPos.y -= speed * Time.deltaTime;
      pos = tempPos;
    }


    void OnCollisionEnter(Collision coll) {
      GameObject otherGO = coll.gameObject;
      if (otherGO.GetComponent<ProjectileHero>() != null) {
        Destroy(otherGO);      // Destroy the Projectile
        Destroy(gameObject);    // Destroy this Enemy GameObject
      }
      else {
        Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
      }
    }
  
  
    /*
      void Start() {...}
    */
  }
```

__3.__ Switch back to Unity and click _Play_

__4.__ Return to Enemy_1 script in VS and re-comment out the __print()__ line to stop all these messages

```cs
// Enemy_1.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_1 : Enemy {    // Enemy_1 also extends the Enemy class
    [Header("Enemy_1 Inscribed Fields")]
    [Tooltip("# of seconds for a fulle sine wave")]
    public float    waveFrequency = 2;
    [Tooltip("Sine wave width in meters")]
    public float    waveWidth = 4;
    [Tooltip("Amount the ship will roll left and right with the sine wave")]
    public float waveRotY = 45;

    private float    x0;    // The initial x value of pos
    private float    birthTime;

    void Start() {
      //  Set x0 to the intitial x position of Enemy_1
      x0 = pos.x

      birthTime = Time.time;
    }

    // Override the Move function on Enemy
    public override void Move() {
      /*
        Because pos is a property, can't directly set pos.x
        so get the pos as an editabel Vector3
      */
      Vector3 tempPos = pos;

      // theta adjust based on time
      float age = Time.time - birthTime;
      float theta = Mathf.PI * 2 * age / waveFrequency;
      float sin = Mathf.Sine(theta);
      tempPos.z = z0 + waveWidth * sin;
      pos = tempPos;

      // rotate a bit about y
      Vector3 rot = new Vector3(0, sin *waveRotY, 0);
      this.transform.rotation = Quaternion.Euler(rot);

      // base.Move() still handles the movement down in y
      base.Move();

      // print(bndCheck.isOnScreen);
    }

    /*
      void Update() {...}
    */
  }
```

__5.__ Save the _Enemy_1_ script


## Enemy_2
__1.__ Select the _Enemy_2_ prefab in the __Prefabs_ folder of the Project pane

__2.__ In the Enemy_2 prefab BoundsCheck Inspector
* __boundsType__ = _outset_
* __radius__ = _3_
* __keepOnScreen__ = _false_

__3.__ Open the _Enemy_2_ C# script in VS and enter the code

```cs
// Enemy_2.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_2 : Enemy {
    [Header("Enemy_2 Inscribed Fields")]
    public float    lifeTime = 10;

    // Enemy_2 uses a Sine wave to modify a 2-point linear interpolation
    [Tooltip("Determines how much the Sine wave will ease the interpolation")]
    public float    sinEccentricity = 0.6f;

    [Header("Enemy_2 Private Fields")]
    [SerializeField] private float    birthTime;    // Interpolation start time
    [SerializeField] private Vector3    p0, p1;      // Lerp_points


    void Start() {
      // Pick any point on the left side of the screen
      p0 = Vector3.zero;
      p0.x = -bndCheck.camWidth - bndCheck.radius;
      p0.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

      // Pick any point on the right side of the screen
      p1 = Vector3.zero
      p1.x = bndCheck.camWidth + bndCheck.radius;
      p1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

      // Possibly swap sides
      if (Random.value > 0.5f) {
        // Set the .x of each point to its negative will move it to the other side of the screen
        p0.x *= -1;
        p1.x *= -1;
      }

      // Set the birthTime to the current time
      birthTime = Time.time;
    }


    public override void Move() {
      // Linear interpolations work based on a u value between 0 & 1
      float u = (Time.time - birthTime) / lifeTime;

      // if u > 1, then it has been longer than lifeTime since brithTime
      if (u > 1) {
        // This Enemy_2 has finished its life
        Destroy(this.gameObject);
        return;
      }

      // Adjust u by adding a U curve based on a Sine wave
      u = u + sinEccentricity*(Mathf.Sin(u*Mathf.PI*2));

      // Interpolate the two linear interpolation points
      pos = (1 - u) * p0 + u * p1;

      // Note that Enemy_2 doe NOT call the base.Move() method
    }


    /*
      void Update() {...}
    */
  }
```

__4.__ In the _MainCamera _Main (Script)_ Inspector, swap the Enemy_2 prefab into the _Enemy_0_ slot of __Main.prefabEnemies__ and click _Play_

__5.__ While in playm mode adjust the easing curve __sinEccentricity__ value on the _Enemy_2_ prefab to see how it effects the motion

__6.__ Click _Play_ again to stop playback


### Improving Enemy_2 with an AnimationCurve
__1.__ Open the _Enemy_2_ C# script in VS and make changes

```cs
// Enemy_2.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_2 : Enemy {
    [Header("Enemy_2 Inscribed Fields")]
    public float    lifeTime = 10;

    // Enemy_2 uses a Sine wave to modify a 2-point linear interpolation
    [Tooltip("Determines how much the Sine wave will ease the interpolation")]
    public float    sinEccentricity = 0.6f;
    public AnimationCurve    rotCurve;

    [Header("Enemy_2 Private Fields")]
    [SerializeField] private float    birthTime;    // Interpolation start time
    [SerializeField] private Vector3    p0, p1;      // Lerp_points


    void Start() {
      // Pick any point on the left side of the screen
      p0 = Vector3.zero;
      p0.x = -bndCheck.camWidth - bndCheck.radius;
      p0.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

      // Pick any point on the right side of the screen
      p1 = Vector3.zero
      p1.x = bndCheck.camWidth + bndCheck.radius;
      p1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

      // Possibly swap sides
      if (Random.value > 0.5f) {
        // Set the .x of each point to its negative will move it to the other side of the screen
        p0.x *= -1;
        p1.x *= -1;
      }

      // Set the birthTime to the current time
      birthTime = Time.time;
    }


    public override void Move() {
      // Linear interpolations work based on a u value between 0 & 1
      float u = (Time.time - birthTime) / lifeTime;

      // if u > 1, then it has been longer than lifeTime since brithTime
      if (u > 1) {
        // This Enemy_2 has finished its life
        Destroy(this.gameObject);
        return;
      }

      // Use the AnimationCurve to set the rotation about Y
      float shipRot = rotCurve.Evaluate(u) * 360;
      if (p0.x > p1.x) {
        shipRot = -shipRot;
      }
      transform.rotation = Quaternion.Euler(0, shipRot, 0);

      // Adjust u by adding a U curve based on a Sine wave
      u = u + sinEccentricity*(Mathf.Sin(u*Mathf.PI*2));

      // Interpolate the two linear interpolation points
      pos = (1 - u) * p0 + u * p1;

      // Note that Enemy_2 doe NOT call the base.Move() method
    }


    /*
      void Update() {...}
    */
  }
```

__2.__ Save the _Enemy_2_ script and return to Unity

__3.__ Select the _Enemy_2_ prefab in the __Prefabs_ folder of the Project pane. A dark rectangle next to the field name _rotCurve_ in the _Enemy_2 Inscribed Fields_ section of the _Enemy_2 (Script)_ Inspector
> __a.__ Click the _dark grey rectangle_ to open the Curve Editor window
>
> __b.__ In the Curve Editor window, click the _second_ small curve shown at the bottom of the window (the one that shows a straight line moving from the bottom left to top right). This will place that curve in the window, giving points at [0.0, 0.0] and [1.0, 1.0]

__4.__ Add 4 additional points
> __a.__ Place a point at [0.1, 0.0]
>
> __b.__ Place a point at [0.4, 0.5]
>
> __c.__ Place a point at [0.6, 0.5]
>
> __d.__ Place a point at [0.9, 1.0]

__5.__ Close the curve Editor window, will be a smaller version of the curve just made

__6.__ Click _Play_ in Unity, Enemy_2 ship now roll as they change direction


### Improving the Enmey_2 AnimationCurve Even More
__1.__ Open the _Enemy_2_ C# script in VS and make changes

```cs
// Enemy_2.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_2 : Enemy {
    [Header("Enemy_2 Inscribed Fields")]
    public float    lifeTime = 10;

    // Enemy_2 uses a Sine wave to modify a 2-point linear interpolation
    [Tooltip("Determines how much the Sine wave will ease the interpolation")]
    public float    sinEccentricity = 0.6f;
    public AnimationCurve    rotCurve;

    [Header("Enemy_2 Private Fields")]
    [SerializeField] private float    birthTime;    // Interpolation start time
    [SerializeField] private Vector3    p0, p1;      // Lerp_points

    private Quaternion    baseRotation;


    void Start() {
      // Pick any point on the left side of the screen
      p0 = Vector3.zero;
      p0.x = -bndCheck.camWidth - bndCheck.radius;
      p0.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

      // Pick any point on the right side of the screen
      p1 = Vector3.zero
      p1.x = bndCheck.camWidth + bndCheck.radius;
      p1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

      // Possibly swap sides
      if (Random.value > 0.5f) {
        // Set the .x of each point to its negative will move it to the other side of the screen
        p0.x *= -1;
        p1.x *= -1;
      }

      // Set the birthTime to the current time
      birthTime = Time.time;

      // Set up the initial ship rotation
      transform.position = p0;
      transform.LookAt(p1, Vector3.back);
      baseRotation = transform.rotation;
    }


    public override void Move() {
      // Linear interpolations work based on a u value between 0 & 1
      float u = (Time.time - birthTime) / lifeTime;

      // if u > 1, then it has been longer than lifeTime since brithTime
      if (u > 1) {
        // This Enemy_2 has finished its life
        Destroy(this.gameObject);
        return;
      }

      // Use the AnimationCurve to set the rotation about Y
      float shipRot = rotCurve.Evaluate(u) * 360;
      /*
        if (p0.x > p1.x) {
          shipRot = -shipRot;
        }

        transform.rotation = Quaternion.Euler(0, shipRot, 0);
      */

      transform.rotation = baseRotation * Quaternion.Euler(-shipRot, 0, 0);

      // Adjust u by adding a U curve based on a Sine wave
      u = u + sinEccentricity*(Mathf.Sin(u*Mathf.PI*2));

      // Interpolate the two linear interpolation points
      pos = (1 - u) * p0 + u * p1;

      // Note that Enemy_2 doe NOT call the base.Move() method
    }


    /*
      void Update() {...}
    */
  }
```

__2.__ Save the _Enemy_2_ script and return to Unity

__3.__ Double-click the _Enemy_2_ prefab in the __Prefabs_ folder of the Project pane to open it in the Prefab Editor view

__4.__ In the Prefab Editor Hierarchy for Enemy_2, click the _disclosure triangle_ next to Enemy_2 to see its child _enemy2_ (the original model for this ship)

__5.__ Select this _enemy2_ model

__6.__ Set the _rotation_ of the enemry2 transform to [0, 0, 0]

__7.__ Click the _Scenes_ button at the top of the Scene pane to exit Prefab Editor mode

__8.__ Save the _Scene_ and click _Play_ in Unity


## Enemy_3
### A Reusable, Fast Bezier Method for Any Number of Points
__1.__ Create a new C# script named _Utils_ and place it in the __Scripts folder

__2.__ Open the _Utiles_ script in VS and enter code

```cs
// Utils.cs

  using System.Collections;
  using System.Collections.Generic;
  using UntiyEngine;

  public class Utils : MonoBehaviour {
    // == Bezier Curves ============================================

    /*
      While most Bezier curves are 3 and 4 points, it is possible to have any number of points using this recursive function

      <param name="u">The amount of interpolation [0...1]</param>
      <param name="points">An array of Vector3s to interpolate</param>
    */

    static public Vector3 Bezier(float u, params Vactor3[] points) {
      // Set up the array
      Vector3[,] vArr = new Vector3[points.Length, points.Length];

      // Fill the last roe of vArr with the elements of vList
      int r = points.Length - 1;
      for(int c = 0; c < points.Length; c++) {
        vArr[r, c] = vList[c];
      }

      // Iterate over all remaining rows and interpolate points at each one
      for (r--; r >= 0; r--) {
        for (int c = 0; c <= r; c++) {
          vArr[r, c] = Vector3.LerpUnclamped(vArr[r+1, c], vArr[r+1, c+1], u);
        }
      }

      // When complete, vArr[0, 0] holds the final interpolated value
      return vArr[0, 0];
    }


    /*
      void Start() {...}
      void Update() {...}
    */
  }
```

__3.__ Save the _Utils_ script in VS and return to Unity


### Implementing a 3-Point Bezier Curve in the Enemy_3 Script
__1.__ Select the _Enemy_3_ prefab in the __Prefabs_ folder of the Project pane

__2.__ In the Enemy_3 prefab _BoundsCheck_ Inspector
* __boundsType__ = _outset_
* __radius__ = _3_
* __keepOnScreen__ = _false_

__3.__ Open _Enemy_3_ script and enter the code

```cs
// Enemy_3.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_3 : Enemy {
    [Header("Enemy_3 Inscribed Fields")]
    public float    lifeTime = 5;
    public Vector2 midpointYRange = new Vector2(1.5f, 3);
    [Tooltip("If true, the Bezier points & path are drawn in the Scene pane")]
    public bool drawDebugInfo = true;

    [Header("Enemy_3 Private Fields")]
    [SeralizeField]
    private Vector3[] points;    // The three points for the Bezier curve
    [SerializeField]
    private float    birthTime;

    // Again, Starts works well because it is not used in the Enemy superclass
    void Start() {
      points = new Vector3[3];    // Initialize points

      // The start process has already been set by Main.SpawnEnemy()
      points[0] = pos;

      // Set xMin and xMax the same way that Main.SpawnEnemy() does
      float xMin = -bndCheck.camWidth + bndCheck.radius;
      float xMax = bndCheck.camWidth - bndCheck.radius;

      // Pick a random middle position in the bottom half of the screen
      points[1] = Vector3.zero
      points[1].x = Random.Range(xMin, xMax);
      float midYMult = Random.Range(midpointYRange[0], midpointYRange[1]);
      points[1].y = -bndCheck.camHeight * midYMult;

      // Pick a random final position above the top of the screen
      points[2] = Vector3.zero;
      points[2].y = pos.y;
      points[2].x = Random.Range(xMin, xMax);

      // Seet the birthTime to current time
      birthTime = Time.time;

      if (drawDebugInfor) {
        DrawDebug();
      } 
    }


    public override void Move() {
      // Bezier curves work based on a u value between 0 & 1
      float u = (Time.time - birthTime) / lifeTime;

      if (u > 1) {
        // This Enemy_3 has finished its life
        Destroy(this.gameObject);
        return;
      }

      transform.rotation = Quaternion.Euler(u * 180, 0, 0);

      // Interpolate the three Bezier curve points
      pos = Utils.Bezier(u points);

      // Enemy_3 does not call base.Move()
    }


    void DrawDebug() {
      // Draw the three points
      Debug.DrawLine(points[0], points[1], Color.cyan, lifeTime);
      Debug.DrawLine(points[1], points[2], Color.yellow, lifeTime);

      // Draw the Bezier Curve
      float numSections = 20;
      Vector3 prevPoint = points[0];
      Color    col;
      Vector3    pt;
      for (int i = 1; i < numSections; i++) {
        float u = i / numSections;
        pt = Utils.Bezier(u, points);
        col = Color.Lerp(Color.cyan, Color.yellow, u);
        Debug.DrawLine(prevPoint, pt, col, lifeTime);
        prevPoint = pt;
      }
    }
  }
```

__4.__ Save the _Enemy_3_ script in VS and return to Unity

__5.__ Select __MainCamera_ in the Hierarchy and set _Element_0_ of __prefabEnemies__ to the Enemy_3 prefab and save the scene

__6.__ Click _Play_ to see the movement of these new enemies

__7.__ Add code to _Enemy_3_ script


```cs
// Enemy_3.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Enemy_3 : Enemy {
    [Header("Enemy_3 Inscribed Fields")]
    public float    lifeTime = 5;
    public Vector2 midpointYRange = new Vector2(1.5f, 3);
    [Tooltip("If true, the Bezier points & path are drawn in the Scene pane")]
    public bool drawDebugInfo = true;

    [Header("Enemy_3 Private Fields")]
    [SeralizeField]
    private Vector3[] points;    // The three points for the Bezier curve
    [SerializeField]
    private float    birthTime;

    // Again, Starts works well because it is not used in the Enemy superclass
    void Start() {
      points = new Vector3[3];    // Initialize points

      // The start process has already been set by Main.SpawnEnemy()
      points[0] = pos;

      // Set xMin and xMax the same way that Main.SpawnEnemy() does
      float xMin = -bndCheck.camWidth + bndCheck.radius;
      float xMax = bndCheck.camWidth - bndCheck.radius;

      // Pick a random middle position in the bottom half of the screen
      points[1] = Vector3.zero
      points[1].x = Random.Range(xMin, xMax);
      float midYMult = Random.Range(midpointYRange[0], midpointYRange[1]);
      points[1].y = -bndCheck.camHeight * midYMult;

      // Pick a random final position above the top of the screen
      points[2] = Vector3.zero;
      points[2].y = pos.y;
      points[2].x = Random.Range(xMin, xMax);

      // Seet the birthTime to current time
      birthTime = Time.time;

      if (drawDebugInfor) {
        DrawDebug();
      } 
    }


    public override void Move() {
      // Bezier curves work based on a u value between 0 & 1
      float u = (Time.time - birthTime) / lifeTime;

      if (u > 1) {
        // This Enemy_3 has finished its life
        Destroy(this.gameObject);
        return;
      }

      transform.rotation = Quaternion.Euler(u * 180, 0, 0);

      // Interpolate the three Bezier curve points
      u = u - 0.1f * Mathf.Sin(u * Mathf.PI * 2)
      pos = Utils.Bezier(u points);

      // Enemy_3 does not call base.Move()
    }


    void DrawDebug() {
      // Draw the three points
      Debug.DrawLine(points[0], points[1], Color.cyan, lifeTime);
      Debug.DrawLine(points[1], points[2], Color.yellow, lifeTime);

      // Draw the Bezier Curve
      float numSections = 20;
      Vector3 prevPoint = points[0];
      Color    col;
      Vector3    pt;
      for (int i = 1; i < numSections; i++) {
        float u = i / numSections;
        pt = Utils.Bezier(u, points);
        col = Color.Lerp(Color.cyan, Color.yellow, u);
        Debug.DrawLine(prevPoint, pt, col, lifeTime);
        prevPoint = pt;
      }
    }
  }
```

__8.__ Select the _Enemy_3_ prefab in the Project pane, and then in the _Enemy_3 (Script)_ Inspector, uncheck __drawDebugInfo__


# Shooting Revisited
## The eWeaponType Enum
__1.__ Right-click on the ___Scripts_ folder in the Project pane and choose _Create > C# Script_. This creates _NewBehaviourScript_ in the __Scripts folder

__2.__ Rename NewBehaviourScript to _Weapon_

__3.__ Open the _Weapon_ script in VS and enter the code

```cs
// Weapon.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  /*
    This is an enum of the various possible weapon types
    It also includes a "shield" type to allow a chield PowerUp
    Items marked [NI] below are Not Implemented
  */

  public enum eWeaponType {
    none,        // The default / no weapon
    blaster,    // A simple blaster
    spread,      // Multiple shots simultaneously
    phaser,      // [NI] Shots that move in waves
    missile,    // [NI] Homin missiles
    laser,      // [NI] Damage over time
    shield      // Raise shieldLevel
  }

  public class Weapon : MonoBehaviour {

  }
```


## The Serializable WeaponDefinition Class
__1.__ Open the Weapon script and enter the code

```cs
// Weapon.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  /*
    This is an enum of the various possible weapon types
    It also includes a "shield" type to allow a chield PowerUp
    Items marked [NI] below are Not Implemented
  */

  public enum eWeaponType {
    none,        // The default / no weapon
    blaster,    // A simple blaster
    spread,      // Multiple shots simultaneously
    phaser,      // [NI] Shots that move in waves
    missile,    // [NI] Homin missiles
    laser,      // [NI] Damage over time
    shield      // Raise shieldLevel
  }

  /*
    The WeaponDefinition class allow to set the properties
      of a specific weapon in the Inspector. The main class has
      an array of WeaponDefinitions that make this possible
  */
  [System.Serializable]
  public class WeaponDefinition {
    public eWeaponType    type = eWeaponType.none;

    [Tooltip("Letter to show on the PowerUp Cube")]
    public string    letter;

    [Tooltip("Color of PowerUp Cube")]
    public Color    powerUpColor = Color.white;

    [Tooltip("Prefab of Weapon model that is attached to the Player Ship")]
    public GameObject    weaponModelPrefab;

    [Tooltip("Prefab of the Projectile that is fired")]
    public GameObject    projectilePrefab;

    [Tooltip("Color of the Projectile that is fired")]
    public Color    projectileColor = Color.white;

    [Tooltip("Damage caused when a single Projectile hits an Enemy")]
    public float    damageOnHit = 0;

    [Tooltip("Damage caused per second by a Laser [Not Implemented]")]
    public float    damagePerSec = 0;

    [Tooltip("Seconds to delay between shots")]
    public float     delayBetweenShots = 0;

    [Tooltip("Velocity of individual Projectiles")]
    public float    velocity = 50;
  }

  public class Weapon : MonoBehaviour {

  }
```


### Modifying Main to Use WeaponDefinition and eWeaponType
__1.__ Open the _Main_ C# script in VS and add code

```cs
// Main.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.SceneManagement;    // Enables the loading & reloading of scenes
  
  public class Main : MonoBehaviour {
    static private Main S;    // A private singleton for Main
  
    [Header("Inscribed")]
    public bool spawnEnemies = true;
    public GameObject[] prefabEnemies;          // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f;    // # Enemies spawned/second
    public float enemyInsetDefault = 1.5;       // Inset from the sides
    public float gameRestartDelay = 2;
    public WeaponDefinition[] weaponDefinitions;
  
    private BoundsCheck bndCheck;
  
    void Awake() {
      S = this;
  
      // Set bndCheck to reference the BoundsCheck component on this GameObject
      bndCheck = GetComponent<BoundsCheck>();
  
      // Invoke SpawnEnemey() once (in 2 seconds, based on default values)
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }
  
  
    public void SpawnEnemy() {
      // If spawnEnemies is false, skip to the next invoke of SpawEnemy()
      if (!spawnEnemies) {
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
        return;
      }

      // Pick a random Enemy prefab to instantiate
      int ndx = Random.Range(0, prefabEnemies.Length);
      GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
  
      // position the Enemy above the screen with a random x position
      float enemyInset = enemyInsetDefault;
      if (go.GetComponent<BoundsCheck>() != null) {
        enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
      }
  
      // Set the initial position for the spawned Enemy
      Vector3 pos = Vector3.zero;
      float xMin = -bndCheck.camWidth + enemyInset;
      float xMax = bndCheck.camWidth - enemyInset;
      pos.x = Random.Range(xMin, xMax);
      pos.y = bndCheck.camHeight + enemyInset;
      go.transform.position = pos;
  
      // Invoke SpawnEnemy() again
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }


    void DelayRestart() {
      // Invoke the Restart() method in gameRestartDelay seconds
      Invoke(nameof(Restart), gameRestartDelay);
    }


    void Restart() {
      SceneManager.LoadScene("__Scene_0");
    }


    static public void HERO_DIED() {
      S.DelayedRestart();
    }
  
    /*
      void Start() {...}
  
      void Update() {...}
    */
  }
```

__2__ Save the _Main_ script and return to Unity

__3.__ Select __MainCamera_ in the Hierarchy

__4.__ Click the _disclosure triangle_ next to weaponDefinitions in the Inspector and set the _Size_ of the array to _3_

__5.__ Enter settings for the three __WeaponDefinitions__
* Element 0
  * __Type:__ Blaster
  * __Letter:__ B
  * __Power Up Color:__ White
  * __Weapon Model Prefab:__ weapon_Blaster
  * __Projectile Prefab:__ ProjectileHero
  * __Projectile Color:__ White
  * __Damage On Hit:__ 1
  * __Delay Between Shots:__ 0.2

* Element 1
  * __Type:__ Spread
  * __Letter:__ S
  * __Power Up Color:__ Cyan
  * __Weapon Model Prefab:__ weaponSpreadShot
  * __Projectile Prefab:__ ProjectileHero
  * __Projectile Color:__ Cyan
  * __Damage On Hit:__ 1
  * __Delay Between Shots:__ 0.4

* Element 2
  * __Type:__ Shield
  * __Letter:__ O
  * __Power Up Color:__ Green

__6.__ Save Scene


## A Generic Dictionary for WeaponDefinitions
__1.__ Open the _Main_ script in VS and enter the code

```cs
// Main.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.SceneManagement;    // Enables the loading & reloading of scenes
  
  public class Main : MonoBehaviour {
    static private Main S;    // A private singleton for Main
  
    [Header("Inscribed")]
    public bool spawnEnemies = true;
    public GameObject[] prefabEnemies;          // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f;    // # Enemies spawned/second
    public float enemyInsetDefault = 1.5;       // Inset from the sides
    public float gameRestartDelay = 2;
    public WeaponDefinition[] weaponDefinitions;
  
    private BoundsCheck bndCheck;
  
    void Awake() {
      S = this;
  
      // Set bndCheck to reference the BoundsCheck component on this GameObject
      bndCheck = GetComponent<BoundsCheck>();
  
      // Invoke SpawnEnemey() once (in 2 seconds, based on default values)
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);

      // A generic Dictionary with eWeaponType as the key
      WEAP_DICT = new Dictionary<eWeaponType, WeaponDefinition>();
      foreach (WeaponDefinition def in weaponDefinitions) {
        WEAP_DICT[def.type] = def;
      }
    }
  
  
    public void SpawnEnemy() {
      // If spawnEnemies is false, skip to the next invoke of SpawEnemy()
      if (!spawnEnemies) {
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
        return;
      }

      // Pick a random Enemy prefab to instantiate
      int ndx = Random.Range(0, prefabEnemies.Length);
      GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
  
      // position the Enemy above the screen with a random x position
      float enemyInset = enemyInsetDefault;
      if (go.GetComponent<BoundsCheck>() != null) {
        enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
      }
  
      // Set the initial position for the spawned Enemy
      Vector3 pos = Vector3.zero;
      float xMin = -bndCheck.camWidth + enemyInset;
      float xMax = bndCheck.camWidth - enemyInset;
      pos.x = Random.Range(xMin, xMax);
      pos.y = bndCheck.camHeight + enemyInset;
      go.transform.position = pos;
  
      // Invoke SpawnEnemy() again
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }


    void DelayRestart() {
      // Invoke the Restart() method in gameRestartDelay seconds
      Invoke(nameof(Restart), gameRestartDelay);
    }


    void Restart() {
      SceneManager.LoadScene("__Scene_0");
    }


    static public void HERO_DIED() {
      S.DelayedRestart();
    }
  
    /*
      void Start() {...}
  
      void Update() {...}
    */
  }
```

__2.__ Add code to the end of the Main C# script

```cs
// Main.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.SceneManagement;    // Enables the loading & reloading of scenes
  
  public class Main : MonoBehaviour {
    static private Main S;    // A private singleton for Main
  
    [Header("Inscribed")]
    public bool spawnEnemies = true;
    public GameObject[] prefabEnemies;          // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f;    // # Enemies spawned/second
    public float enemyInsetDefault = 1.5;       // Inset from the sides
    public float gameRestartDelay = 2;
    public WeaponDefinition[] weaponDefinitions;
  
    private BoundsCheck bndCheck;
  
    void Awake() {
      S = this;
  
      // Set bndCheck to reference the BoundsCheck component on this GameObject
      bndCheck = GetComponent<BoundsCheck>();
  
      // Invoke SpawnEnemey() once (in 2 seconds, based on default values)
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);

      // A generic Dictionary with eWeaponType as the key
      WEAP_DICT = new Dictionary<eWeaponType, WeaponDefinition>();
      foreach (WeaponDefinition def in weaponDefinitions) {
        WEAP_DICT[def.type] = def;
      }
    }
  
  
    public void SpawnEnemy() {
      // If spawnEnemies is false, skip to the next invoke of SpawEnemy()
      if (!spawnEnemies) {
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
        return;
      }

      // Pick a random Enemy prefab to instantiate
      int ndx = Random.Range(0, prefabEnemies.Length);
      GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
  
      // position the Enemy above the screen with a random x position
      float enemyInset = enemyInsetDefault;
      if (go.GetComponent<BoundsCheck>() != null) {
        enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
      }
  
      // Set the initial position for the spawned Enemy
      Vector3 pos = Vector3.zero;
      float xMin = -bndCheck.camWidth + enemyInset;
      float xMax = bndCheck.camWidth - enemyInset;
      pos.x = Random.Range(xMin, xMax);
      pos.y = bndCheck.camHeight + enemyInset;
      go.transform.position = pos;
  
      // Invoke SpawnEnemy() again
      Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }


    void DelayRestart() {
      // Invoke the Restart() method in gameRestartDelay seconds
      Invoke(nameof(Restart), gameRestartDelay);
    }


    void Restart() {
      SceneManager.LoadScene("__Scene_0");
    }


    static public void HERO_DIED() {
      S.DelayedRestart();
    }


    /*
        Static functino that gets a WeaponDefinition from the WEAP_DICT static
            protected field of the Main class

        <returns>The WeaponDefinition, of it there is no WeaponDefinition with
            the eWeaponType passes in, returns a new WeaponDefinition with a
            eWeaponType of eWeaponType.none</returns>

        <param name = "wt">The eWeaponType of the desored WeaponDefinition</param>
    */
    static public WeaponDefinition GET_WEAPON_DEFINITION(eWeaponType wt) {
        if (WEAP_DICT.ContainKey(wt)) {
            return(WEAP_DICT);
        }

        /*
            If no entry of the correct type exists to WEAP_DICT, return a new
                WeaponDefinition with a type of eWeaponType.none (the default value)
        */
        return(new WeaponDefinition());
    }

  
    /*
      void Start() {...}
  
      void Update() {...}
    */
  }
```


### Modifying the ProjectileHero Class to Use WeaponDefinitions
__1.__ Open the _ProjectileHero_ class in VS

__2.__ Match code for the ProjectileHero class to match code

```cs
// ProjectileHero.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  [RequireComponent(typeof(BoundsCheck))]

  public class ProjectileHero : MonoBehaviour {
    private BoundsCheck    bndCheck;
    private Renderer        rend;

    [Header("Dynamic")]
    public Rigidbody        rigid;
    [SeralizeField]
    private eWeaponType     _type;

    // This public property masks the private field _type
    public eWeaponType      type {
        get {return(_type);}
        get {SetType(value);}
    }

    void Awake() {
      bndCheck = GetComponent<BoundsCheck>();
      rend = GetComponent<Renderer>();
      rigid = GetComponent<RigidBody>();
    }


    void Update() {
      if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp)) {
        Destroy(gameObject);
      }
    }


    /*
        Set the _type private field and colors this projectile to match the 
            WeaponDefinition
        <param name = "eType">The eWeaponType to use</param>
    */
    public void SetType(eWeaponType eType) {
        _type = eType;
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION(_type);
        rend.material.color = def.projectileColor;
    }


    // Allows Weapon to easily set the velocity of this ProjectileHero
    public Vector3 vel {
        get {return rigid.velocity;}
        get {rigid.velocity = value;}
    }


    /*
      void Start() {...}
    /*
  }
```


## Using a Delegate Event to Fire
__1.__ Delegates are like nicknames for one or more functions that can be called with a single call to a delegate

__2.__ Add the following code to the _Hero_ class

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
    public GameObject    projectilePrefab;
    public float    projectileSpeed = 40;
  
    [Header("Dynamic)]    [Range(0,4)]    [SerialField]
    private float _shieldLevel = 1;
    // public float    shieldLevel = 1;
    [Tooltip("This field holds a reference tothe last triggering GameObject")]
    private GameObject lastTriggerGo = null;
    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate event named fireEvent
    public even WeaponFireDelegate fireEvent;
  
  
    void Awake() {
      if (S == null) {
        S = this;    // Set the Singleton onlly if it's null
      }
      else {
        Debug.LogError("Hero.Awake() - Attempt to assign second Hero.S!");
      }

      fireEvent += TempFire;
    }
  
  
    void Update() {
      // Pull in information from the Input class
      float hAxis = Input.GetAxis("Horizontal");
      float vAxis = Input.GetAxis("Vertical");
  
      // change transform.position based on the axis
      Vector3 pos = transform.position;
      pos.x += hAxis * speed * Time.deltaTime;
      pos.y += vAxis * speed * Time.deltaTime;
      transform.position = pos;
  
      // Rotate the ship to make it feel more dynamic
      transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rullMult,0);

      /*
          // Allow the ship to fire
          if (Input.GetKeyDown(KeyCode.Space)) {
            TempFire();
          }
      */

      // Use the fireEvent to fire Weapons when the Spacebar is pressed
      if (Input.GetAxis("Jump") == 1 && fireEvent != null) {
            fireEvent();
      }
    }


    void TempFire() {
      GameObject projGO = Instantiate<GameObject>(projectilePrefab);
      projGO.transform.position = transform.position;
      Rigidbody rigidB = projGO.GetComponent<RigidBody>();
      // rigidB.velocity = Vector3.up * projectileSpeed;

      ProjectileHero proj = projGO.GetComponent<ProjectileHero>();
      proj.type = eWeaponType.blaster;
      float tSpeed = Main.GET_WEAPON_DEFINITION(proj.type).velocity;
      rigidB.velocity = Vector3.up * tSpeed;
    }


    void OnTriggerEnter(Collider other) {
      Transform rootT = other.gameObject.transform.root;
      GameObject fo = rootT.gameObject;
      // Debug.Log("Shield trigger hit by: ") +go.gmaeObject.name;

      // Make sure it's not the same trigger fo as last time
      if (go == lastTriggerGo) return;
      lastTriggerGo = go;

      Enemy enemy = go.GetComponent<Enemy>();
      if (enemy != null) {    // If the shield was triggered by an enemy
        shieldLevel--;        // Decreases the level of the shield by 1
        Destroy(go);          // ... and Destroy the enemy
      }
      else {
        Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
      }
    }


    public float shieldLevel {
      get {return (_shieldLevel);}
      private set {
        _shieldLevel = Mathf.Min(value, 4);

        // If the shield is going to be set to less than zero
        if (value < 0) {
          Destroy(this.gameObject);    // Destroy the Hero
          Main.HERO_DIED();
        }
      }
    }
  
    /*
      void Start() {...}
    */
}
```

__3.__ Please _Save All_ in VS and then return to Unity

__4.__ Click _Play_ in Unity and try firing


## Creating a Weapon GameObject to Fire Projectiles
__1.__ In the Hierarchy, create an empty GameObject, name it _Weapon_, and giv it the following transforms and empty child named _ShotPoint_.
* Weapon (Empty)
    * P:[0, 0, 0]
    * R:[0, 0, 0]
    * S:[1, 1, 1]
* ShotPoint (Empty)
    * P:[0, 0, 1]
    * R:[0, 0, 0]
    * S:[1, 1, 1]

__2.__ Attach the _Weapon_ C# script to the Weapon GameObject in the Hierarchy

__3.__ Drag the _Weapon_ GameObject into the __Prefabs_ folder in the Project pane to make it a prefab

__4.__ Delete the remaining _Weapon_ instance in the Hierarchy

__5.__ Drag the _Weapon_ prefab from the __Prefabs_ folder onto __Hero > heroShip > hardpoint_0_ in the Hierarchy, making it a child of hardpoint_0

__6.__ Double-click that the Weapon _position_ in the Inspector is _[0, 0, 0]_ and that its scale is _[1, 1, 1]_. This should place the Weapon o the nose of the _Hero ship

__7.__ Save the scene


### Adding Fireing to the Weapon C# Script
__1.__ Start by disabling the __fireEvent__ use of the __TempFire()__ method in Hero. Open the Hero C# script in VS and copy code

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
    public GameObject    projectilePrefab;
    public float    projectileSpeed = 40;
  
    [Header("Dynamic)]    [Range(0,4)]    [SerialField]
    private float _shieldLevel = 1;
    // public float    shieldLevel = 1;
    [Tooltip("This field holds a reference tothe last triggering GameObject")]
    private GameObject lastTriggerGo = null;
    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate event named fireEvent
    public even WeaponFireDelegate fireEvent;
  
  
    void Awake() {
      if (S == null) {
        S = this;    // Set the Singleton onlly if it's null
      }
      else {
        Debug.LogError("Hero.Awake() - Attempt to assign second Hero.S!");
      }

      // fireEvent += TempFire;
    }
  
  
    void Update() {
      // Pull in information from the Input class
      float hAxis = Input.GetAxis("Horizontal");
      float vAxis = Input.GetAxis("Vertical");
  
      // change transform.position based on the axis
      Vector3 pos = transform.position;
      pos.x += hAxis * speed * Time.deltaTime;
      pos.y += vAxis * speed * Time.deltaTime;
      transform.position = pos;
  
      // Rotate the ship to make it feel more dynamic
      transform.rotation = Quaternion.Euler(vAxis*pitchMult,hAxis*rullMult,0);

      /*
          // Allow the ship to fire
          if (Input.GetKeyDown(KeyCode.Space)) {
            TempFire();
          }
      */

      // Use the fireEvent to fire Weapons when the Spacebar is pressed
      if (Input.GetAxis("Jump") == 1 && fireEvent != null) {
            fireEvent();
      }
    }

    /*
        void TempFire() {
          GameObject projGO = Instantiate<GameObject>(projectilePrefab);
          projGO.transform.position = transform.position;
          Rigidbody rigidB = projGO.GetComponent<RigidBody>();
          // rigidB.velocity = Vector3.up * projectileSpeed;

          ProjectileHero proj = projGO.GetComponent<ProjectileHero>();
          proj.type = eWeaponType.blaster;
          float tSpeed = Main.GET_WEAPON_DEFINITION(proj.type).velocity;
          rigidB.velocity = Vector3.up * tSpeed;
        }
    */


    void OnTriggerEnter(Collider other) {
      Transform rootT = other.gameObject.transform.root;
      GameObject fo = rootT.gameObject;
      // Debug.Log("Shield trigger hit by: ") +go.gmaeObject.name;

      // Make sure it's not the same trigger fo as last time
      if (go == lastTriggerGo) return;
      lastTriggerGo = go;

      Enemy enemy = go.GetComponent<Enemy>();
      if (enemy != null) {    // If the shield was triggered by an enemy
        shieldLevel--;        // Decreases the level of the shield by 1
        Destroy(go);          // ... and Destroy the enemy
      }
      else {
        Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
      }
    }


    public float shieldLevel {
      get {return (_shieldLevel);}
      private set {
        _shieldLevel = Mathf.Min(value, 4);

        // If the shield is going to be set to less than zero
        if (value < 0) {
          Destroy(this.gameObject);    // Destroy the Hero
          Main.HERO_DIED();
        }
      }
    }
  
    /*
      void Start() {...}
    */
}
```

__2.__ Open the _Weapon_ C# script in VS add the following code

```cs
// Weapon.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  /*
    This is an enum of the various possible weapon types
    It also includes a "shield" type to allow a chield PowerUp
    Items marked [NI] below are Not Implemented
  */

  public enum eWeaponType {
    none,        // The default / no weapon
    blaster,    // A simple blaster
    spread,      // Multiple shots simultaneously
    phaser,      // [NI] Shots that move in waves
    missile,    // [NI] Homin missiles
    laser,      // [NI] Damage over time
    shield      // Raise shieldLevel
  }

  /*
    The WeaponDefinition class allow to set the properties
      of a specific weapon in the Inspector. The main class has
      an array of WeaponDefinitions that make this possible
  */
  [System.Serializable]
  public class WeaponDefinition {
    public eWeaponType    type = eWeaponType.none;

    [Tooltip("Letter to show on the PowerUp Cube")]
    public string    letter;

    [Tooltip("Color of PowerUp Cube")]
    public Color    powerUpColor = Color.white;

    [Tooltip("Prefab of Weapon model that is attached to the Player Ship")]
    public GameObject    weaponModelPrefab;

    [Tooltip("Prefab of the Projectile that is fired")]
    public GameObject    projectilePrefab;

    [Tooltip("Color of the Projectile that is fired")]
    public Color    projectileColor = Color.white;

    [Tooltip("Damage caused when a single Projectile hits an Enemy")]
    public float    damageOnHit = 0;

    [Tooltip("Damage caused per second by a Laser [Not Implemented]")]
    public float    damagePerSec = 0;

    [Tooltip("Seconds to delay between shots")]
    public float     delayBetweenShots = 0;

    [Tooltip("Velocity of individual Projectiles")]
    public float    velocity = 50;
  }

  public class Weapon : MonoBehaviour {
    static public Transform     PROJECTILE_ANCHOR;

    [Header("Dynamic")]
    [SerializeField]
    [Tooltip("Setting this manually while playing does not work properly")]
    private eWeaponType     _type = eWeaponType.none;
    public WeaponDefinition     def;
    public float        nextShotTime;       // Time the Weapon will fire next

    private GameObject      weaponModel;
    private Transform       shotPointTrans;


    void Start() {
        // Set up PROJECTILE_ANCHOR if it has not already been done
        if (PROJECTILE_ANCHOR == null) {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }

        shotPointTrans = transform.GetChild(0);

        // Call SetType() for the default _type set in the Inspector
        SetType(_type);

        // Find the fireEvent of a Hero Component in the parent hierarchy
        Hero hero = GetComponentInParent<Hero>();
        if (hero != null) {
            hero.fireEvent += Fire;
        }
    }


    public void SetType(eWeaponType wt) {
        _type = wt;
        if (type == eWeaponType.none) {
            this.gameObject.SetActive(false);
            return;
        }
        else {
            this.gameObject.SetActive(true);
        }

        // Get the WeaponDefinition for this type from Main
        def = Main.GET_WEAPON_DEFINITION(_type);

        // Destroy any old model and then attach a model for this weapon
        if (weaponModel != null) {
            Destroy(weaponModel);
        }
        weaponModel = Instantiate<GameObject>(def.weaponModelPrefab, transform);
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localScale = Vector3.one;

        nextShotTime = 0;       // Can fire immediately after _type is set
    }


    private void Fire() {
        // If this.gameObject is inactive, return
        if (!gameObject.activeInHierarchy) {
            return;
        }
        // If its hasn't been enough time between shots, return
        if (Time.time < nextShotTime) {
            return;
        }

        ProjectileHero      p;
        Vector3     vel = Vector3.up * def.velocity;

        switch(type) {
            case eWeaponType.blaster:
                p = MakeProjectile();
                p.vel = vel;
                break;

            case eWeaponType.spread:
                p = MakeProjectile();
                p.vel = vel;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                p.vel = p.transform.rotation * vel;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.vel = p.transform.rotation * vel;
                break;
        }
    }


    private ProjectileHero MakeProjectile() {
        GameObject go;
        go = Instantiate<GameObject>(def.projectilePrefab.PROJECTILE_ANCHOR);
        ProjectileHero p = go.GetComponent<ProjectileHero>();

        Vector3 pos = shotPointTrans.position;
        pos.z = 0;
        p.transform.position = pos;

        p.type = type;
        nextShotTime = Time.time + def.delayedBetweenShots;
        return(p);
    }
  }
```

__3.__ Save the _Weapon_ script in VS and return to Unity

__4.__ Select the _Weapon_ attached to __Hero > heroShip > hardpoint_0_ in the Hierarchy and set the __type__ of its _Weapon (Script)_ component to _Blaster_

__5.__ Click _Play_

__6.__ Stop Unity. Select the _Weapon_ in the Hierarchy again, and this time set the __type__ of its _Weapon (Script)_ component to _Spread_

__7.__ Click _Play_. Now a different weapon model should appear

__8.__ Stop Unity. Select the _Weapon_ in the Hierarchy one last time, and this time set the __type__ of its _Weapon (Scripts)_ component back to _Blaster_

__9.__ Save the Scene


## Revising the Enemy OnCollisionEnter Method
__1.__ Open the _Enemy_ C# script in VS and delete the __OnCollissionEnter()__ method

__2.__ Replace the old __OnCollissionEnter()__ method with code

```cs
// Enemy.cs
  
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  [RequireComponent(typeof(BoundsCheck))]
  
  public class Enemy : MonoBehaviour {
    [Header("Inscribed)]
    public float speed = 10f;        // The movement speed is 10m/s
    public float fireRate = 0.3;      // Seconds/shot (Unused)
    public float health = 10;        // Damage needed to destroy this enemy
    public int score = 100;          // Points earned for destroying this

    protected BoundsCheck bndCheck;    // Change bndCheck from private to protected

    void Awake() {
      bndCheck = GetComponent<BoundsCheck>();
    }
  
    // This is a Property: A method that acts like a field
    public Vector3 pos {
      get {
        return this.transform.position;
      }
      set {
        this.transform.position = value;
      }
    }


    void Update() {
      Move();

      // Check whether this Enemy has gone off the bottom of the screen
      if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown)) {
        Destroy(gameObject);
      }

      /*
        if (!bndCheck.isonScreen) {
          if (pos.y < bndCheck.camHeight - bndCheck.radius) {
            // We're off the the bottom, so destroy this GameObject
            Destroy(gameObject);
          }
        }
      */
    }
  
  
    public virtual void Move() {
      Vector3 tempPos = pos;
      tempPos.y -= speed * Time.deltaTime;
      pos = tempPos;
    }

    /*
        void OnCollisionEnter(Collision coll) {
          GameObject otherGO = coll.gameObject;
          if (otherGO.GetComponent<ProjectileHero>() != null) {
            Destroy(otherGO);      // Destroy the Projectile
            Destroy(gameObject);    // Destroy this Enemy GameObject
          }
          else {
            Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
          }
        }
    */
    void OnCollisionEnter(Collision coll) {
        GameObject otherGO = coll.gameObject;

        // Check the collisions with ProjectileHero
        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        if (p != null) {
            // Only damage this Enemy if it's on screen
            if (bndCheck.isOnScreen) {
                // Get the damage amount from the Main WEAP_DICT
                health -= Main.GET_WEAPON_DEFINITION(p.type).damageOnHit;

                if (health <= 0) {
                    // Destroy the Enemy
                    Destroy(this.gameObject);
                }
            }

            // Destroy the ProjectileHero regardless
            Destroy(otherGO);
        }
        else {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
  
  
    /*
      void Start() {...}
    */
  }
```

__3.__ Save the _Enemy_ script and return to Unity

__4.__ Select __MainCamera_ in the Hierarchy and set _Element_0_ of the __prefabEnemies__ array of the _Main (Script)_ component to the Enemy_0 prefab

__5.__ Save scene and click _Play_ in Unity


# Showing Enemy Damage
## Adding GetAllMaterials to the Utils Script
__1.__ Open the _Utils_ script in VS and enter code

```cs
// Utils.cs

  using System.Collections;
  using System.Collections.Generic;
  using UntiyEngine;

  public class Utils : MonoBehaviour {
    // == Bezier Curves ============================================

    /*
      While most Bezier curves are 3 and 4 points, it is possible to have any number of points using this recursive function

      <param name="u">The amount of interpolation [0...1]</param>
      <param name="points">An array of Vector3s to interpolate</param>
    */

    static public Vector3 Bezier(float u, params Vactor3[] points) {
      // Set up the array
      Vector3[,] vArr = new Vector3[points.Length, points.Length];

      // Fill the last roe of vArr with the elements of vList
      int r = points.Length - 1;
      for(int c = 0; c < points.Length; c++) {
        vArr[r, c] = vList[c];
      }

      // Iterate over all remaining rows and interpolate points at each one
      for (r--; r >= 0; r--) {
        for (int c = 0; c <= r; c++) {
          vArr[r, c] = Vector3.LerpUnclamped(vArr[r+1, c], vArr[r+1, c+1], u);
        }
      }

      // When complete, vArr[0, 0] holds the final interpolated value
      return vArr[0, 0];
    }


    // == Materials ====================================================

    /*
      Returns a list of all Materials on this GameObject and its children

      <param name="go">The GameObject o which to search for Renders</param>
    */

    static public Material[] GetAllMaterials(GameObject go) {
      Renderer[] rends = go.GetComponentsInChildren<Renderer>();

      Materials[] mats = new Material[rends.Length];
      for (int i = 0; i < rends.Length; i++) {
        mats[i] = rends[i].material;
      }

      return mats;
    }


    /*
      void Start() {...}
      void Update() {...}
    */
  }
```


## Using GetAllMeterials to Make the Enemy Blink Red
__1.__ Create a new C# script named _BlinkColorOnHit_ and place it in the __Scripts folder

__2.__ Open the _BlinkColorOnHit_ script in VS and enter the code

```cs
// BlinkColorOnHit.cs

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  [DisallowMultipleComponent]

  public class BlinkColorOnHit : MonoBehaviour {
    private static float blinkDuration = 0.1;    // # seconds to show damage
    private static Color blinkColor = Color.red;

    [Header("Dynamic")]
    public bool    showingColor = false;
    public float    blinkCompleteTime;    // Time to stop showing the color

    private Material[]    materials;    // All the Materials of this & its children
    private Color[]    originalColors;
    private BoundsCheck    bndCheck;


    void Awake() {
      bndCheck = GetComponentInParent<BoundsCheck>();

      // Get materials and colors for this GameObject and its children
      materials = Utils.GetAllaterials(gameObject);
      originalColors = new Color(materials.Length);
      for (int i = 0; i < materials.Length; i++) {
        originalColors[i] = materials[i].color;
      }
    }


    /*
      void Start() {...}
    */


    void Update() {
      if (showingColor && Time.time > blinkCompleteTime) {RevertColors();}
    }


    void OnCollisionEnter(Collision coll) {
      // Check for collisions with ProjecitleHero
      ProjectileHero p = coll.gameObject.GetComponent<ProjectileHero>();

      if (p != null) {
        if (bndCheck != null && !bndCheck.isOnScreen) {
          return;    // Don't show damage if this is off screen
        }

        SetColors();
      }
    }


    /*
      Sets the Albedo color (i.e., the main color) of all materials in the
        materials array to blinkColor, set showingColor to true, and sets the
        time that the colrs should be reverted
    */
    void SetColors() {
      foreach (Material m in materials) {
        m.color = blinkColor;
      }

      showingColor = true;
      blinkCompleteTime = Time.time + blinkDuration;
    }


    /*
      Reverts all material in the material array back to their original color
        and sets showingColor to false
    */
    void RevertColors() {
      for (int i = 0; i < materials.Length; i++) {
        materials[i].color = originalColors[i];
      }

      showingColor = false;
    }
  }
```

__3.__ _Save All_ scripts in VS and return to Unity

__4.__ Select __MainCamera_ in the Hierarchy and ensure that the Enemy_0 prefabs assigned to _Emenet_0_ of the __prefabEnemies__ array of the _Main (Script)_ component

__5.__ Click the name _Enemy_0_ in the _Element_0_ position of the __prefabEnemies__ array of the _Main (Script)_ component. This highlights the Enemy_0 prefab in the Project pane

__6.__ Select _Enemy_0_ in the _Prefabs folder of the Project pane

__7.__ Add the _BlinkColorOnHit_ script to the Enemy_0 prefab by choosing _Component > Scripts> Blink Color On Hit_ from the main Unity menu, and confirm that it was added in the Inspector

__8.__ Click _Play_ in Unity to test the game

__9.__ It's time to apply the __BlinkColorOnHit__ script to all Enemy_# prefabs:
> __a.__ Select _Enemy_0_ in the _Prefabs folder of the Project pane
>
> __b.__ Hold Shift and click _Enemy_4_ in the _Prefabs folder. This selects all five Enemy_# prefabs
>
> __c.__ From the main Unity menu, choose _Component > Scripts > Blink Color On Hit_

__10.__ Save the project


# Adding PowerUps and Boosting Weapons
* __blaster [B]:__ If the player weapon type is not blaster, this switches to blaster and resets the ship to have only a single gun. If the player weapon type is already blaster, it increases the number of guns
* __spread [S]:__ If the player weapon type is not spread, this switched to spread and resets the ship to have only a single gun. If the player weapon type is already spread, it increases the number of guns
* __shield [O]:__ This increases the player's shieldLevel by 1


## Artword for the PowerUp GameObject
__1.__ Create a new 3D Text (_GameObject > 3D Object > 3D Text_ from the menu bar; it's at the very bottom of the 3D Object options)

__2.__ Rename the _NewText_ GameObject to _PowerUp_ and give it the following settings:
* P:[10, 0, 0]
* R:[0, 0, 0]
* S:[1, 1, 1]

__3.__ Set the _TextMesh_ component properties of PowerUp:
* P:[10, 0, 0]
* __Text:__ M
* __Offset Z:__ -2
* __Character Size:__ 0.5
* __Anchor:__ Middle center
* __Alignment:__ Center
* __Font Size:__ 38
* __Font Style:__ Bold

__4.__ Add a _Rigidbody_ component to _PowerUp_ (_Component > Physics > Rigidbody_) and set it to
* __Angular Drag:__ 0
* __Use Gravity:__ :white_large_square:
* __Freeze Position Z:__ :heavy_check_mark:

__5.__ Set both _Physics Layer_ of PowerUp to _PowerUp_

__6.__ Create a new _Cube_ that is a child of _PowerUp_
> __a.__ Rename the _Cube_ to _PowerCube_

> __b.__ Assign _PowerCube_ the following settings:
> * P:[0, 0, 0]
> * R:[0, 0, 0]
> * S:[2, 2, 2]

__7.__ Srt the _Physics Layer_ of _PowerCube_ to _PowerUp_

__8.__ Create a custom material for the PowerUp cube, as follows:
> __a.__ Create a new Material named _Mat_PowerUp_ inside the _Materials folder
>
> __b.__ Drag the _Mat_PowerUp_ Material onto _PowerCube_ (the child of PowerUp)
>
> __c.__ Select _PowerCube_
>
> __d.__ Set the _Shader_ of _Mat_PowerUp_ to _ProtoTools > UnlitAlpha_
>
> __e.__ Click the _Select_ button at the bottom right of the texture box for Mat_PowerUp and choose the texture named _PowerUp_ from the Assets tab
>
> __f.__ Set the _Main Color_ of Mat_PowerUp to cyan (RGBA:[0, 255, 255, 255])

__9.__ Save the scene


## PowerUp Code
__1.__ Select the _PowerUp_ GameObject in the Hierarchy
> __a.__ Attach a _BoundsCheck (Script)_ component to the _PowerUp_ GameObject
>
> __b.__ Set the BoundsCheck __boundsType__ = _Inset_, __radius__ = _1_, and __keepOnScreen__ = _true_. These values will keep the PowerUp on screen as long as it exists

__2.__ Create a new C# script named _PowerUp_ in the ___Scripts_ folder

__3.__ Attached the _PowerUp_ script to the _PowerUp_ GameObject in the Hierarchy

__4.__ Open the _PowerUp_ script in VS and enter the code

```cs
// PowerUp.cs

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(BoundsCheck))]

    public class PowerUp : MonoBehaviour {
        [Header("Inscribed")]
        // This is an unusual but handy use of Vector2s
        [Tooltip("x holds a min value and y a max value for a Random.Range() call")]
        public Vector2      rotMinMax = new Vector2(15, 90);
        [Tooltip("x holds a min value and y a max value for a Random.Range() call")]
        public Vector2      driftMinMax = new Vector2(.25f, 2);
        public float        lifeTime = 10;      // PoerUp will exist for # seconds
        public float        fadeTime = 4;       // Then it fades over # seconds

        [Header("Dynamic")]
        public eWeaponType      type;               // The type of the PowerUp
        public GameObject       cube;               // Reference to the PowerCube child
        public TextMesh         letter;             // Reference to the TextMesh
        public Vector3          rotPerSecond;       // Euler rotation speed for PowerCube
        public float            birthTime;          // The Time.time this was instantiated

        private Rigidbody       rigid;
        private BoundsCheck     bndCheck;
        private Material        cubeMat;


        void Awake() {
            // Find the Cube reference (there's only a single child)
            cube = transform.GetChild(0).gameObject;

            // Find the TextMesh and other components
            letter = GetComponent<TextMesh>();
            rigid = GetComponent<RigidBody>();
            bndCheck = GetComponent<BoundsCheck>();
            cubeMat = cube.GetComponent<Renderer>().material;

            // Set a random velocity
            Vector3 vel = Random.onUnitSphere;       // Get Random XYZ velocity
            vel.z = 0;      // Flatten the vel to the XY plane
            vel.Normalize();        // Normalizing a Vecotr set its length to 1m

            vel *= Random.Range(driftMinMax.x, driftMinMax.y);
            rigid.velocity = vel;
        }
    }
```
