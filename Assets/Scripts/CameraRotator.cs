using DG.Tweening;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotator : MonoBehaviour
{
    private float targetY;
    private static bool rotating;
    // Update is called once per frame
    void Update()
    {
        if (rotating) return;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            rotating = true;

            targetY += 90;
            transform.DORotate(new Vector3(0, targetY, 0), 0.5f)
                .OnComplete(() =>
                {
                    transform.eulerAngles = new Vector3(0, targetY, 0);
                    UpdateCameraLook();
                });
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            rotating = true;

            targetY -= 90;
            transform.DORotate(new Vector3(0, targetY, 0), 0.5f)
                .OnComplete(() =>
                {
                    transform.eulerAngles = new Vector3(0, targetY, 0);
                    UpdateCameraLook();
                });
        }

    }

    void UpdateCameraLook()
    {
        targetY %= 360;
        Debug.Log(transform.eulerAngles.y + " || " + transform.rotation.y + " || " + targetY);
        if (targetY == 0 || Mathf.Abs(targetY) == 180)
        {
            GameManager.Instance.XYview = true;
        }
        else
        {
            GameManager.Instance.XYview = false;
        }

        rotating = false;
    }
}
