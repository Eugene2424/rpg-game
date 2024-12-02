using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Vector3 _scale;
    [HideInInspector] public float value, maxValue;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        _scale = transform.GetChild(0).transform.localScale; 
        if (!float.IsNaN(value / maxValue))
            transform.GetChild(0).transform.localScale = new Vector3(_scale.x, value / maxValue, _scale.z);
    }
}
