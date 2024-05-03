using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Color : MonoBehaviour
{
    private Color color;
    private SpriteRenderer self;
    // Start is called before the first frame update
    private void Start()
    {
        self = GetComponent<SpriteRenderer>();

        color = new Color(RandFloat(200),RandFloat(200), RandFloat(200), 1);

        self.color = color;
    }

    float RandFloat(float max)
    {
        float _output;

        _output = Random.Range(20, max);

        return _output;
    }
}
