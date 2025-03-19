using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBreakEffect : MonoBehaviour
{

    public float lifetime = 0.2f;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
