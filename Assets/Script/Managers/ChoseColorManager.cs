using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoseColorManager : MonoBehaviour
{
    public ImageToDisplay allImages;
    public Color32 globalColorToCompare;

    public PinColor colorDisk;

    public void StartCOmparing()
    {
        CompareTexturesToColor();
    }

    public void CompareTexturesToColor()
    {
        // get chosen color to to compare
        globalColorToCompare = colorDisk.GetPointedColor();

        // methods library
        ColorLibrary library = new ColorLibrary();

        // array of difference and array to sort images 
        float[] differenceIntensity = new float[allImages.image.Length];
        float[] newSortedArray = new float[allImages.image.Length]; ;

        // compare all images according to RBG method
        for (int i = 0; i < allImages.image.Length; i++)
        {
            differenceIntensity[i] = library.DifferenceValueRGB(allImages.colorOfImage[i], globalColorToCompare);
            newSortedArray[i] = library.DifferenceValueRGB(allImages.colorOfImage[i], globalColorToCompare);
        }

        /*
        // compare all images according to HSV method
        for (int i = 0; i < allImages.image.Length; i++)
        {
            differenceIntensity[i] = library.DifferenceValueHSV(allImages.colorOfImage[i], globalColorToCompare);
            newSortedArray[i] = library.DifferenceValueHSV(allImages.colorOfImage[i], globalColorToCompare);
        }*/

        // sort images 
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
