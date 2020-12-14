using System;
using UnityEngine;

public class FindNearestColoredImage : MonoBehaviour
{
    public ImageToDisplay allImages;

    public void Compare(Texture2D _compareThisImage)
    {
        Texture2D compareThisImage = _compareThisImage;

        Texture2D[] closestToImage = new Texture2D[allImages.image.Length];
        float[] difference = new float[allImages.image.Length];


        ColorArray toCompare = (ColorArray)ScriptableObject.CreateInstance<ColorArray>();
        toCompare.image = compareThisImage;
        toCompare.resolution = 10;
        toCompare.SetArray();

        int index = 0;

        foreach(Texture2D t in allImages.image)
        {
            ColorArray newColorArray = (ColorArray)ScriptableObject.CreateInstance<ColorArray>();
            newColorArray.image = t;
            newColorArray.resolution = 10;
            newColorArray.SetArray();

            closestToImage[index] = t;
            difference[index] = colorDifference(toCompare, newColorArray);
            index++;
        }

        Array.Sort(difference, closestToImage);

        allImages.image = closestToImage;
    }

    private float colorDifference(ColorArray a, ColorArray b)
    {
        Color32[] colorGridA = a.colorArray;
        Color32[] colorGridB = b.colorArray;
        float r = 0;

        for (int i = 0; i < colorGridA.Length; i++)
        {
            if (colorGridA[i].r != colorGridA[i].g || colorGridA[i].b != colorGridA[i].g-1)
            {
                r += Mathf.Sqrt((colorGridA[i].r - colorGridB[i].r) * (colorGridA[i].r - colorGridB[i].r) + (colorGridA[i].g - colorGridB[i].g)
                    * (colorGridA[i].g - colorGridB[i].g) + (colorGridA[i].b - colorGridB[i].b) * (colorGridA[i].b - colorGridB[i].b));
            }
        }

        return r;
    }
}
