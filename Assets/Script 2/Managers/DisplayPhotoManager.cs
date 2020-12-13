using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPhotoManager : MonoBehaviour
{
    public ImageToDisplay allImages;

    private void Start()
    {
        GameObject.FindObjectOfType<DynamicPhotoLayout>().allImages = allImages;
    }

    public void GoBackToMainMenu()
    {

    }

    public void GoBackToPaintColor()
    {

    }

    public void GoBackToChoseColor()
    {

    }

}
