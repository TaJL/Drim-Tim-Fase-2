using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimatedImage : MonoBehaviour
{
    public AnimationCurve curve;
    // Start is called before the first frame update
    private RectTransform rect_transform;
    void Start()
    {
        rect_transform = GetComponent<RectTransform>();
    }

    private float accumulator = 0;
    // Update is called once per frame
    void Update()
    {
        accumulator += Time.deltaTime/2;
        if(accumulator > 1)
            accumulator = 0;
        
        rect_transform.pivot = new Vector2(0.5f,curve.Evaluate(accumulator));
    }
}
