using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHealthUI : MonoBehaviour
{
    public GameObject fakeHealthPrefab;
    public int count = 3;
    public int spacing = 16;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newHealth = Instantiate(fakeHealthPrefab, transform);
            newHealth.transform.localPosition = new Vector3(0, i * spacing, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
