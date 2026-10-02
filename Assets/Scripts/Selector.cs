using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    GameObject m_PickedUpObject = null;

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
            PickUp(GetObjectUnderCursor("InteractableVector"));

        if(Mouse.current.leftButton.isPressed && m_PickedUpObject != null)
        {
            Hold();
        }

        if(Mouse.current.leftButton.wasReleasedThisFrame && m_PickedUpObject != null)
            Leave();
    }

    void PickUp(GameObject go)
    {
        if(go == null) return;

        VectorData vD = go.GetComponentInChildren<VectorData>();
        if (!vD.canBePicked) return;

        m_PickedUpObject = go;
        m_PickedUpObject.GetComponent<VectorData>().UpdateVector();
    }

    void Hold()
    {
        m_PickedUpObject.transform.position = Mouse.current.position.ReadValue();
    }    

    void Leave()
    {
        GameObject container = GetObjectUnderCursor("Container");
        if (container != null)
        {
            m_PickedUpObject.transform.SetParent(container.transform);
            RectTransform rectObj = m_PickedUpObject.GetComponent<RectTransform>();
            RectTransform rectContainer = container.GetComponent<RectTransform>();
            rectObj.anchoredPosition = Vector2.zero;
            if(rectObj.sizeDelta.y >= rectContainer.sizeDelta.y)
            {
                float sizeDiff = rectObj.sizeDelta.y / rectContainer.sizeDelta.x;
                rectObj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectObj.sizeDelta.y / sizeDiff - 20);
            }
        }
        else
        {
            m_PickedUpObject.transform.SetParent(this.transform);
        }

        m_PickedUpObject = null;
    }

    GameObject GetObjectUnderCursor(string tagName)
    {
        var data = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        foreach (var r in results)
        {
            if (r.gameObject.CompareTag(tagName))
                return r.gameObject;
        }

        return null;
    }
}
