using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PaintColorManager : MonoBehaviour
{
    public ImageToDisplay allImages;
    public GameObject brush;

    private void Start()
    {    
        foreach (Button btn in GameObject.FindObjectsOfType<Button>())
        {
            btn.onClick.AddListener(delegate { ColorButtons(btn); });
        }
    }

 
    public void ResetPainting()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("brush"))
        {
            Destroy(g);
        }
    }

    private void ColorButtons(Button btnPressed)
    {
        switch (btnPressed.name)
        {
            case "color" :
                brush.GetComponentInChildren<Image>().color = btnPressed.GetComponentInChildren<Image>().color;
                break;
            case "Personnalisé":
                Debug.Log("test");
                break;
            default:
                break;
        }
    }


public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("1 - Main menu", LoadSceneMode.Single);
    }

    public void GoToPhotoDisplayer()
    {
        SceneManager.LoadScene("3 - Display photo", LoadSceneMode.Single);
    }
}
