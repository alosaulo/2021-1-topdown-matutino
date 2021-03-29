using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoracaoController : MonoBehaviour
{

    public Image coracaoFill;

    // Start is called before the first frame update
    void Start()
    {
        if (coracaoFill == null) {
            coracaoFill = transform.GetChild(0).GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
