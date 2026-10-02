using DG.Tweening;
using System;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public Container[] containers;
    public Transform m_Player;
    public static bool animPlaying = false;
    private int currentFrame = 0;

    private Vector3 startPos = Vector3.zero;

    private void Start()
    {
        startPos = m_Player.transform.position;
    }

    public void NextFrame()
    {
        if (currentFrame > containers.Length)
        {
            currentFrame = containers.Length;
            return;
        }

        if (containers[currentFrame].m_VectorData == null) return;


        Vector2 vec = containers[currentFrame].m_VectorData.data;
        MovePlayer(vec);
        containers[currentFrame].m_VectorData.canBePicked = false;
        currentFrame++;
    }

    public void PlayAll()
    {
        Vector2 vec = containers[currentFrame].m_VectorData.data;
        containers[currentFrame].m_VectorData.canBePicked = false;
        MovePlayer(vec, CallBackMethod);
    }

    public void PreviousFrame()
    {
        currentFrame--;

        if (currentFrame == -1)
        {
            m_Player.transform.position = startPos;
            currentFrame = 0;
            return;
        }

        Vector2 vec = containers[currentFrame].m_VectorData.data;
        MovePlayer(-vec);
        containers[currentFrame].m_VectorData.canBePicked = true;
    }

    public void MovePlayer(Vector2 move, Action doSomething = null)
    {
        if (animPlaying) return;

        animPlaying = true;

        m_Player.DOMove(m_Player.transform.position + new Vector3(move.x, move.y, 0), 0.3f)
            .SetEase(Ease.OutBack)
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

        Vector2 vecI = containers[currentFrame].m_VectorData.data;
        containers[currentFrame].m_VectorData.canBePicked = false;
        MovePlayer(vecI, CallBackMethod);
    }
}
