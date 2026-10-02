using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VectorManipulator : MonoBehaviour
{
    public GameObject m_Container1;
    public GameObject m_Container2;
    public Toggle[] m_Operations;
    public GameObject m_OutputContainer;
    public GameObject m_OutputContainer_Scalar;
    [Header("Prefabs")]
    public GameObject m_Vector;
    public GameObject m_Scalar;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            ComputeVector();
    }

    public void ComputeVector()
    {
        VectorData v1 = null, v2 = null;
        ScalarData s1 = null, s2 = null;
        ValueType vType;
        OperationType oType = OperationType.Invalid;

        if (m_Container1.GetComponentInChildren<VectorData>())
            v1 = m_Container1.GetComponentInChildren<VectorData>();
        else
            s1 = m_Container1.GetComponentInChildren<ScalarData>();

        if (m_Container2.GetComponentInChildren<VectorData>())
            v2 = m_Container2.GetComponentInChildren<VectorData>();
        else
            s2 = m_Container2.GetComponentInChildren<ScalarData>();

        if (v1 != null && v2 != null) vType = ValueType.VectorOp;
        else if (s1 != null && s2 != null) vType = ValueType.ScalarOp;
        else if ((v1 != null && s2 != null) || (v2 != null && s1 != null)) vType = ValueType.VectorScalarOp;
        else vType = ValueType.Invalid;

        if (vType == ValueType.Invalid) return;

        for (int i = 0; i < m_Operations.Length; i++)
        {
            if(!m_Operations[i].isOn) continue;

            if (i == 0) oType = OperationType.Add;
            if (i == 1) oType = OperationType.Subtract;
            if (i == 2) oType = OperationType.Multiply;

            if(vType == ValueType.VectorOp)
            {
                Vector2 val1 = v1.data;
                Vector2 val2 = v2.data;
                Vector3 output = Vector3.zero;
                float scalarOutput = 0;
                bool vector3D = false;
                if (oType == OperationType.Add) output = val1 + val2;
                if (oType == OperationType.Subtract) output = val1 - val2;
                if (oType == OperationType.Multiply)
                {
                    output = Vector3.Cross(val1, val2);
                    scalarOutput = Vector3.Dot(val1, val2);
                    vector3D = true;

                    GameObject scalarObj = Instantiate(m_Scalar, m_Container2.transform);
                    ScalarData newScalarData = scalarObj.GetComponent<ScalarData>();
                    newScalarData.UpdateData(scalarOutput);
                }

                GameObject obj = Instantiate(m_Vector, m_OutputContainer.transform);
                VectorData newVectorData = obj.GetComponent<VectorData>();
                
                if(vector3D) newVectorData.UpdateVector3D(output);
                else newVectorData.UpdateVector(output);

                RectTransform rectObj = obj.GetComponent<RectTransform>();
                RectTransform rectContainer = m_OutputContainer.GetComponent<RectTransform>();

                if (rectObj.sizeDelta.y >= rectContainer.sizeDelta.y)
                {
                    float sizeDiff = rectObj.sizeDelta.y / rectContainer.sizeDelta.x;
                    rectObj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectObj.sizeDelta.y / sizeDiff - 20);
                }
            }
            break;
        }
    }
}

public enum OperationType
{
    Add,
    Subtract,
    Multiply,
    Invalid
}

public enum ValueType
{
    VectorOp,
    VectorScalarOp,
    ScalarOp,
    Invalid
}
