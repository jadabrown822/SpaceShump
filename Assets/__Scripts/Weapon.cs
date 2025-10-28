using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This is an enum of the various possible weapon types.
    It also includes a "shield" type to allow a shield PowerUp
    Items marked [NI] below are not Implented
*/
public enum eWeaponType
{
    none,           // The default / no weapon
    blaster,        // A simple Blaster
    spread,         // Multiple shots simultaneously
    phaser,         // [NI] Shots move in waves
    missile,        // [NI] Homing missiles
    laser,          // [NI] Damage over time
    shield          // Raise shieldLevel
}

/*
    The WeaponDefinition class allow to set the properties
        of a specific weapon in the Inspector. The Main class has
        an array of WeaponDefinitions that make this possible
*/
[System.Serializable]
public class WeaponDefinition
{
    public eWeaponType type = eWeaponType.none;

    [Tooltip("Letter to show on the PowerUp Cube")]
    public string letter;

    [Tooltip("Color of PowerUp Cube")]
    public Color powerUpColor = Color.white;

    [Tooltip("Prefab of Weapon model that is attached to the Player Ship")]
    public GameObject weaponModelPrefab;

    [Tooltip("Prefab of projectile that is fired")]
    public GameObject projectilePrefab;

    [Tooltip("Color of the projectile that is fired")]
    public Color projectileColor = Color.white;

    [Tooltip("Damage caused when a sinlge Projectile hits an Enemy")]
    public float damageOnHit = 0;

    [Tooltip("Damage caused per second by the Laser [Not Implemented]")]
    public float damagePerSec = 0;

    [Tooltip("Seconds to delay bewteen shots")]
    public float delayBetweenShots = 0;

    [Tooltip("Velocity of individual Projectiles")]
    public float velocity = 50;
}

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
