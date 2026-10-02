using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Container : MonoBehaviour
{
    public VectorData m_VectorData;
    public bool m_AcceptEverything = true;
    public bool isFilled = false;

    private void Update()
    {
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StartCoroutine(CheckForChild());
        }
    }

    IEnumerator CheckForChild()
    {
        isFilled = transform.childCount > 0;
        m_VectorData = GetComponentInChildren<VectorData>();
        yield return new WaitForEndOfFrame();
        if(m_VectorData == null)
            m_VectorData = GetComponentInChildren<VectorData>();
        isFilled = transform.childCount > 0;
    }
}
