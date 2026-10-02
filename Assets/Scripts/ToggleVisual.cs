using UnityEngine;
using UnityEngine.UI;

public class ToggleVisual : MonoBehaviour
{
    private Toggle m_Toggle;
    private ColorBlock m_CB;

    private void Awake()
    {
        m_Toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        if (m_Toggle != null)
        {
            m_CB = m_Toggle.colors;
        }
    }

    public void ToggleSwitch()
    {
        if (m_Toggle.isOn)
        {
            ColorBlock cb = m_Toggle.colors;
            cb.normalColor = Color.green;
            m_Toggle.colors = cb;
        }
        else
            m_Toggle.colors = m_CB;
    }
}
