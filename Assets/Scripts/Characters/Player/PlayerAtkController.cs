using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkController : MonoBehaviour
{
    public PlayerKnightController player;

    private void Start()
    {
        //player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            float damage = player.atkDamage;
            Character enemy = collision.gameObject.GetComponent<Character>();
            enemy.RecieveDamage(damage);
        }
    }
}
