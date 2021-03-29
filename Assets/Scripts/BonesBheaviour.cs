using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesBheaviour : MonoBehaviour
{

    public float valX;
    public float valY;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeXY();
    }

    void ChangeXY() {
        animator.SetFloat("X", valX);
        animator.SetFloat("Y", valY);
    }

}
