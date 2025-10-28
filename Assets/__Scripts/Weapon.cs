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
