using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is an enum of the various possible weapon types
    It also includes a "shield" type to allow a shield PowerUp
    Items marked [NI] below are Not Implemented  
*/
public enum eWeaponType {
    none,        // The default / no weapon
    blaster,    // A simple blaster
    spread,      // Multiple shots simultaneously
    phaser,      // [NI] Shots that move in waves
    missile,    // [NI] Homing missiles
    laser,      // [NI] Damage over time
    shield      // Raise shieldLevel
}

/* The WeaponDefinition class allows to set the properties
      of a specific weapon in the Inspector. The Main class has
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
    public float     delayBetweenShots = 0; // Corrected typo: delayedBetweenShots -> delayBetweenShots

    [Tooltip("Velocity of individual Projectiles")]
    public float    velocity = 50;
}

public class Weapon : MonoBehaviour {
    // Static Transform used to hold all ProjectileGameObjects in the Hierarchy
    static public Transform PROJECTILE_ANCHOR;

    [Header("Dynamic")]
    [SerializeField]
    [Tooltip("Setting this manually while playing does not work properly")]
    private eWeaponType     _type = eWeaponType.none;
    public WeaponDefinition def;
    public float        nextShotTime;       // Time the Weapon will fire next

    private GameObject      weaponModel;
    private Transform       shotPointTrans;

    // Public property to access the private _type field and call SetType on assignment
    public eWeaponType type {
        get { return _type; }
        set { SetType(value); } 
    }


    void Start() {
        // Set up PROJECTILE_ANCHOR if it has not already been done
        if (PROJECTILE_ANCHOR == null) {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }

        // ShotPoint is assumed to be the first child (index 0) of the Weapon GameObject
        shotPointTrans = transform.GetChild(0);

        // Call SetType() for the default _type set in the Inspector
        SetType(_type);

        // Find the Hero component in the parent hierarchy
        Hero hero = GetComponentInParent<Hero>();
        if (hero != null) {
            // Subscribe the Fire method to the Hero's fireEvent delegate
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
        
        if (def.weaponModelPrefab != null) {
            weaponModel = Instantiate<GameObject>(def.weaponModelPrefab, transform);
            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localScale = Vector3.one;
        }

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

        ProjectileHero p;
        Vector3 vel = Vector3.up * def.velocity;

        // Set the time for the next shot based on the delay
        nextShotTime = Time.time + def.delayBetweenShots;

        switch(type) {
            case eWeaponType.blaster:
                p = MakeProjectile();
                p.vel = vel;
                break;

            case eWeaponType.spread:
                // Middle shot
                p = MakeProjectile();
                p.vel = vel;
                
                // Left shot
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                p.vel = p.transform.rotation * vel;
                
                // Right shot
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.vel = p.transform.rotation * vel;
                break;
        }
    }


    private ProjectileHero MakeProjectile() {
        GameObject go;
        // CRITICAL FIX: The Instantiate call was incorrect. We need to instantiate the projectilePrefab
        // and set the PROJECTILE_ANCHOR as its parent.
        go = Instantiate<GameObject>(def.projectilePrefab, PROJECTILE_ANCHOR);
        
        ProjectileHero p = go.GetComponent<ProjectileHero>();

        Vector3 pos = shotPointTrans.position;
        pos.z = 0;
        p.transform.position = pos;

        // Set the projectile type, which also handles coloring and setting damage
        p.type = type; 
        
        return(p);
    }
}
