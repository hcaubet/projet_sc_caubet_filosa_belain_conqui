using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public GameObject brush;
    public float brushSize = 1;
    public Camera renderingCamera;

    public Texture2D painted;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name != "Image")
                {
                    GameObject brushPoint = Instantiate(brush, hit.point + Vector3.up * 0.1f, Quaternion.identity);
                    brushPoint.transform.localScale = Vector3.one * brushSize;
                    brushPoint.transform.parent = this.gameObject.transform;
                    brushPoint.transform.SetAsFirstSibling();
                }
                else
                {
                    GameObject brushPoint = Instantiate(brush, hit.point + Vector3.up * 0.1f - Vector3.up * hit.collider.gameObject.GetComponent<BoxCollider>().size.y*0.3f, Quaternion.identity);
                    brushPoint.transform.localScale = Vector3.one * brushSize;
                    brushPoint.transform.parent = this.gameObject.transform;
                    brushPoint.transform.SetAsLastSibling();
                }
            }    
        }

        if (Input.GetMouseButton(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Image")
            {
                Destroy(hit.collider.gameObject);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //RenderStarter();
        }
    }

    public void RenderStarter()
    {
        StartCoroutine(RenderPainting());
    }

    public IEnumerator RenderPainting()
    {

        if (renderingCamera.targetTexture != null)
        {
            renderingCamera.targetTexture.Release();
        }

        renderingCamera.targetTexture = new RenderTexture((int)(800 * gameObject.transform.localScale.x), (int)(800 * gameObject.transform.localScale.z), 1);

        yield return new WaitForEndOfFrame();

        //set active texture
        RenderTexture.active = renderingCamera.targetTexture;

        //convert rendering texture to texture2D
        Texture2D texture2D = new Texture2D(renderingCamera.targetTexture.width, renderingCamera.targetTexture.height);
        texture2D.ReadPixels(new Rect(0, 0, renderingCamera.targetTexture.width, renderingCamera.targetTexture.height), 0, 0);
        texture2D.Apply();
        painted = texture2D;
    }

    public void Launch()
    {
        GameObject.FindObjectOfType<FindNearestColoredImage>().Compare(painted);
    }
}
