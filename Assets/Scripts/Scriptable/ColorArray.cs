    using UnityEngine;

[CreateAssetMenu]
public class ColorArray : ScriptableObject
{
    public Texture2D image;
    public int resolution;
    public Color32[] colorArray;

    public void SetArray()
    {
        colorArray = new Color32[resolution * resolution];
        Color32[] texColors = image.GetPixels32();

        int squareWidth = image.width / resolution;
        int squareHeight = image.height / resolution;

        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float r = 0;
                float g = 0;
                float b = 0;

                int index = 0;
                for (int j = x * squareWidth; j < x * squareWidth + squareWidth; j++)
                {
                    for (int k = y * squareHeight; k < y * squareHeight + squareHeight; k++)
                    {
                        if (texColors[k * image.width + j].r != texColors[k * image.width + j].g || texColors[k * image.width + j].b != texColors[k * image.width + j].g - 1)
                        {
                            index++;
                            r += texColors[k * image.width + j].r;
                            g += texColors[k * image.width + j].g;
                            b += texColors[k * image.width + j].b;
                        }
                    }
                }

                if (index != 0)
                    colorArray[x * resolution + y] = new Color32((byte)(r / index), (byte)(g / index), (byte)(b / index), 0);
                else
                    colorArray[x * resolution + y] = new Color32((byte)255, (byte)255, (byte)254, 0);
            }
        }
    }
}
