using UnityEngine;
using UnityEngine.InputSystem;

public class VectorData : Data
{
    public Vector3 data;

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float magnitude;

    private RectTransform rectT;

    private void Awake()
    {
        rectT = GetComponent<RectTransform>();
    }

    private void OnValidate()
    {
        direction = data.normalized;
        magnitude = data.magnitude;
        if (rectT == null) rectT = GetComponent<RectTransform>();
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 * magnitude);
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, data);
    }

    public void UpdateVector(Vector2 a_Data)
    {
        data = a_Data;
        direction = data.normalized;
        magnitude = data.magnitude;
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 * magnitude);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    public void UpdateVector3D(Vector3 a_Data)
    {
        data = a_Data;
        direction = data.normalized;
        magnitude = data.magnitude;
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 * magnitude);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, data);
    }

    public override void UpdateData() 
    {
        direction = data.normalized;
        magnitude = data.magnitude;
        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 * magnitude);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
