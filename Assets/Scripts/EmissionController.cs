using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionController : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.MainModule mainModule;

    public float transparency = 0.1f;
    public float transparencyMult = 0.01f;

    public float emissionRate = 25;
    public float emissionMult = 4;

    public Color color1 = Color.white;
    public Color color2 = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        if (ps != null)
        {
            emissionModule = ps.emission;
            mainModule = ps.main;

            SetEmissionRate(emissionRate);
            SetRandomColor(transparency);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEmissionRate(float newRate)
    {
        emissionModule.rateOverTime = newRate * emissionMult;
    }

    public void SetRandomColor(float alpha)
    {
        alpha = Mathf.Clamp01(alpha * transparencyMult);

        color1.a = alpha;
        color2.a = alpha;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
    }
}
