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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float dist = Vector2.Distance(target.position, transform.position);
        if(doAtk == false) { 
            if (AtkDistance < dist)
            {
                FollowPlayerNB();
            }
            else if (AtkDistance >= dist) {
                doAtk = true;
                collider2D.isTrigger = true;
                SpiderVision.SetActive(false);
                myAnimator.SetBool("Atk",true);
                Vector2 trajectory = target.position - transform.position;
                myBody.AddForce(trajectory * AtkSpeed, ForceMode2D.Impulse);
            }
        }
    }



}
