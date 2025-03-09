using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public GameObject healthPrefab;
    public int spacing = 16;
    private List<GameObject> healthSprites = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth (int healthCount)
    {
        while (healthSprites.Count < healthCount)
        {
            AddHealthSprite();
        }

        while (healthSprites.Count > healthCount)
        {
            RemoveHealthSprite();
        }
    }

    private void AddHealthSprite()
    {
        GameObject newAmmo = Instantiate(healthPrefab, transform);
        newAmmo.transform.localPosition = new Vector3(0, healthSprites.Count * spacing, 0);
        healthSprites.Add(newAmmo);
    }

    private void RemoveHealthSprite()
    {
        if (healthSprites.Count > 0)
        {
            GameObject topAmmo = healthSprites[healthSprites.Count - 1];
            healthSprites.RemoveAt(healthSprites.Count - 1);
            Destroy(topAmmo);
        }
    }
}
