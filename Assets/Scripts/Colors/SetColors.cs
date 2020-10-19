using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SetColors : MonoBehaviour
{
    public Texture2D[] images;
    private ColorImage[] categories;

    public PanelManager panelManager;

    public void AttributeColor()
    {
        categories = GetComponent<GetColors>().colorScriptables;

        foreach(ColorImage c in categories)
        {
            List<Texture2D> imageList = new List<Texture2D>();

            foreach (Texture2D t in images)
            {
                if (FindCategorie(AverageColorFromTexture(t),categories) == Array.IndexOf(categories, c))
                {
                    imageList.Add(t);
                }
            }

            c.image = new Texture2D[imageList.Count];
            c.image = imageList.ToArray();
        }

        panelManager.CreatePanels();
    }

    Color32 AverageColorFromTexture(Texture2D tex)
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

    int FindCategorie(Color32 color, ColorImage[] categorie)
    {
        float ressemblance = 1000;
        int i = 0;

        foreach(ColorImage colorImage in categorie)
        {
            Color32 c = colorImage.color;

            float r = Mathf.Sqrt((color.r - c.r) * (color.r - c.r) + (color.g - c.g) * (color.g - c.g) + (color.b - c.b) * (color.b - c.b));

            if (r < ressemblance) 
            { 
                ressemblance = r;
                i = Array.IndexOf(categorie, colorImage);
            }
        }

        return i;
    }
}


