using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]

public class ProjectileHero : MonoBehaviour
{

    public Transform target;        // Missile
    public float homingStrength = 10f;

    private float waveFrequency = 5f;       // Oscillations per second
    private float waveAmplitude = 0.5f;      // Side-to-side distance
    private Vector3 startPos;
    private float birthTime;

    private BoundsCheck bndCheck;
    private Renderer rend;

    [Header("Dynamic")]
    public Rigidbody rigid;
    [SerializeField]
    private eWeaponType _type;

    // Thispublic property masks the private field _type
    public eWeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }


    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp))
        {
            Destroy(gameObject);
        }
    }


    /*
        Set the _type private field and colors thos projectile to match the WeaponDefinition
    
        <param name="eType">The eWeaponType to use</param>
    */
    public void SetType(eWeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION(_type);
        Color c = def.projectileColor;
        c.a = 1f;

        rend.material.color = c;
    }


    // Allows Weapon to easily set the velocity of this ProjectHero
    public Vector3 vel
    {
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }


    private void FixedUpdate()
    {
        if (type == eWeaponType.missile && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 desiredVelocity = direction * vel.magnitude;
            rigid.velocity = Vector3.Lerp(rigid.velocity, desiredVelocity, Time.fixedDeltaTime * homingStrength);
        }
        else if (type == eWeaponType.phaser)
        {
            float age = Time.time - birthTime;
            Vector3 offset = Vector3.right * Mathf.Sin(age * waveFrequency * Mathf.PI * 2) * waveAmplitude;
            Vector3 forward = Vector3.up * vel.magnitude * Time.fixedDeltaTime;
            rigid.MovePosition(startPos + forward + offset);
            startPos += forward;        // Advance the base position
        }

    }


    private void Start()
    {
        startPos = transform.position;
        birthTime = Time.time;
    }
}
