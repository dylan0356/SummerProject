using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static bool start = false;
    public AnimationCurve curve;
    public float duration = 0.5f;

    [SerializeField] private Transform target;
    
    void Update()
    {
        if (start) {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking() {
        // Vector3 originalPos = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration) {
            Vector3 originalPos = transform.position;
            elapsed += Time.deltaTime;
            float strength = curve.Evaluate(elapsed / duration);
            transform.position = originalPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = target.position;
        // transform.position = originalPos;
    }
}
