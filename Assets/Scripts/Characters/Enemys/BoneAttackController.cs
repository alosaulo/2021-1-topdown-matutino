using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneAttackController : MonoBehaviour
{
    Rigidbody2D myBody;

    private float atkDamage;

    bool colide = false;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myBody.transform.Rotate(Vector3.forward,3f);
    }

    public void AimTarget(Vector2 posAim, float speed, float damage) {
        myBody.velocity = (posAim - (Vector2)transform.position).normalized * speed * Time.deltaTime ;
        atkDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && colide == false) {
            colide = true;
            collision.GetComponent<Character>().RecieveDamage(atkDamage);
            Destroy(gameObject);
        }
    }

}
