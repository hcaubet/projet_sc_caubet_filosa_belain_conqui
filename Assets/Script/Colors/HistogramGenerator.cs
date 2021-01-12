using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HistogramGenerator : MonoBehaviour
{
    public int resolution = 5;
    public Color32[] colorGrid;

    public Color32[] colorsOfTexture;
    public int[] frequencyOfColors;

    private void Start()
    {
        CreateColorGrid();
    }

    public Color32[] CreateHistogram(Texture2D texture)
    {
        Color32[] texColors = texture.GetPixels32();
        colorsOfTexture = new Color32[colorGrid.Length];
        frequencyOfColors = new int[colorsOfTexture.Length];

        float h, s, v;

        int n = 0;
        for (int i = 0; i < texColors.Length; i += 100)
        {
            Color.RGBToHSV(texColors[i], out h, out s, out v);
            if (s > 0.2f && v > 0.2f)
            {
                Color32 nearestColor = AttributeNearestColor(texColors[i]);
                if (!Array.Exists(colorsOfTexture, element => element == (Color)nearestColor))
                {
                    colorsOfTexture[n] = nearestColor;
                    n++;
                }
                else
                {
                    frequencyOfColors[Array.IndexOf(colorsOfTexture, nearestColor)]++;
                }
            }
        }

        Color32[] newColorArray = new Color32[n];
        int[] newFrequencyArray = new int[n];

        for (int i = 0; i < n; i++)
        {
            newColorArray[i] = colorsOfTexture[i];
            newFrequencyArray[i] = frequencyOfColors[i];
        }

        colorsOfTexture = newColorArray;
        frequencyOfColors = newFrequencyArray;

        Array.Sort(frequencyOfColors, colorsOfTexture);
        Array.Reverse(frequencyOfColors);
        Array.Reverse(colorsOfTexture);

        return colorsOfTexture;
    }

    public int[] GetFrequencies(Texture2D texture)
    {
        CreateHistogram(texture);
        return frequencyOfColors;
    }

    private void CreateColorGrid()
    {
        colorGrid = new Color32[resolution * resolution * resolution];

        int n = 0;

        for (int i = 0; i < resolution; i += 1)
        {
            for (int j = 0; j < resolution; j += 1)
            {
                for (int k = 0; k < resolution; k += 1)
                {
                    Color32 colorToAdd = new Color32((byte)(i * 255 / resolution), (byte)(j * 255 / resolution), (byte)(k * 255 / resolution), 1);
                    colorGrid[n] = colorToAdd;
                    n++;
                }
            }
        }
    }

    private Color32 AttributeNearestColor(Color32 colorToAttribute)
    {
        float minDifference = Mathf.Infinity;
        Color32 nearestColor = new Color32();

        for (int i=0; i< colorGrid.Length; i++)
        {
            float diff = Mathf.Abs(colorToAttribute.r - colorGrid[i].r) + Mathf.Abs(colorToAttribute.g - colorGrid[i].g) + Mathf.Abs(colorToAttribute.b - colorGrid[i].b);
            if (minDifference > diff)
            {
                minDifference = diff;
                nearestColor = colorGrid[i];
            }
        }

        return nearestColor;
    }
}
