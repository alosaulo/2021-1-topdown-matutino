using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviour : EnemyBehavior
{
    Collider2D collider2D;

    [Header("Spider Attack")]
    public GameObject SpiderVision;
    public float AtkDistance;
    public float AtkSpeed;

    bool targetOnZone;
    bool isWalking;
    bool isAttacking;
    bool isDamaged;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CountAtk();
        if (doAtk == false && collider2D.isTrigger == true) {
            collider2D.isTrigger = false;
            SpiderVision.SetActive(true);
        }
        AI();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            targetOnZone = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doAtk == true && collision.tag == "Player") {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.RecieveDamage(atkDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            targetOnZone = false;
    }

    public void TackleTarget() {
        Vector2 trajectory = target.position - transform.position;
        myBody.AddForce(trajectory * AtkSpeed, ForceMode2D.Impulse);
    }

    public void AI()
    {
        if (targetOnZone)
        {
            float dist = Vector2.Distance(target.position, transform.position);
            if (doAtk == false)
            {
                if (AtkDistance < dist)
                {
                    FollowPlayerNB();
                    isWalking = true;
                }
                else if (AtkDistance >= dist)
                {
                    isWalking = false;
                    doAtk = true;
                    collider2D.isTrigger = true;
                    SpiderVision.SetActive(false);
                }
            }
            else
            {
                isWalking = false;
                doAtk = false;
            }
        }
        ChangeAnimation();
    }

    void ChangeAnimation() {
        myAnimator.SetBool("Walk", isWalking);
        myAnimator.SetBool("Atk", doAtk);
    }

}
