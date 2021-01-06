using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ColorLibrary: MonoBehaviour
{ 
    public Color32 AverageColorFromTextureRGB(Texture2D tex)
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

    public float DifferenceValueRGB(Color32 textureColor, Color32 colorToCompare)
    {
        return Mathf.Sqrt((textureColor.r - colorToCompare.r) * (textureColor.r - colorToCompare.r) +
            (textureColor.g - colorToCompare.g) * (textureColor.g - colorToCompare.g) +
            (textureColor.b - colorToCompare.b) * (textureColor.b - colorToCompare.b));
    }

    public float[] AverageColorFromTextureHSV(Texture2D tex)
    {
        Color[] texColors = tex.GetPixels();

        float total = 0;

        float h = 0;
        float s = 0;
        float v = 0;

        for (int i=0; i<texColors.Length;i++)
        {
            Color.RGBToHSV(texColors[i], out h, out s, out v);
            total += h;
        }

        return new float[] { h, s, v };
    }

    public float DifferenceValueHSV(Color32 textureColor, Color32 colorToCompare)
    {
        float hueToCompare, h, s, v;

        Color.RGBToHSV(textureColor, out h, out s, out v);
        Color.RGBToHSV(colorToCompare, out hueToCompare, out s, out v);

        return Mathf.Min(Mathf.Abs(hueToCompare - h), 360 - Mathf.Abs(hueToCompare - h));

    }
}


