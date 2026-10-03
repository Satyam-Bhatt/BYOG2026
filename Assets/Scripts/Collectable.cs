using DG.Tweening;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool activeTex = false;
    public TMP_Text text;

    private void Start()
    {
        if(activeTex)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
            GameManager.Instance.Collected();

        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce)
            .OnComplete(() => Destroy(this));
    }
}
