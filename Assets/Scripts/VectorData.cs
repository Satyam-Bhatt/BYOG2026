using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VectorData : Data
{
    [SerializeField] private Vector3 _data;
    public Vector3 data { get => _data; set { _data = value; DataUpdated(); } }

    public Image image;
    public Sprite intoTheScreen;
    public Sprite outOfTheScreen;
    public TMP_Text text;

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

    private void Start()
    {
        data = _data;
        UpdateData();
    }

    public void DataUpdated()
    {
        if (direction == new Vector3(0, 0, -1)) 
        {
            image.sprite = outOfTheScreen; 
            image.gameObject.SetActive(true);
        }
        else if (direction == new Vector3(0, 0, 1))
        {
            image.sprite = intoTheScreen; 
            image.gameObject.SetActive(true);
        }
        else image.gameObject.SetActive(false);

        text.text = $"({data.x}, {data.y}, {data.z})";
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
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, data);
    }
}
