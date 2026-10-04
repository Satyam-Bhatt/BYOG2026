using UnityEngine;
using DG.Tweening;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private Vector2 durationRange = new Vector2(1f, 3f);
    [SerializeField] private Vector2 angleRange = new Vector2(45f, 180f);
    [SerializeField] private Ease ease = Ease.InOutSine;
    [SerializeField] private bool useLocalSpace = true;

    private Tween tween;

    private void OnEnable() => RotateToRandom();

    private void OnDisable() => tween?.Kill();

    private void RotateToRandom()
    {
        float duration = Random.Range(durationRange.x, durationRange.y);
        float angle = Random.Range(angleRange.x, angleRange.y);
        Quaternion delta = Quaternion.AngleAxis(angle, Random.onUnitSphere);

        if (useLocalSpace)
        {
            Quaternion target = delta * transform.localRotation;
            tween = transform.DOLocalRotateQuaternion(target, duration);
        }
        else
        {
            Quaternion target = delta * transform.rotation;
            tween = transform.DORotateQuaternion(target, duration);
        }

        tween.SetEase(ease).OnComplete(RotateToRandom);
    }
}
