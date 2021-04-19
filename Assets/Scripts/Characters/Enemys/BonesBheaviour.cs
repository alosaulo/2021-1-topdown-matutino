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
        FacePlayer();
        AttackRanged();
    }

}
