using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D), typeof(NavMeshAgent))]
public abstract class EnemyBehavior : Character
{
    protected Transform target;
    protected Vector3 lastTargetPosition;
    protected PlayerController player;
    protected NavMeshAgent agent;

    public Image healthImage;
    [Header("Score Points")]
    public int scorePoints;

    [Header("NPC Attack Attributes")]
    public GameObject prefabRangedAttack;
    public bool doAtk;
    public bool isReadyToAtack;
    public float atkDelay;
    public float projectileSpeed;
    protected float atkCount;

    [Header("Drops")]
    public float dropChance;
    public GameObject dropPotionPrefab;

    protected virtual void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        target = player.transform;
        isReadyToAtack = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
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
                isReadyToAtack = true;
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

    protected void FollowPlayerNoAnimator() {
        //Vector2 dir = target.position - transform.position;
        agent.destination = target.position;
    }

    protected void FollowPlayer()
    {
        //Vector2 dir = target.position - transform.position;
        float xPos = agent.velocity.x;
        float yPos = agent.velocity.y;

        myAnimator.SetFloat("X", xPos * 100);
        myAnimator.SetFloat("Y", yPos * 100);
        //myBody.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime));
        agent.destination = target.position;
    }

    protected void FollowPlayerNB() {
        myAnimator.SetBool("Walk", true);
        //myBody.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime));
        agent.destination = target.position;
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

    protected void DropPotion() {
        if (dropPotionPrefab != null && player.IsFullHealth()) {
            float chance = Random.Range(0, 100);
            if (chance >= 50) { 
                Instantiate(dropPotionPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public override void DestroyGameObject()
    {
        DropPotion();
        base.DestroyGameObject();
    }

    public override void RecieveDamage(float damage)
    {
        myBody.velocity = Vector2.zero;
        base.RecieveDamage(damage);
        if (isDead) { 
            StopAgent();
            GameManager._instance.UpdateScore(scorePoints);
        }
        if (healthImage != null)
        {
            healthImage.fillAmount = currentHealth/maxHealth;
        }
    }

    public void DoAttackMelee()
    {
        if (isReadyToAtack) {
            isReadyToAtack = false;
            doAtk = true;
            lastTargetPosition = target.position;
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

    protected float GetDistance() {
        return Vector2.Distance(transform.position, target.position);
    }

    protected void StopAgent() {
        if(isDead == false) { 
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
    }

    protected void StartAgent()
    {
        if(isDead == false) { 
            agent.isStopped = false;
        }
    }

}
