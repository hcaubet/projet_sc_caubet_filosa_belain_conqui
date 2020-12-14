using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintColorManager : MonoBehaviour
{
    public ImageToDisplay allImages;

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("1 - Main menu", LoadSceneMode.Single);
    }

    public void GoToPhotoDisplayer()
    {
        SceneManager.LoadScene("3 - Display photo", LoadSceneMode.Single);
    }
}
