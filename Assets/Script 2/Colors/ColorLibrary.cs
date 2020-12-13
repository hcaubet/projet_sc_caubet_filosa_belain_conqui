using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ColorLibrary: MonoBehaviour
{ 
    public Color32 AverageColorFromTexture(Texture2D tex)
    {
        Color32[] texColors = tex.GetPixels32();

        int total = texColors.Length;

        float r = 0;
        float g = 0;
        float b = 0;

        for (int i = 0; i < total; i++)
        {
            r += texColors[i].r;
            g += texColors[i].g;
            b += texColors[i].b;
        }

        return new Color32((byte)(r / total), (byte)(g / total), (byte)(b / total), 0);
    }

    public float DifferenceValue(Color32 textureColor, Color32 colorToCompare)
    {
        return Mathf.Sqrt((textureColor.r - colorToCompare.r) * (textureColor.r - colorToCompare.r) +
            (textureColor.g - colorToCompare.g) * (textureColor.g - colorToCompare.g) +
            (textureColor.b - colorToCompare.b) * (textureColor.b - colorToCompare.b));
    }
}


