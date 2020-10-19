using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GetColors colorRepartition;
    public GameObject prefabDisplayer;
    public GameObject prefabCube;

    GameObject[] panels;
    GameObject[] bubbles;

    public void CreatePanels()
    {
        colorRepartition = FindObjectOfType<GetColors>();

        int repartition = colorRepartition.repartition;
        ColorImage[] categories = colorRepartition.colorScriptables;

        int index = 0;
        for (int i = 0; i < repartition; i++)
        {
            if (categories[i].image.Length != 0 || categories[i].image.Length == 0)
            {
                GameObject newPanel = Instantiate(prefabDisplayer);

                newPanel.GetComponent<PhotoDisplayer>().colorImage = categories[i];
                newPanel.GetComponent<PhotoDisplayer>().CreatePanel();

                panels[index] = newPanel;
                index++;
            }
        }

        bubbles = GameObject.FindGameObjectsWithTag("Bubbles");
        index = 0;

        foreach (GameObject b in bubbles)
        {
            GameObject newBubble = Instantiate(prefabCube);
            newBubble.GetComponent<MeshRenderer>().material = b.GetComponent<MeshRenderer>().material;
            bubbles[index] = newBubble;
            index++;
        }

        SetCoords();
    }

    void SetCoords()
    {
        float angle = 2 * 3.14f / colorRepartition.repartition;

        for (int i = 0; i < colorRepartition.repartition; i++)
        {
            float x = 100 * Mathf.Cos(angle * i);
            float y = 100 * Mathf.Sin(angle * i);


            panels[i].transform.position = new Vector3(x, 2, y);
            bubbles[i].transform.position = new Vector3(x / 25, 2, y / 25);


            var lookAtPos = gameObject.transform.position;
            lookAtPos.y = panels[i].transform.position.y;

            panels[i].transform.LookAt(lookAtPos);
        }
    }
}
