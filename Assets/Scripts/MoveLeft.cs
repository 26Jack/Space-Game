using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10;
    public bool canMove = false;

    public float timer = 0;
    public float waitTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            canMove = true;
        }

        if (canMove)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
