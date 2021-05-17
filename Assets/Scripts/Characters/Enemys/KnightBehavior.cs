using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehavior : EnemyBehavior
{

    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        CountAtk();
    }

    private void LateUpdate()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            target = player.transform;
            Debug.DrawRay(transform.position,
                player.transform.position - transform.position ,
                Color.magenta);
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance >= 1 && doAtk == false) {
                FollowPlayer();
            }
            else {
                AttackMelee();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            float x = myAnimator.GetFloat("X");
            float y = myAnimator.GetFloat("Y");
            NormalizePosition(x, y);
        }
    }

    public void DoAttack()
    {
        Vector2 dir = lastTargetPosition - transform.position;
        Debug.DrawRay(transform.position, dir, Color.red, 2f);
        int layer = 1 << 10;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1, layer);
        if (hit)
        {
            if (hit.collider.tag == "Player")
            {
                PlayerController player = hit.transform.GetComponent<PlayerController>();
                player.RecieveDamage(atkDamage);
            }
        }
    }

}
