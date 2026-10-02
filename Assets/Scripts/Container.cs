using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Container : MonoBehaviour
{
    public VectorData m_VectorData;

    private void Update()
    {
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StartCoroutine(CheckForChild());
        }
    }

    IEnumerator CheckForChild()
    {
        m_VectorData = GetComponentInChildren<VectorData>();
        yield return new WaitForEndOfFrame();
        if(m_VectorData == null)
            m_VectorData = GetComponentInChildren<VectorData>();
    }
}
