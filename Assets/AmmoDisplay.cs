using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    public GameObject ammoPrefab;
    public int spacing = 16;
    private List<GameObject> ammoSprites = new List<GameObject>();

    public void UpdateAmmo(int ammoCount)
    {
        while (ammoSprites.Count < ammoCount)
        {
            AddAmmoSprite();
        }

        while (ammoSprites.Count > ammoCount)
        {
            RemoveAmmoSprite();
        }
    }

    private void AddAmmoSprite()
    {
        GameObject newAmmo = Instantiate(ammoPrefab, transform);
        newAmmo.transform.localPosition = new Vector3(0, ammoSprites.Count * spacing, 0);
        ammoSprites.Add(newAmmo);
    }

    private void RemoveAmmoSprite()
    {
        if (ammoSprites.Count > 0)
        {
            GameObject topAmmo = ammoSprites[ammoSprites.Count - 1];
            ammoSprites.RemoveAt(ammoSprites.Count - 1);
            Destroy(topAmmo);
        }
    }
}