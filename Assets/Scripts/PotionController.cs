using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    SpriteRenderer sRenderer;

    [SerializeField]
    float destroyTime;
    public float HealthPoints;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("DestroyObject");
    }

    IEnumerator DestroyObject() {
        for (float i = 1f; i >= 0; i -= 0.1f)
        {
            sRenderer.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(.5f);
        }
        Destroy(gameObject);
    }

}
