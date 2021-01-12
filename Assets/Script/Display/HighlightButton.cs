using UnityEngine;
using UnityEngine.UI;

public class HighlightButton : MonoBehaviour
{
    public GameObject brush;
    private Button[] colors;
    private float k = 1.001f;
    private int i = 1;
    private Vector3 origin;

    void Start()
    {
        colors = GetComponentsInChildren<Button>();
        origin = colors[0].transform.localScale;

    }

    void Update()
    {
        k += 0.02f * i;
        if (k > 1.3f || k < 0.7f)
        {
            i *= -1;
        }


        foreach(Button b in colors)
        {
            if (b.GetComponent<Image>().color == brush.GetComponentInChildren<Image>().color)
            {
                b.transform.localScale = origin * k;
            }
            else
            {
                b.transform.localScale = origin;
            }
        }
    }
}
