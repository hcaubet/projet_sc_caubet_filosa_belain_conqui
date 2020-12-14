using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoseColorManager : MonoBehaviour
{
    public ImageToDisplay allImages;
    private Color32 globalColorToCompare;

    public PinColor colorDisk;

    private void Start()
    {
        CompareTexturesToColor();
    }

    public void CompareTexturesToColor()
    {
        globalColorToCompare = colorDisk.GetPointedColor();

        ColorLibrary library = new ColorLibrary();
        float[] differenceIntensity = new float[allImages.image.Length];

        float[] newSortedArray = new float[allImages.image.Length]; ;


        for (int i = 0; i < allImages.image.Length; i++)
        {
            differenceIntensity[i] = library.DifferenceValue(allImages.colorOfImage[i], globalColorToCompare);
            newSortedArray[i] = library.DifferenceValue(allImages.colorOfImage[i], globalColorToCompare);
        }

        Texture2D[] sortedImages = allImages.image;


        Array.Sort(newSortedArray);

        for (int i = 0; i < newSortedArray.Length; i++)
        {
            for (int j = 0; j < newSortedArray.Length; j++)
            {
                if (differenceIntensity[j] == newSortedArray[i])
                {
                    sortedImages[i] = allImages.image[j];
                }
            } 
        }

        allImages.image = sortedImages;
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("1 - Main menu", LoadSceneMode.Single);
    }

    public void GoToPhotoDisplayer()
    {
        SceneManager.LoadScene("3 - Display photo", LoadSceneMode.Single);
    }
}
