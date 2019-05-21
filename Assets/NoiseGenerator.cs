using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    public Material matToChange;
    public Texture noise;

    private void Update()
    {
        matToChange.SetTexture("DisplacementMap", noise);
    }
}
