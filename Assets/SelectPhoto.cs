using UnityEngine;
using UnityEngine.UI;

public class SelectPhoto : MonoBehaviour
{
    public GameObject colorBar;
    public GameObject image;

    private Image highlightedPhotoCanvas;
    private Image highlightedPhoto;
    private HistogramGenerator histogram;

    private bool isSelected = false;

    private void Start()
    {
        transform.parent.GetComponent<Canvas>().enabled = false;
        highlightedPhotoCanvas = GetComponentInChildren<Image>();
        histogram = GetComponent<HistogramGenerator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSelected)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Photo")))
            {
                transform.parent.GetComponent<Canvas>().enabled = true;

                highlightedPhoto = Instantiate(hit.collider.gameObject.GetComponent<Image>(), highlightedPhotoCanvas.transform);
                highlightedPhoto.rectTransform.anchorMax = new Vector2(0.5f, 0.6f);
                highlightedPhoto.rectTransform.anchorMin = new Vector2(0.5f, 0.6f);
                highlightedPhoto.rectTransform.pivot = new Vector2(0.5f, 0.5f);

                while (highlightedPhoto.rectTransform.rect.height < 300 && highlightedPhoto.rectTransform.rect.width < 430)
                { 
                    highlightedPhoto.rectTransform.sizeDelta *= 1.1f;
                }

                isSelected = true;

                ColorBar(highlightedPhoto.GetComponent<Image>().sprite.texture);
            }
        }
    }

    private void ColorBar(Texture2D texture)
    {
        Color32[] colorOfTexture = histogram.CreateHistogram(texture);
        int[] frequencyOfTexture = histogram.frequencyOfColors;

        float n = 1;

        for (int i = 0; i < colorOfTexture.Length; i++)
        {
            colorOfTexture[i].a = 255;

            for (int j = 0; j < frequencyOfTexture[i]; j++)
            {
                n++;
                GameObject newColor = Instantiate(image, colorBar.transform);
                newColor.GetComponent<Image>().color = colorOfTexture[i];
            }

            colorBar.GetComponent<GridLayoutGroup>().cellSize = new Vector2(408 / n, 100);
        }
    }

    public void Unselect()
    {
        Destroy(highlightedPhoto);
        transform.parent.GetComponent<Canvas>().enabled = false;
        isSelected = false;

        foreach (Transform child in colorBar. transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
