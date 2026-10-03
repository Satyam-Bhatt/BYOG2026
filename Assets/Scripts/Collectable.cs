using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
            GameManager.Instance.Collected();

        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBounce)
            .OnComplete(() => Destroy(this));
    }
}
