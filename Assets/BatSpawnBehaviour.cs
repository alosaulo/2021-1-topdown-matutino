using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawnBehaviour : EnemyBehavior
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
        float distance = GetDistance();
        if (distance > 1)
            FollowPlayerNoAnimator();
        else
            DoAttackMelee();
    }
}
