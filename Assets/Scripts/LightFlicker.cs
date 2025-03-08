using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public Light2D flickeringLight;  // Assign your light in the Inspector
    public float minIntensity = 0.5f;  // Minimum light intensity
    public float maxIntensity = 2f;  // Maximum light intensity
    public float flickerSpeed = 0.1f; // Speed of flickering

    private float targetIntensity;

    void Start()
    {
        if (flickeringLight == null)
        {
            flickeringLight = GetComponent<Light2D>();
        }
        targetIntensity = flickeringLight.intensity;
        StartCoroutine(Flicker());
    }

    System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            flickeringLight.intensity = Mathf.Lerp(flickeringLight.intensity, targetIntensity, flickerSpeed);
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f)); // Randomize flicker timing
        }
    }
}
