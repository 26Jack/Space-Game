using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{
    public GameObject visualPrefab;
    public GameObject solidPrefab;
    public int length;

    // Start is called before the first frame update
    void Start()
    {
        CreateBorder();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void CreateBorder()
    {
        for (int i = -length / 2; i <= length / 2; i++)
        {
            // Top edge
            Instantiate(visualPrefab, new Vector3(i * 16, (length / 2) * 16, 0), Quaternion.identity);
            // Instantiate(solidPrefab, new Vector3(i * 16, (length / 2) * 16, 0), Quaternion.identity);

            // Bottom edge
            Instantiate(visualPrefab, new Vector3(i * 16, -(length / 2) * 16, 0), Quaternion.identity);
            // Instantiate(solidPrefab, new Vector3(i * 16, -(length / 2) * 16, 0), Quaternion.identity);

            // Left edge (Rotated 90 degrees clockwise)
            Instantiate(visualPrefab, new Vector3(-(length / 2) * 16, i * 16, 0), Quaternion.Euler(0, 0, -90));
            // Instantiate(solidPrefab, new Vector3(-(length / 2) * 16, i * 16, 0), Quaternion.Euler(0, 0, -90));

            // Right edge (Rotated 90 degrees clockwise)
            Instantiate(visualPrefab, new Vector3((length / 2) * 16, i * 16, 0), Quaternion.Euler(0, 0, -90));
            // Instantiate(solidPrefab, new Vector3((length / 2) * 16, i * 16, 0), Quaternion.Euler(0, 0, -90));
        }
    }
}
