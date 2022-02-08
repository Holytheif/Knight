using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickerLights : MonoBehaviour
{
    public Light2D light2D;
    // Start is called before the first frame update
    public bool isFlickering = false;
    public float timeDelay;
    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFlickering == false)
        {
            StartCoroutine(FlickeringLights());
        }
    }

    IEnumerator FlickeringLights()
    {
        isFlickering = true;
        light2D.pointLightOuterRadius = Random.Range(3.5f, 4f); 
        timeDelay = Random.Range(0.08f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
