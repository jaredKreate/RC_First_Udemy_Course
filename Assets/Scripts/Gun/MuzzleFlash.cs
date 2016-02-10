using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {

    Material mat;
    Color matColor;
    public float matFadeSmooth = 16;

    Light light;
    float lightIntensity;
    public float intensityFadeSmooth = 20;
    public float maxIntensity = 5;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        matColor = mat.color;
        matColor.a = 0;
        mat.color = matColor;

        light = GetComponent<Light>();
        lightIntensity = light.intensity;
        lightIntensity = 0;
        light.intensity = lightIntensity;
    }

    void Update()
    {
        if (matColor.a > 0)
        {
            matColor.a -= matFadeSmooth * Time.deltaTime;
            mat.color = matColor;
        }
        if (lightIntensity > 0)
        {
            lightIntensity -= intensityFadeSmooth * Time.deltaTime;
            light.intensity = lightIntensity;
        }
    }

    public void Flash()
    {
        matColor.a = 1;
        lightIntensity = maxIntensity;
    }
}
