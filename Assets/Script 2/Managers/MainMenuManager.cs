using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager: MonoBehaviour
{
    public ImageToDisplay allImages;

    private void Start()
    {
        GetAllImagesAverageColor();
        allImages.colorOfImage = new Color32[100];
    }

    private void GetAllImagesAverageColor()
    {
        ColorLibrary library = new ColorLibrary();

        for (int i=0; i< allImages.image.Length; i++)
        {
            allImages.colorOfImage[i] = library.AverageColorFromTexture(allImages.image[i]);
        }
    }

    public void GoToChoseColor()
    {

    }

    public void GoToPaintColor()
    {

    }
}
