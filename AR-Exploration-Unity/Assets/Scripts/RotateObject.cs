using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    //Assign in the inspector
    public GameObject prefabToRotate;
    public Slider slider;

    //Preserve the original and current orientation
    private float previousValue;

    // Start is called before the first frame update
    void Awake()
    {
        //Assign a callback for when this slider changes
        this.slider.onValueChanged.AddListener(this.OnSliderChanged);

        //And current value
        this.previousValue = this.slider.value;

    }

    // Update is called once per frame
    void OnSliderChanged(float value)
    {

        //How much we've changed
        float delta = value - this.previousValue;
        this.prefabToRotate.transform.Rotate(Vector3.up * delta * 360);

        //Set our previous value for the next change
        this.previousValue = value;
    }
}
