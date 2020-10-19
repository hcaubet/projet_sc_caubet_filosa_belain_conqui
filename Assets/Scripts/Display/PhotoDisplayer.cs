using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhotoDisplayer : MonoBehaviour
{
    public ColorImage colorImage;
    public Image imagePrefab;

    public void CreatePanel()
    {
        if (GetComponentInChildren<Image>().name == "Color")
        {
            GetComponentInChildren<Image>().color = colorImage.color;
        }

        foreach (Texture2D image in colorImage.image)
        {
            Image newImage = Instantiate(imagePrefab, transform);
            newImage.transform.parent = GetComponentInChildren<GridLayoutGroup>().transform;
            newImage.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        }
    }
}
