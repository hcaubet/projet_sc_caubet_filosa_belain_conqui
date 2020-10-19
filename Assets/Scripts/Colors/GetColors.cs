using System;
using UnityEngine;

public class GetColors : MonoBehaviour
{
    [Range(3,30)]
    public int repartition = 1;

    public Color32[] color;
    public ColorImage[] colorScriptables;
    
    public GameObject bubble;
    public Shader bubbleShader;

    private void Start ()
    {
        colorScriptables = new ColorImage[repartition];
        color = new Color32[repartition];

        GetCoord();
        CreateScriptable();

        GetComponent<SetColors>().AttributeColor();
    }

    void GetCoord()
    {

        float angle = 2 * 3.14f / repartition;

        for (int i = 0; i < repartition; i++)
        {
            float x = 100 * Mathf.Cos(angle * i);
            float y = 100 * Mathf.Sin(angle * i);

            CreateBubble(x / 30, y / 30, i);
        }
    }

    void CreateBubble(float x, float y, int i)
    {
        Vector3 position = transform.position + new Vector3(x, 2, y);
        GameObject newBubble = Instantiate(bubble, position, Quaternion.identity);
        newBubble.transform.parent = this.gameObject.transform;
        color[i] = GetColorUnderBubble(newBubble);

        Material bubbleColor = new Material(bubbleShader);
        bubbleColor.color = color[i];
        newBubble.GetComponent<MeshRenderer>().material = bubbleColor;
    }

    Color32 GetColorUnderBubble(GameObject bubble)
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(bubble.transform.position, Vector3.down, out raycastHit))
        {
            Renderer renderer = raycastHit.collider.GetComponent<MeshRenderer>();
            Texture2D texture2D = renderer.material.mainTexture as Texture2D;
            Vector2 pCoord = raycastHit.textureCoord;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;

            Vector2 tiling = renderer.material.mainTextureScale;
            return texture2D.GetPixel(Mathf.FloorToInt(pCoord.x * tiling.x), Mathf.FloorToInt(pCoord.y * tiling.y));
        }
        else
            return new Color32(0, 0, 0, 0);
    }

    void CreateScriptable()
    {
        for (int i = 0; i < repartition; i++)
        {
            ColorImage newColor = (ColorImage)ScriptableObject.CreateInstance<ColorImage>();
            newColor.color = color[i];
            colorScriptables[i] = newColor;
        }
    }
}
