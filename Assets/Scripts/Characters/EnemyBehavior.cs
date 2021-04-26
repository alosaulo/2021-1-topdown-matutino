using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : Character
{

    protected Transform target;
    protected Vector3 lastTargetPosition;

    [Header("NPC Attack Attributes")]
    public GameObject prefabRangedAttack;
    public bool doAtk;
    public float atkDelay;
    public float projectileSpeed;
    protected float atkCount;

    protected virtual void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    protected void CountAtk()
    {
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

    protected void NormalizePosition(float x, float y)
    {
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

    protected void FollowPlayer()
    {
        Vector2 dir = target.position - transform.position;
        myAnimator.SetFloat("X", dir.x);
        myAnimator.SetFloat("Y", dir.y);
        myBody.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime));
    }

    protected void FollowPlayerNB() {
        myAnimator.SetBool("Walk", true);
        myBody.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime));
    }

    protected void AttackMelee()
    {
        if (doAtk == false)
        {
            float x = myAnimator.GetFloat("X");
            float y = myAnimator.GetFloat("Y");
            NormalizePosition(x, y);
            lastTargetPosition = target.position;
            myAnimator.SetTrigger("Atk");
            doAtk = true;
        }
    }

    protected void AttackRanged() {
        if (doAtk == false) {
            lastTargetPosition = target.position;
            GameObject GO = Instantiate(prefabRangedAttack,transform.position,Quaternion.identity);
            GO.GetComponent<BoneAttackController>().AimTarget(lastTargetPosition, projectileSpeed,atkDamage);
            myAnimator.SetTrigger("Atk");
            doAtk = true;
        }
    }

    protected void FacePlayer() {
        Vector2 dir = target.position - transform.position;
        float clampX = Mathf.Clamp(dir.x, -0.1f, 0.1f);
        float clampY = Mathf.Clamp(dir.y, -0.1f, 0.1f); ;
        myAnimator.SetFloat("X", clampX);
        myAnimator.SetFloat("Y", clampY);
    }

}
