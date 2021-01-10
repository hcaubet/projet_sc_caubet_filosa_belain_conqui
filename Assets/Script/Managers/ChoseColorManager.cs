using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoseColorManager : MonoBehaviour
{
    public ImageToDisplay allImages;
    public Color32 globalColorToCompare;

    public PinColor colorDisk;

    public void StartComparing()
    {
        CompareTexturesToColor();
    }

    public void CompareTexturesToColor()
    {
        // get chosen color to to compare
        globalColorToCompare = colorDisk.pointer.GetComponentInChildren<Image>().color;

        // methods library
        ColorLibrary library = new ColorLibrary();

        // array of difference
        float[] differenceIntensity = new float[allImages.image.Length];

        
        // compare all images according to HSV method
        for (int i = 0; i < allImages.image.Length; i++)
        {
            differenceIntensity[i] = library.DifferenceValueHSV(allImages.colorOfImage[i], globalColorToCompare);
            Debug.Log(differenceIntensity[i]);
        }
        
        // sort images 
        Array.Sort(differenceIntensity, allImages.image);
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
