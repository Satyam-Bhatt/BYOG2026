using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnDie += Die;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnDie -= Die;
    }

    public void Die()
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }    
}
