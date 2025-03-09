using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMoves : MonoBehaviour
{
    public Transform player;
    public float mod = 2f;
    public float mirror = -1f; // -1 for mirror, 1 for not

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector2(player.position.x * mirror / mod, player.position.y * mirror / mod);
        }
    }
}
