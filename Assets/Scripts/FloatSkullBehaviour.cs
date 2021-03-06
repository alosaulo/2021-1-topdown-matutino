using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatSkullBehaviour : EnemyBehavior
{
    [Header("Explosion Settings")]
    public float explosionRadius;
    [Header("Audioclips")]
    public AudioClip explosionClip;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            float distance = GetDistance();
            if (distance > 1)
                FollowPlayerNoAnimator();
            else
                DoExplosion();
        }
    }

    public void DoExplosion()
    {
        if (isReadyToAtack)
        {
            isReadyToAtack = false;
            doAtk = true;
            lastTargetPosition = target.position;
            Vector2 dir = lastTargetPosition - transform.position;
            int layer = 1 << 10;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, explosionRadius, dir, explosionRadius,layer);
            if (hit) {
                if (hit.collider.tag == "Player")
                {
                    PlayerKnightController player = hit.transform.GetComponent<PlayerKnightController>();
                    player.RecieveDamage(atkDamage);
                }
            }
            myAnimator.SetTrigger("Explosion");
            audioSource.PlayOneShot(explosionClip);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
