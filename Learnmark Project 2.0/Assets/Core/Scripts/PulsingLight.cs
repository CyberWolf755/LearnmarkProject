using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingLight : MonoBehaviour {
    
    Light light;
    float lightIntencity;

    Animator animator;
    Renderer renderer;

    [SerializeField] private float alpha;
    Color currentColor;
    Color offColor;
    Color greenColor = new Color(0, 1, 0, 1);
    Color redColor = new Color(1, 0, 0, 1);


    // Use this for initialization
    void Start ()
    {

        light = GetComponent<Light>();
        light.enabled = false;

        lightIntencity = light.intensity;

        animator = GetComponent<Animator>();

        renderer = GetComponent<Renderer>();
        offColor = renderer.material.color;
        
    }


	// Update is called once per frame
	void Update ()
    {
        if(light.enabled)
        {
            light.intensity = lightIntencity * alpha;
            renderer.material.color = Color.Lerp(offColor, currentColor, alpha);
        }
        
    }


    public void Off()
    {
        light.enabled = false;
        animator.SetTrigger("Off");

        Debug.Log("Lights: OFF");
        
    }

    public void GreenOn()
    {
        currentColor = greenColor;
        light.color = greenColor;
        light.enabled = true;
        
        animator.SetTrigger("Green");


    }

    public void RedOn()
    {
        currentColor = redColor;
        light.color = redColor;
        light.enabled = true;
        
        animator.SetTrigger("Red");


    }
}
