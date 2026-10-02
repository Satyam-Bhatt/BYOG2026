using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    public GameObject label;
    private TMP_Text labelText;
    private Vector3 initialPosition;
    GameObject m_PickedUpObject = null;

    private void Awake()
    {
        labelText = label.GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        ShowLabel();

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

        Data vD = go.GetComponentInChildren<Data>();
        if (!vD.canBePicked) return;

        m_PickedUpObject = go;
        m_PickedUpObject.GetComponent<Data>().UpdateData();
        initialPosition = m_PickedUpObject.transform.position;
    }

    void Hold()
    {
        m_PickedUpObject.transform.position = Mouse.current.position.ReadValue();
    }    

    void Leave()
    {
        GameObject container = GetObjectUnderCursor("Container");
        if (container != null && !container.GetComponent<Container>().isFilled)
        {
            if (!container.GetComponent<Container>().m_AcceptEverything)
            {
                VectorData vD = m_PickedUpObject.GetComponent<VectorData>();
                if (vD == null)
                {
                    m_PickedUpObject.transform.position = initialPosition;
                    m_PickedUpObject = null; 
                    return;
                }
            }

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
            m_PickedUpObject.transform.position = initialPosition;
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

    void ShowLabel()
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        mouseScreenPos.z = 10f;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        int nearestX = Mathf.RoundToInt(mouseWorldPos.x);
        int nearestY = Mathf.RoundToInt(mouseWorldPos.y);

        float distanceX = Mathf.Abs(mouseWorldPos.x - nearestX);
        float distanceY = Mathf.Abs(mouseWorldPos.y - nearestY);

        if (distanceX < 0.2f && distanceY < 0.2f)
        {
            label.SetActive(true);
            label.transform.position = Mouse.current.position.ReadValue();

            RectTransform rectTransform = label.GetComponent<RectTransform>();
            Canvas canvas = label.GetComponentInParent<Canvas>();

            float scaleFactor = canvas.scaleFactor;
            Vector3 offset = new Vector3(
                (rectTransform.rect.width / 2 + 20) * scaleFactor,
                (rectTransform.rect.height / 2 + 10) * scaleFactor,
                0
            );

            label.transform.position += offset;
            labelText.text = $"({nearestX}, {nearestY})";
        }
        else
        {
            label.SetActive(false);
        }
    }
}
