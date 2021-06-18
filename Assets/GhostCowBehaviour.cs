using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCowBehaviour : EnemyBehavior
{
    [Header ("Sprite Renderer")]
    public SpriteRenderer sprite;

    bool isSeeingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        CountAtk();
        if (isSeeingPlayer == true) {
            Vector2 dir = target.position - transform.position;

            if (dir.x > 0) {
                sprite.flipX = false;
            }
            else {
                sprite.flipX = true;
            }

            agent.SetDestination(target.position);
            agent.isStopped = true;
            
            float remainingDistance = agent.remainingDistance;
            Debug.Log(remainingDistance);

            if (remainingDistance >= Mathf.Infinity)
            {
                myAnimator.SetBool("Atk", false);
                agent.isStopped = true;
            }
            else if (remainingDistance > 4 && remainingDistance <= 10) {
                agent.isStopped = false;
            }
            else if (remainingDistance > 0 && remainingDistance <= 4) {
                myAnimator.SetBool("Atk", true);
                myBody.velocity = dir;
            }

        }
        if (isSeeingPlayer == false) {
            agent.SetDestination(transform.position);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            isSeeingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            isSeeingPlayer = false;
        }
    }


    void DoAttack() { 
        
    }

}
