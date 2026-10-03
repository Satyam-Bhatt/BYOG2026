using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    public Container[] containers;
    public Transform m_Player;
    public static bool animPlaying = false;
    private int currentFrame = 0;
    public CameraRotator camRotator;

    private Image[] containerImages;
    public Slider slider;

    private Vector3 startPos = Vector3.zero;

    private void Start()
    {
        camRotator = FindFirstObjectByType<CameraRotator>();
        startPos = m_Player.transform.position;

        containerImages = new Image[containers.Length];
        for (int i = 0; i < containers.Length; i++)
        {
            containerImages[i] = containers[i].GetComponent<Image>();
        }

        slider.value = (float)currentFrame / (float)containers.Length;
        for (int i = 0; i < containers.Length; i++)
        {
            if (i < currentFrame) containerImages[i].color = Color.red;
            else if (i == currentFrame) containerImages[i].color = Color.green;
            else containerImages[i].color = Color.yellow;
        }
    }

    public void NextFrame()
    {
        if (currentFrame >= containers.Length)
        {
            currentFrame = containers.Length;
            return;
        }

        if (containers[currentFrame].m_VectorData == null) return;


        Vector3 vec = containers[currentFrame].m_VectorData.data;

        if (!containers[currentFrame].m_VectorData.permanent) Destroy(containers[currentFrame].m_VectorData.gameObject);

        StartCoroutine(MovePlayer(vec));
        // containers[currentFrame].m_VectorData.canBePicked = false;
        currentFrame++;
        
        slider.value = (float)currentFrame / (float)containers.Length;
        for (int i = 0; i < containers.Length; i++)
        {
            if (i < currentFrame) containerImages[i].color = Color.red;
            else if (i == currentFrame) containerImages[i].color = Color.green;
            else containerImages[i].color = Color.yellow;
        }
    }

    public void PlayAll()
    {
        Vector3 vec = containers[currentFrame].m_VectorData.data;
        // containers[currentFrame].m_VectorData.canBePicked = false;

        if (!containers[currentFrame].m_VectorData.permanent) Destroy(containers[currentFrame].m_VectorData.gameObject);

        StartCoroutine(MovePlayer(vec, CallBackMethod));
    }

    public void PreviousFrame()
    {
        currentFrame--;

        if (currentFrame == -1)
        {
            //m_Player.transform.position = startPos;
            currentFrame = 0;
            return;
        }

        if (containers[currentFrame].m_VectorData == null)
        {
            currentFrame++;
            return;
        }

        slider.value = (float)currentFrame / (float)containers.Length;
        for (int i = 0; i < containers.Length; i++)
        {
            if (i < currentFrame) containerImages[i].color = Color.red;
            else if (i == currentFrame) containerImages[i].color = Color.green;
            else containerImages[i].color = Color.yellow;
        }


        Vector3 vec = containers[currentFrame].m_VectorData.data;
        if (!containers[currentFrame].m_VectorData.permanent) Destroy(containers[currentFrame].m_VectorData.gameObject);

        StartCoroutine(MovePlayer(-vec));

        // containers[currentFrame].m_VectorData.canBePicked = true;
    }

    public IEnumerator MovePlayer(Vector3 move, Action doSomething = null)
    {
        if (animPlaying) yield break;

        if (!CameraRotator.IsCameraReset)
        {
            camRotator.ResetCamera();
            yield return new WaitUntil(() => CameraRotator.IsCameraReset);
            yield return new WaitForSeconds(0.5f);
        }


        animPlaying = true;

        m_Player.DOMove(m_Player.transform.position + move, 0.3f)
            .SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                animPlaying = false;
                doSomething?.Invoke();
            });
    }

    public void CallBackMethod()
    {
        currentFrame++;
        if (currentFrame == containers.Length || containers[currentFrame].m_VectorData == null) return;

        slider.value = (float)currentFrame / (float)containers.Length;
        for (int i = 0; i < containers.Length; i++)
        {
            if (i < currentFrame) containerImages[i].color = Color.red;
            else if (i == currentFrame) containerImages[i].color = Color.green;
            else containerImages[i].color = Color.yellow;
        }

        Vector3 vecI = containers[currentFrame].m_VectorData.data;
        // containers[currentFrame].m_VectorData.canBePicked = false;

        if (!containers[currentFrame].m_VectorData.permanent) Destroy(containers[currentFrame].m_VectorData.gameObject);

        StartCoroutine(MovePlayer(vecI, CallBackMethod));
    }
}
