using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrderInLayer : MonoBehaviour
{
    private Renderer render;
    private int baseOrder;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        baseOrder = render.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        render.sortingOrder = baseOrder - (Mathf.RoundToInt(transform.position.y));
    }
}
