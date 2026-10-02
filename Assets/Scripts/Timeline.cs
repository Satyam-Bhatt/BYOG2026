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
        if(currentFrame > containers.Length)
        {
            currentFrame = containers.Length;
            return;
        }

        if (containers[currentFrame].m_VectorData == null) return;


        Vector2 vec = containers[currentFrame].m_VectorData.data;
        m_Player.transform.position += new Vector3(vec.x, vec.y, 0);
        containers[currentFrame].m_VectorData.canBePicked = false;
        currentFrame++;
    }

    public void PlayAll()
    {

    }

    public void PreviousFrame()
    {
        currentFrame--;

        if(currentFrame == -1)
        {
            m_Player.transform.position = startPos;
            currentFrame = 0;
            return;
        }

        Vector2 vec = containers[currentFrame].m_VectorData.data;
        m_Player.transform.position -= new Vector3(vec.x, vec.y, 0);
        containers[currentFrame].m_VectorData.canBePicked = true;
    }
}
