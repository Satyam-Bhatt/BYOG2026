using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour
{
    TMP_Text t;

    private void Awake()
    {
        t = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        t.text = $"Level No. {SceneManager.GetActiveScene().buildIndex}";
    }
}
