using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileController : MonoBehaviour
{
    public float speed;
    Rigidbody2D myBody;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.velocity = transform.right * speed;
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
