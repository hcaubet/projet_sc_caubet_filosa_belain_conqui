using UnityEngine;
using UnityEngine.UI;

public class PinColor : MonoBehaviour
{
    public GameObject pointer;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                pointer.GetComponentInChildren<Image>().color = GetPointedColor();
            }
        }
    }

    private Color32 GetPointedColor()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name ==  "Color disc")
        {
            pointer.transform.position = new Vector3(hit.point.x, hit.point.y, pointer.transform.position.z);

            Renderer renderer = hit.collider.GetComponent<MeshRenderer>();
            Texture2D texture2D = renderer.material.mainTexture as Texture2D;
            Vector2 pCoord = hit.textureCoord;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;

            Vector2 tiling = renderer.material.mainTextureScale;
            return texture2D.GetPixel(Mathf.FloorToInt(pCoord.x * tiling.x), Mathf.FloorToInt(pCoord.y * tiling.y));
        }
        else
            return new Color32(0, 0, 0, 0);
    }
}
