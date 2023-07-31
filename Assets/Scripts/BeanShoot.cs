using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color += new Color(0, 0, 0, 255);
    }
}
