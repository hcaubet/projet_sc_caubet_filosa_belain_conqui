using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager: MonoBehaviour
{
    public ImageToDisplay allImages;

    private void Start()
    {
        allImages.colorOfImage = new Color32[allImages.image.Length];
    }

    public void GoToChoseColor()
    {
        SceneManager.LoadScene("2.1 - Chose color", LoadSceneMode.Single);
    }

    public void GoToPaintColor()
    {
        SceneManager.LoadScene("2.2 - Paint color", LoadSceneMode.Single);
    }
}
