using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesBheaviour : EnemyBehavior
{

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        CountAtk();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            float distance = GetDistance();
            if(distance > 7) {
                StartAgent();
                agent.SetDestination(target.position);
            }
            else {
                StopAgent();
                AttackRanged();
            }
        }
    }

}
