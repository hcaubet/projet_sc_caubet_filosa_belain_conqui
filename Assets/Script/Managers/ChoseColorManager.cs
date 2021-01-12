using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoseColorManager : MonoBehaviour
{
    public ImageToDisplay allImages;
    public Color32 globalColorToCompare;

    public PinColor colorDisk;
    private HistogramGenerator histogramGenerator;

    public void StartComparing()
    {
        histogramGenerator = GameObject.FindObjectOfType<HistogramGenerator>();
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
            float totalDifference = 0;
            Color32[] histogramOfTexture = histogramGenerator.CreateHistogram(allImages.image[i]);

            for (int c = 0; c < histogramOfTexture.Length; c++)
            {
                totalDifference += (histogramOfTexture.Length - c) * 
                    library.DifferenceValueLAB(globalColorToCompare, histogramOfTexture[c]);
            }

            totalDifference /= histogramOfTexture.Length * (histogramOfTexture.Length + 1) / 2;
            differenceIntensity[i] = totalDifference;
        }

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
