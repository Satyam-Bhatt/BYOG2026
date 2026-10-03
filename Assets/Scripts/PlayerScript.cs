using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public bool activeTex = false;
    public TMP_Text text;

    private void Start()
    {
        if (activeTex)
        {
            Vector3Int p = Vector3Int.RoundToInt(transform.position);
            string s = p.ToString();
            text.text = s;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnDie += Die;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnDie -= Die;
    }

    private void Update()
    {
        if (activeTex)
        {
            Vector3Int p = Vector3Int.RoundToInt(transform.position);
            string s = p.ToString();
            text.text = s;
        }
    }

    public void Die()
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }    
}
