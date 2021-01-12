using UnityEngine;
using System.Linq;

public class ColorLibrary : MonoBehaviour
{
    public float DifferenceValueLAB(Color32 textureColor, Color32 colorToCompare)
    {
        float[] xyz1 = rgb2xyz(textureColor);
        float[] xyz2 = rgb2xyz(colorToCompare);
        float[] lab1 = xyz2lab(xyz1);
        float[] lab2 = xyz2lab(xyz2);

        return Mathf.Sqrt(
            Mathf.Pow(lab1[0] - lab2[0],2) +
            Mathf.Pow(lab1[1] - lab2[1], 2) +
            Mathf.Pow(lab1[2] - lab2[2], 2)
            );
    }

    private float[] rgb2xyz(Color32 c)
    {
        float r = (c.r / 255f);
        float g = (c.g / 255f);
        float b = (c.b / 255f);

        if (r > 0.04045f) 
            r = Mathf.Pow(((r + 0.055f) / 1.055f),2.4f);
        else r = r / 12.92f;

        if (g > 0.04045) 
            g = Mathf.Pow(((g + 0.055f) / 1.055f), 2.4f);
        else g = g / 12.92f;
      
        if (b > 0.04045) 
            b = Mathf.Pow(((b + 0.055f) / 1.055f), 2.4f);
        else b = b / 12.92f;

        r *= 100;
        g *= 100;
        b *= 100;

        float X = r * 0.4124f + g * 0.3576f + b * 0.1805f;
        float Y = r * 0.2126f + g * 0.7152f + b * 0.0722f;
        float Z = r * 0.0193f + g * 0.1192f + b * 0.9505f;

        return new float[] { X, Y, Z };
    }

    private float[] xyz2lab(float[] _xyz)
    {
        float r = _xyz[0] / 94.811f;
        float g = _xyz[1] / 100.0f;
        float b = _xyz[2] / 107.304f;

        if (r > 0.008856f) r = Mathf.Pow(r, (1.0f / 3.0f));
        else r = (7.787f * r) + (16.0f / 116.0f);

        if (g > 0.008856f) g = Mathf.Pow(g, (1.0f / 3.0f));
        else g = (7.787f * g) + (16.0f / 116.0f);

        if (b > 0.008856f) b = Mathf.Pow(b, (1.0f / 3.0f));
        else b = (7.787f * b) + (16.0f / 116.0f);

        float labX = (116 * g) - 16;
        float labY = 500 * (r - g);
        float labZ = 200 * (g - b);

        return new float[] { labX, labY, labZ };
    }
}


