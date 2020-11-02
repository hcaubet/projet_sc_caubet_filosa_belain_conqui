using System;
using UnityEngine;

public class FindNearestColoredImage : MonoBehaviour
{
    public Texture2D compareThisImage;
    public Texture2D[] closestToImage;

    private ColorArray toCompare;
    public Texture2D[] compareToThisImages;

    public void Compare(Texture2D _compareThisImage)
    {
        compareThisImage = _compareThisImage;

        closestToImage = new Texture2D[compareToThisImages.Length];
        float[] difference = new float[compareToThisImages.Length];


        toCompare = (ColorArray)ScriptableObject.CreateInstance<ColorArray>();
        toCompare.image = compareThisImage;
        toCompare.resolution = 10;
        toCompare.SetArray();

        int index = 0;

        foreach(Texture2D t in compareToThisImages)
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

        GameObject.FindObjectOfType<DynamicPhotoLayout>().sprites = closestToImage;
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
