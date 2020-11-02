using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public LayerMask layerPhoto;
    private Vector3 originPhoto;
    private DynamicPhotoLayout layout;

    private void Start()
    {
        layout = GetComponentInParent<DynamicPhotoLayout>();
        originPhoto = transform.position;
    }

    /*private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerPhoto) && (hit.collider.gameObject == this.gameObject))
        {
            transform.SetSiblingIndex(layout.sprites.Length);
            Vector3 toPlayer = Quaternion.AngleAxis(transform.parent.parent.parent.GetComponent<RectTransform>().eulerAngles.y, Vector3.up) * Vector3.forward / 3;
            transform.position = Vector3.Lerp(transform.position, originPhoto + toPlayer, 3 * Time.deltaTime);
        }
        else
        {
            transform.SetSiblingIndex(0);
            transform.position = Vector3.Lerp(transform.position, originPhoto, 3 * Time.deltaTime);
        }
    }*/
}
