using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScalarData : Data
{
    public float scalarData;
    [HideInInspector] public TMP_Text m_Text;

    private void Awake()
    {
        m_Text = GetComponentInChildren<TMP_Text>();
    }

    private void OnValidate()
    {
        m_Text = GetComponentInChildren<TMP_Text>();
        m_Text.text = scalarData.ToString();
        if (!permanent)
        {
            m_Text.color = Color.brown;
        }
        else m_Text.color = Color.white;
    }

    private void Start()
    {
        m_Text.text = scalarData.ToString(); 
        if (!permanent)
        {
            m_Text.color = Color.brown;
        }
    }
    public override void UpdateData()
    {
        m_Text.text = scalarData.ToString();

        if (!permanent)
        {
            m_Text.color = Color.brown;
        }
        else m_Text.color = Color.white;
    }
    public void UpdateData(float scalar)
    {
        scalarData = scalar;
        m_Text.text = scalarData.ToString();

        if (!permanent)
        {
            m_Text.color = Color.brown;
        }
        else m_Text.color = Color.white;
    }

}
