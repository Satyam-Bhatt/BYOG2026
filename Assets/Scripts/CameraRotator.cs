using DG.Tweening;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotator : MonoBehaviour
{
    private float targetY;
    private static bool rotating;
    public static bool IsCameraReset = true;

    private CinemachinePositionComposer cPC;

    private void Awake()
    {
        cPC = GetComponent<CinemachinePositionComposer>();
    }

    public void LeftButton()
    {
        if (rotating) return;

        rotating = true;
        targetY += 90;
        transform.DORotate(new Vector3(0, targetY, 0), 0.5f)
            .OnComplete(() =>
            {
                transform.eulerAngles = new Vector3(0, targetY, 0);
                UpdateCameraLook();
            });
    }

    public void RightButton()
    {
        if (rotating) return;

        rotating = true;
        targetY -= 90;
        transform.DORotate(new Vector3(0, targetY, 0), 0.5f)
            .OnComplete(() =>
            {
                transform.eulerAngles = new Vector3(0, targetY, 0);
                UpdateCameraLook();
            });
    }

    void UpdateCameraLook()
    {
        targetY %= 360;
        //Debug.Log(transform.eulerAngles.y + " || " + transform.rotation.y + " || " + targetY);
        if (targetY == 0 || Mathf.Abs(targetY) == 180)
        {
            GameManager.Instance.XYview = true;
        }
        else
        {
            GameManager.Instance.XYview = false;
        }

        if (targetY == 0)
        {
            IsCameraReset = true;
            cPC.TargetOffset.x = 5;
        }
        else cPC.TargetOffset.x = 0;

        rotating = false;
    }

    public void ResetCamera()
    {
        if (targetY == 0)
        {
            IsCameraReset = true;
            return;
        }

        if (rotating) return;
        rotating = true;
        targetY = 0;
        transform.DORotate(new Vector3(0, targetY, 0), 0.2f)
            .OnComplete(() =>
            {
                transform.eulerAngles = new Vector3(0, targetY, 0);
                UpdateCameraLook();
            });
    }
}
