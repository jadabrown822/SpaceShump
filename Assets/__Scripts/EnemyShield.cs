using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BlinkColorOnHit))]

public class EnemyShield : MonoBehaviour
{
    [Header("INscribed")]
    public float health = 10;

    private List<EnemyShield> protectors = new List<EnemyShield>();
    private BlinkColorOnHit blinker;


    // Start is called before the first frame update
    void Start()
    {
        blinker = GetComponent<BlinkColorOnHit>();
        blinker.ignoreOnCollisionEnter = true;      // This will not yet compile

        if (transform.parent == null) { return; }
        EnemyShield shieldParent = transform.parent.GetComponent<EnemyShield>();
        if (shieldParent != null {
            shieldParent.AddProtector(this);
        }
    }


    /*
        Called by another Enemyshield to join the protectors of this EnemyShield
    <param nme="shieldChild">The EnmeyShield that will protect this</param>
    */
    public void AddProtector(EnemyShield shieldChild)
    {
        protectors.Add(shieldChild);
    }


    /*
        Shortcut for gameObject.activeInHierarchy and gameObject.SetActive() 
    */
    public bool isActive
    {
        get { return gameObject.activeInHierarchy; }
        private set { gameObject.SetActive(value); }
    }


    /*
        Called by Enemy_4.OnCollisionEnter() & parent's EnemyShield.TakeDamage()
            to distribute damage to EnemyShield protectors
        <param name="dmg"> The amount of damage to be handled</param>
        <returns>Any damage not handled by this shield</returns>
    */
    public float TakeDamage(float dmg)
    {
        // Can we pass damage to a protector EnemyShield?
        foreach (EnemyShield es in protectors)
        {
            if (es.isActive)
            {
                dmg = es.TakeDamage(dmg);
                // If all damage was handled, return 0 damage
                if (dmg == 0) { return 0; }
            }
        }

        /*
            If teh code gets here, then the EnemyShield will blink and take Damage
            Make the blinker blink
        */
        blinker.SetColors();        // This will appear uunderlined in red for now

        health -= dmg;
        if (health <= 0)
        {
            // Deactivate this EnemyShield GameObject
            isActive = false;

            // Return any damage that was not absorbed by this EnemyShield
            return -health;
        }

        return 0;
    }

    /*
        // Update is called once per frame
        void Update()
        {
        
        }
    */
}
