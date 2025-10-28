using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S { get; private set; }      // Singleton property

    [Header("Inscribed")]
    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;

    [Header("Dynamic")]     [Range(0, 4)]       [SerializeField]
    private float _shieldLevel = 1;
    // public float shieldLevel = 1;
    [Tooltip("This filed holds a reference to the last triggering GameObject")]
    private GameObject lastTriggerGo = null;
    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate event name fireEvent
    public event WeaponFireDelegate fireEvent;


    void Awake()
    {
        if (S == null)
        {
            S = this;       // Set the Singleton only if it's null
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempt to assign second Hero.S!");
        }

        // fireEvent += TempFire;

        // Reset the weapons to start _Hero with 1 blaster
        ClearWeapons();
        weapons[0].SetType(eWeaponType.blaster);
    }


    // Update is called once per frame
    void Update()
    {
        // Pull in information from the Inpu class
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Change transform.position based on the axis
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);

        /*
            // Allow the ship to fire
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TempFire();
            }
        */

        // Use the fireEvent to fire Weapons when the Spacebar is pressed
        if (Input.GetAxis("Jump") == 1 && fireEvent != null)
        {
            fireEvent();
        }
    }


    /*
        void TempFire()
        {
            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = transform.position;
            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
            // rigidB.velocity = Vector3.up * projectileSpeed;

            ProjectileHero proj = projGO.GetComponent<ProjectileHero>();
            proj.type = eWeaponType.blaster;
            float tSpeed = Main.GET_WEAPON_DEFINITION(proj.type).velocity;
            rigidB.velocity = Vector3.up * tSpeed;
        }
    */


    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        // Debug.Log("Shield trigger hit by: " + go.gameObject.name);

        // Make sure it's not the same trigger go as last time
        if (go == lastTriggerGo) { return; }
        lastTriggerGo = go;

        Enemy enemy = go.GetComponent<Enemy>();
        PowerUp pUp = go.GetComponent<PowerUp>();
        if (enemy != null)
        {                       // If the shield was triggered by an enemy
            shieldLevel--;      // Decreases the level of the shield by 1
            Destroy(go);        // ... and Destroy the enemy
        }
        else if (pUp != null)
        {                               // If the shield hits a PowerUp
            AbsorbPowerUp(pUp);         // .. absorb the PowerUp
        }
        else
        {
            Debug.LogWarning("Shield trigger hit by non-enemy: " + go.name);
        }
    }


    public void AbsorbPowerUp(PowerUp pUp)
    {
        Debug.Log("Absorb PowerUp: " + pUp.type);

        switch (pUp.type)
        {
            case eWeaponType.shield:
                shieldLevel++;
                break;

            default:
                if (pUp.type == weapons[0].type)        // If it is the same type
                {
                    Weapon weap = GetEmptyWeaponSlot();
                    if (weap != null)
                    {
                        // Set ot to pUp.type
                        weap.SetType(pUp.type);
                    }
                }
                else
                {       // If this is a different weapon type
                    ClearWeapons();
                    weapons[0].SetType(pUp.type);
                }

                break;
        }

        pUp.AbsorbedBy(this.gameObject);
    }


    public float shieldLevel
    {
        get { return (_shieldLevel); }
        private set
        {
            _shieldLevel = Mathf.Min(value, 4);

            // If the shiled is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);       // Destroy the Hero
                Main.HERO_DIED();
            }
        }
    }


    /*
        Finds the first empty Weapon slot (i.e., type=none) and returns it
        <returns>The first empty Weapon slot or null if not are empty</return>
    */
    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == eWeaponType.none)
            {
                return (weapons[i]);
            }
        }

        return (null);
    }


    // Sets the type of all Weapon slots to none
    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(eWeaponType.none);
        }
    }


    /*
        void Start() {...} 
    */
}
