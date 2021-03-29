using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{

    public GameManager gameManager;

    public float maxHealth;
    public float currentHealth;
    public float atkForce;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveDamage(float damage) {
        if (currentHealth >= 0)
            currentHealth = currentHealth - damage;
    }

    public void GainHealth(float gain) {
        if (currentHealth <= maxHealth) {
            currentHealth += gain;
            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
        }
    }

    public void DoDamage() { 
        
    }


}
