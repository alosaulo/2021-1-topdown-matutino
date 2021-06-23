using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnightController : Character
{
    [Header("Audio Clips")]
    public AudioClip hitClip;
    public AudioClip atkClip;

    [Header("Dash Attributes")]
    public float dashSpeed;
    public float dashDelayAnimation;
    public float dashCooldown;

    SpriteRenderer sprite;

    [Header("Atk Collider")]
    public GameObject AtkRightCollider;
    public GameObject AtkLeftCollider;

    float dashCount;
    float dashCooldownCount;

    bool dashing;
    float lastH, lastV;
    float hAxis, vAxis;

    bool isAtk = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sprite = GetComponent<SpriteRenderer>();
        dashCooldownCount = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            
            hAxis = Input.GetAxis("Horizontal");
            vAxis = Input.GetAxis("Vertical");

            if (dashing == false)
            {
                lastH = hAxis;
                lastV = vAxis;
            }

            if (Input.GetButton("Fire1"))
            {
                myAnimator.SetTrigger("Atk");
                if (sprite.flipX == false)
                    AtkRightCollider.SetActive(true);
                else
                    AtkLeftCollider.SetActive(true);
            }

            /*if (Input.GetButton("Fire2") &&
                dashing == false &&
                dashCooldownCount >= dashCooldown)
            {
                myAnimator.SetBool("Dash", true);
                gameObject.layer = 12;
                dashing = true;
                dashCooldownCount = 0;
            }

            CountDash();*/
        }
    }

    void FixedUpdate() {
        if (dashing == false)
        {
            Move(hAxis, vAxis);
        }
        else if (dashing == true)
        {
            DoDash();
        }
    }

    void Move(float hAxis, float vAxis)
    {
        myBody.velocity = speed * new Vector2(hAxis, vAxis).normalized * Time.fixedDeltaTime;

        if (hAxis > 0)
        {
            sprite.flipX = false;
            myAnimator.SetBool("Walk", true);
        }
        else if (hAxis < 0)
        {
            sprite.flipX = true;
            myAnimator.SetBool("Walk", true);
        }

        if (Mathf.Abs(vAxis) >= 0.1f) {
            myAnimator.SetBool("Walk", true);
        }

        if (Mathf.Abs(hAxis) <= 0.1f && Mathf.Abs(vAxis) <= 0.1f) {
            myAnimator.SetBool("Walk", false);
        }


    }

    void DoDash()
    {
        if (lastH == 0 && lastV == 0)
        {
            lastH = myAnimator.GetFloat("X");
            lastV = myAnimator.GetFloat("Y");
        }
        Vector2 dir = new Vector2(lastH, lastV).normalized;
        myBody.velocity = ((speed * dashSpeed) * dir) * Time.deltaTime;
    }

    private void LateUpdate()
    {

    }

    public void SetAtack()
    {
        AtkRightCollider.SetActive(false);
        AtkLeftCollider.SetActive(false);
        myAnimator.SetBool("Atk", false);
    }

    public void DesactiveDash()
    {
        dashing = false;
        myAnimator.SetBool("Dash", false);
        gameObject.layer = 10;
    }

    void CountDash()
    {
        if (dashing == false)
        {
            dashCooldownCount += Time.deltaTime;
        }

        if (dashing == true)
        {
            dashCount = dashCount + Time.deltaTime;
            if (dashCount >= dashDelayAnimation)
            {
                DesactiveDash();
                dashCount = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HealthPotion" && currentHealth < maxHealth)
        {
            PotionController potion = collision.GetComponent<PotionController>();
            GainHealth(potion.HealthPoints);
            Destroy(potion.gameObject);
        }
    }

    public override void RecieveDamage(float damage)
    {
        base.RecieveDamage(damage);
        audioSource.PlayOneShot(hitClip);
    }

    public void PlayAtkSound()
    {
        audioSource.PlayOneShot(atkClip);
    }
}