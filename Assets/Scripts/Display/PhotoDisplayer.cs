using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhotoDisplayer : MonoBehaviour
{
    public ColorImage colorImage;
    public GameObject cube;

    private Vector2 mask;

    private void Start()
    {
        mask = GetComponentInChildren<RectMask2D>().rectTransform.sizeDelta;
    }

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
            GetComponentInChildren<RectMask2D>().rectTransform.sizeDelta = mask;
        }
        else
        {
            GetComponentInChildren<RectMask2D>().rectTransform.sizeDelta = new Vector2(0, 0);
        }
    }
}
