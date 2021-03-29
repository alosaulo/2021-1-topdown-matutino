using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Character character;

    public float speed;

    public float dashSpeed;
    
    public float dashDelayAnimation;
    public float dashCooldown;
    
    float dashCount;
    float dashCooldownCount;

    Rigidbody2D meuRigidBody;

    Animator meuAnimator;


    bool dashing;
    float lastH, lastV;

    // Start is called before the first frame update
    void Start()
    {
        meuAnimator = GetComponent<Animator>();
        meuRigidBody = GetComponent<Rigidbody2D>();
        dashCooldownCount = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
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
            meuAnimator.SetTrigger("Atk");
        }

        if (Input.GetButton("Fire2") && 
            dashing == false && 
            dashCooldownCount >= dashCooldown) 
        {
            meuAnimator.SetBool("Dash", true);
            gameObject.layer = 12;
            dashing = true;
            dashCooldownCount = 0;
        }

        CountDash();

    }

    void Move(float hAxis, float vAxis) {
        meuRigidBody.velocity = speed * new Vector2(hAxis, vAxis).normalized * Time.deltaTime;

        if (Mathf.Abs(hAxis) >= 0.1)
        {
            meuAnimator.SetFloat("X", hAxis);
            meuAnimator.SetFloat("Y", 0);
        }
        if (Mathf.Abs(vAxis) >= 0.1)
        {
            meuAnimator.SetFloat("Y", vAxis);
            meuAnimator.SetFloat("X", 0);
        }
    }

    void DoDash() {
        if (lastH == 0 && lastV == 0)
        {
            lastH = meuAnimator.GetFloat("X");
            lastV = meuAnimator.GetFloat("Y");
        }
        Vector2 dir = new Vector2(lastH, lastV).normalized;
        meuRigidBody.velocity = ((speed * dashSpeed) * dir) * Time.deltaTime;
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {

    }

    public void SetAtack() {
        meuAnimator.SetBool("Atk", false);
    }

    public void DesactiveDash() {
        dashing = false;
        meuAnimator.SetBool("Dash", false);
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
