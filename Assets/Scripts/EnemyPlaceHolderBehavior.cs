using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaceHolderBehavior : MonoBehaviour
{
    PlayerController target;
    Vector3 lastTargetPosition;

    public float speed;
    Animator myAnimator;
    Rigidbody2D myBody;

    ContactFilter2D filter2D;
    RaycastHit2D hits;

    public bool doAtk;
    public float atkDelay;
    public float atkDamage;
    float atkCount;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CountAtk();
    }

    private void LateUpdate()
    {
        AI();
    }

    void DoDamage() {
        myAnimator.SetTrigger("Damage");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning(collision.gameObject);
        if (collision.tag == "Attack") {
            DoDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            target = player;
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

    private void FollowPlayer() {
        Vector2 dir = target.transform.position - transform.position;
        myAnimator.SetFloat("X",dir.x);
        myAnimator.SetFloat("Y", dir.y);
        myBody.MovePosition(Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime));
    }

    private void AttackMelee() {
        if (doAtk == false)
        {
            float x = myAnimator.GetFloat("X");
            float y = myAnimator.GetFloat("Y");
            NormalizePosition(x, y);
            lastTargetPosition = target.transform.position;
            myAnimator.SetTrigger("Atk");
            doAtk = true;
        }
    }

    public void DoAttack() {
        Vector2 dir = lastTargetPosition - transform.position;
        Debug.DrawRay(transform.position, dir, Color.red, 2f);
        int layer = 1 << 10;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1, layer);
        if (hit)
        {
            if (hit.collider.tag == "Player")
            {
                PlayerController player = hit.transform.GetComponent<PlayerController>();
                player.character.RecieveDamage(atkDamage);
            }
            Debug.Log("Bati no player");
        }
        
    }

    private void CountAtk() {
        if (doAtk == true)
        {
            atkCount = atkCount + Time.deltaTime;
            if (atkCount >= atkDelay)
            {
                doAtk = false;
                atkCount = 0;
            }
        }
    }

    void NormalizePosition(float x, float y) {
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
                myAnimator.SetFloat("X", 1);
            else
                myAnimator.SetFloat("X", -1);
            myAnimator.SetFloat("Y", 0);
        }
        else
        {
            if (y > 0)
                myAnimator.SetFloat("Y", 1);
            else
                myAnimator.SetFloat("Y", -1);
            myAnimator.SetFloat("X", 0);
        }
    }

    private void AI() {
        
    }

    private void OnDrawGizmos()
    {
        
    }

}
