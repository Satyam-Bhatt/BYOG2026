using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuItems : MonoBehaviour
{
    public GameObject nextButton;

    private void Start()
    {
        nextButton.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnWin += Win;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnWin -= Win;
    }

    public void Win()
    {
        nextButton.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
