using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;

    void Awake() 
    {
        singleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
 

    public void PlayerDamage(float damage)
    {

        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            Dead();
        }

    }

    void Dead() 
    {
        currentHealth = 0;
        isDead = true;
        Debug.Log("The player is now dead");
    }

}
