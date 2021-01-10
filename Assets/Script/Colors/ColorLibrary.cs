using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ColorLibrary : MonoBehaviour
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

    public Color32 AverageColorFromTextureHSV(Texture2D tex)
    {
        Color[] texColors = tex.GetPixels();
        float resolution = 15;
        int[] classifier = new int[(int)resolution];

        float h = 0;
        float s = 0;
        float v = 0;

        for (int i = 0; i < texColors.Length; i++)
        {
            Color.RGBToHSV(texColors[i], out h, out s, out v);

            for (int j = 0; j < classifier.Length; j++)
            {
                if (h < j * (1 / classifier.Length) && h > (j + 1) * (1 / classifier.Length))
                    classifier[j]++;
            }
        }

        int maxValue = classifier.Max();
        float maxIndex = classifier.ToList().IndexOf(maxValue);

        return Color.HSVToRGB(maxIndex / resolution, 1, 1);
    }

    public float DifferenceValueRGB(Color32 textureColor, Color32 colorToCompare)
    {
        return Mathf.Sqrt((textureColor.r - colorToCompare.r) * (textureColor.r - colorToCompare.r) +
            (textureColor.g - colorToCompare.g) * (textureColor.g - colorToCompare.g) +
            (textureColor.b - colorToCompare.b) * (textureColor.b - colorToCompare.b));
    }

    public float DifferenceValueHSV(Color32 textureColor, Color32 colorToCompare)
    {
        float hueToCompare, h, s, v;

        Color.RGBToHSV(textureColor, out h, out s, out v);
        Color.RGBToHSV(colorToCompare, out hueToCompare, out s, out v);

        return Mathf.Sqrt((h - hueToCompare) * (h - hueToCompare));
    }
}


