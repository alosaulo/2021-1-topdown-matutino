using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public abstract class Character : MonoBehaviour
{

    public GameManager gameManager;

    protected Animator myAnimator;
    protected Rigidbody2D myBody;

    protected bool isDead = false;

    [Header("Attributes")]
    public float speed;
    public float atkDamage;
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void RecieveDamage(float damage) {
        myAnimator.SetTrigger("Hurt");
        if (currentHealth > 1)
            currentHealth = currentHealth - damage;
        else {
            currentHealth = 0;
            myAnimator.SetTrigger("Death");
        }
    }

    public void GainHealth(float gain) {
        if (currentHealth <= maxHealth) {
            currentHealth += gain;
            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
        }
    }

    public bool IsFullHealth() {
        return currentHealth < maxHealth;
    }

    public void DoDamage() { 
        
    }

    public virtual void DestroyGameObject() {
        Destroy(gameObject);
    }


}
