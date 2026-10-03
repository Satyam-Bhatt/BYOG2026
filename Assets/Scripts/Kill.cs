using DG.Tweening;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
            GameManager.Instance.Die();
    }
}
