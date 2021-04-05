using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{

    [Header("Dash Attributes")]
    public float dashSpeed;
    public float dashDelayAnimation;
    public float dashCooldown;
    
    float dashCount;
    float dashCooldownCount;

    bool dashing;
    float lastH, lastV;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dashCooldownCount = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false) { 
            float hAxis, vAxis;
             hAxis = Input.GetAxis("Horizontal");
             vAxis = Input.GetAxis("Vertical");

            if (dashing == false) {
                Move(hAxis, vAxis);
                lastH = hAxis;
                lastV = vAxis;
            } else if (dashing == true) {
                DoDash();
            }

            if (Input.GetButton("Fire1"))
            {
                myAnimator.SetTrigger("Atk");
            }

            if (Input.GetButton("Fire2") && 
                dashing == false && 
                dashCooldownCount >= dashCooldown) 
            {
                myAnimator.SetBool("Dash", true);
                gameObject.layer = 12;
                dashing = true;
                dashCooldownCount = 0;
            }

            CountDash();
        }
    }

    void Move(float hAxis, float vAxis) {
        myBody.velocity = speed * new Vector2(hAxis, vAxis).normalized * Time.deltaTime;

        if (Mathf.Abs(hAxis) >= 0.1)
        {
            myAnimator.SetFloat("X", hAxis);
            myAnimator.SetFloat("Y", 0);
        }
        if (Mathf.Abs(vAxis) >= 0.1)
        {
            myAnimator.SetFloat("Y", vAxis);
            myAnimator.SetFloat("X", 0);
        }
    }

    void DoDash() {
        if (lastH == 0 && lastV == 0)
        {
            lastH = myAnimator.GetFloat("X");
            lastV = myAnimator.GetFloat("Y");
        }
        Vector2 dir = new Vector2(lastH, lastV).normalized;
        myBody.velocity = ((speed * dashSpeed) * dir) * Time.deltaTime;
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {

    }

    public void SetAtack() {
        myAnimator.SetBool("Atk", false);
    }

    public void DesactiveDash() {
        dashing = false;
        myAnimator.SetBool("Dash", false);
        gameObject.layer = 10;
    }

    void CountDash() {
        if (dashing == false) {
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

    }

}
