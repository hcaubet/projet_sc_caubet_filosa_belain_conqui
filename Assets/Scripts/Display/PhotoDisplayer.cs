using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhotoDisplayer : MonoBehaviour
{
    public ColorImage colorImage;
    public GameObject cube;

    public void CreatePanel()
    {
        if (GetComponentInChildren<Image>().name == "Color")
        {
            GetComponentInChildren<Image>().color = colorImage.color;
        }

        GetComponentInChildren<DynamicPhotoLayout>().sprites = colorImage.image;
        if (colorImage.image.Length != 0)
        {
            GetComponentInChildren<DynamicPhotoLayout>().Setup();
        }
    }


    public void Update()
    {
        if (cube.GetComponent<Renderer>().enabled == false)
        {
            foreach (Canvas r in GetComponentsInChildren<Canvas>())
                r.enabled = true;
        }
        else
        {
            foreach (Canvas r in GetComponentsInChildren<Canvas>())
                r.enabled = false;
        }
    }
}
