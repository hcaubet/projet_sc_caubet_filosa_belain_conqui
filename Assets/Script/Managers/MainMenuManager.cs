﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager: MonoBehaviour
{
    public ImageToDisplay allImages;

    private void Start()
    {
        allImages.colorOfImage = new Color32[allImages.image.Length];
        GetAllImagesAverageColor();
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
        SceneManager.LoadScene("2.1 - Chose color", LoadSceneMode.Single);
    }

    public void GoToPaintColor()
    {
        SceneManager.LoadScene("2.2 - Paint color", LoadSceneMode.Single);
    }
}
