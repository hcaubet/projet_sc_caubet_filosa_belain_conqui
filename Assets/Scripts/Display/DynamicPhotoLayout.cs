using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicPhotoLayout : MonoBehaviour
{

    public Texture2D[] sprites;

    public Image imagePrefab;
    private Image[] images;

    private RectTransform panel;
    public float resize = 400;

    private void Start()
    {
        panel = GetComponent<RectTransform>();
    }

    public void Setup()
    {
        panel = GetComponent<RectTransform>();
        GetImages();
        images = GetComponentsInChildren<Image>();
        PlaceImages();
    }

    private void GetImages()
    {
        foreach (Texture2D s in sprites)
    {
            Image newImage = Instantiate(imagePrefab, transform);
            newImage.transform.parent = panel.gameObject.transform;
            newImage.sprite = Sprite.Create(s, new Rect(0, 0, s.width, s.height), new Vector2(0.5f, 0.5f));
        }
    }

    private void PlaceImages()
    {
        float x = 0;
        float y = 0;

        int indexOfLine = 0;
        int indexOfImage = 0;
        int[,] lineOfImages = new int[images.Length, images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            // resize chaque image
            images[i].rectTransform.sizeDelta = new Vector2(images[i].sprite.rect.width / resize, images[i].sprite.rect.height / resize);

            // met dans un tableau 2D toutes les images et leurs lignes correspondantes
            if (images[i].sprite.rect.width / resize < panel.rect.width / 2 && x + images[i].sprite.rect.width / resize < panel.rect.width)
            {
                x += images[i].rectTransform.rect.width;
                indexOfImage++;
            }
            else
            {
                x = 0;
                indexOfLine++;
                indexOfImage = 0;
            }

            if (sprites.Length == 1)
            {
                lineOfImages[0, 0] = i;
            }
            else
                lineOfImages[indexOfLine, indexOfImage] = i;
        }

        // place images to right place
        for (int i = 0; i < indexOfLine+1; i++) 
        {
            x = 0;
            float maxY = 0;
            // get min height of line
            float minHeight = GetMinHeight(lineOfImages, i);

            // make all images on line the same size
            for (int j = 0; j < images.Length; j++)
            {
                if (lineOfImages[i, j] != 0 || (i == 0 && j == 0)) 
                {
                    // get ratio of image
                    float ratio = images[lineOfImages[i, j]].sprite.rect.width / images[lineOfImages[i, j]].sprite.rect.height;
                    // resize image to fit 
                    images[lineOfImages[i, j]].rectTransform.sizeDelta = new Vector2(ratio * minHeight, minHeight);
                    // place image
                    images[lineOfImages[i, j]].rectTransform.localPosition = new Vector3(x, y, 0);
                    // get correct x value to offset images 
                    x += ratio * minHeight;
                }
            }
            
            // make all images on line fit the panel
            float z = 1f;
            int k = 0;
            while (GetTotalWidth(lineOfImages, i) * z < panel.rect.width)
            {
                z += 0.005f;
            }

            while (GetTotalWidth(lineOfImages, i) * z > panel.rect.width)
            {
                z -= 0.005f;
            }

            x = 0;
            // make all images on line the same size
            for (int j = 0; j < images.Length; j++)
            {
                if (lineOfImages[i, j] != 0 || (i == 0 && j == 0))
                {
                    // resize again to fit panel
                    images[lineOfImages[i, j]].rectTransform.sizeDelta *= z;
                    // place image again
                    images[lineOfImages[i, j]].rectTransform.localPosition = new Vector3(x, y, 0);
                    // get correct x value to offset images 
                    x += images[lineOfImages[i, j]].rectTransform.rect.width + 0.05f;
                }
            }


            // update y offset
            y -= GetMinHeight(lineOfImages, i) + 0.05f;
        }
    }

    private float GetMinHeight(int[,] grid, int line)
    {
        float maxHeight = 0;
        for (int i = 0; i < images.Length; i++)
        {
            if (grid[line, i] != 0 || (i == 0 && line == 0))
            {
                maxHeight = Math.Max(maxHeight, images[grid[line, i]].rectTransform.rect.height);
            }
        }

        return maxHeight;
    }

    private float GetTotalWidth(int[,] grid, int line)
    {
        float sum = 0;
        for (int i = 0; i < images.Length; i++)
        {
            if (grid[line, i] != 0 || (i == 0 && line == 0))
            {
                sum +=images[grid[line, i]].rectTransform.rect.width;
            }
        }

        return sum;
    }
}
