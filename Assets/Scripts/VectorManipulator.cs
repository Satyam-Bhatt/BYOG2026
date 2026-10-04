using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VectorManipulator : MonoBehaviour
{
    public GameObject m_Container1;
    public GameObject m_Container2;
    public GameObject m_OutputContainer;
    public GameObject m_OutputContainer_Scalar;
    [Header("Prefabs")]
    public GameObject m_Vector;
    public GameObject m_Scalar;
    Color originalColor;

    private void Start()
    {
        originalColor = m_OutputContainer.GetComponent<Image>().color;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            ComputeVector();
    }

    public void ComputeVector(int i = 0)
    {
        if(m_OutputContainer.GetComponent<Container>().isFilled || m_OutputContainer_Scalar.GetComponent<Container>().isFilled)
        {
            Image i1 = m_OutputContainer.GetComponent<Image>();
            Image i2 = m_OutputContainer_Scalar.GetComponent<Image>();

            if(m_OutputContainer.GetComponent<Container>().isFilled)
            {
                Sequence flash = DOTween.Sequence();
                flash.Append(i1.DOColor(Color.red, 0.15f));
                flash.Append(i1.DOColor(originalColor, 0.15f));
            }
            if(m_OutputContainer_Scalar.GetComponent<Container>().isFilled)
            {
                Sequence flash = DOTween.Sequence();
                flash.Append(i2.DOColor(Color.red, 0.15f));
                flash.Append(i2.DOColor(originalColor, 0.15f));
            }
            return;
        }

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

        bool outputPermanent = true;
        if ((v1 != null && !v1.permanent) ||
            (v2 != null && !v2.permanent) ||
            (s1 != null && !s1.permanent) ||
            (s2 != null && !s2.permanent))
        {
            outputPermanent = false;
        }

        if (vType == ValueType.Invalid) return;

        if (i == 0) oType = OperationType.Add;
        if (i == 1) oType = OperationType.Subtract;
        if (i == 2) oType = OperationType.Multiply;

        if (vType == ValueType.VectorOp)
        {
            Vector3 val1 = v1.data;
            Vector3 val2 = v2.data;
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

                GameObject scalarObj = Instantiate(m_Scalar, m_OutputContainer_Scalar.transform);
                RectTransform rectScalar = scalarObj.GetComponent<RectTransform>();
                rectScalar.anchoredPosition = Vector3.zero;
                ScalarData newScalarData = scalarObj.GetComponent<ScalarData>();
                newScalarData.UpdateData(scalarOutput);
                if (!outputPermanent) newScalarData.permanent = false;
            }

            GameObject obj = Instantiate(m_Vector, m_OutputContainer.transform);
            VectorData newVectorData = obj.GetComponent<VectorData>();
            newVectorData.UpdateVector3D(output);
            //if (vector3D) newVectorData.UpdateVector3D(output);
            //else newVectorData.UpdateVector(output);

            if (!outputPermanent) newVectorData.permanent = false;

            RectTransform rectObj = obj.GetComponent<RectTransform>();
            RectTransform rectContainer = m_OutputContainer.GetComponent<RectTransform>();

            if (rectObj.sizeDelta.y >= rectContainer.sizeDelta.y)
            {
                float sizeDiff = rectObj.sizeDelta.y / rectContainer.sizeDelta.x;
                rectObj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectObj.sizeDelta.y / sizeDiff - 20);
            }
        }
        else if (vType == ValueType.VectorScalarOp)
        {
            Vector3 val1 = v1 != null ? v1.data : v2.data;
            float sVal2 = s1 != null ? s1.scalarData : s2.scalarData;

            Vector3 output = Vector3.zero;
            if (oType == OperationType.Add || oType == OperationType.Subtract)
            {
                Debug.LogError("Invalid Operand");
                return;
            }
            else if (oType == OperationType.Multiply) output = val1 * sVal2;

            GameObject obj = Instantiate(m_Vector, m_OutputContainer.transform);
            VectorData newVectorData = obj.GetComponent<VectorData>();
            newVectorData.UpdateVector3D(output);

            if (!outputPermanent) newVectorData.permanent = false;

            RectTransform rectObj = obj.GetComponent<RectTransform>();
            RectTransform rectContainer = m_OutputContainer.GetComponent<RectTransform>();

            if (rectObj.sizeDelta.y >= rectContainer.sizeDelta.y)
            {
                float sizeDiff = rectObj.sizeDelta.y / rectContainer.sizeDelta.x;
                rectObj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectObj.sizeDelta.y / sizeDiff - 20);
            }
        }
        else if (vType == ValueType.ScalarOp)
        {
            float val1 = s1.scalarData;
            float val2 = s2.scalarData;

            float scalarOutput = 0;
            if (oType == OperationType.Add) scalarOutput = val1 + val2;
            else if (oType == OperationType.Subtract) scalarOutput = val1 - val2;
            else if (oType == OperationType.Multiply) scalarOutput = val1 * val2;

            GameObject scalarObj = Instantiate(m_Scalar, m_OutputContainer_Scalar.transform);
            RectTransform rectScalar = scalarObj.GetComponent<RectTransform>();
            rectScalar.anchoredPosition = Vector3.zero;
            ScalarData newScalarData = scalarObj.GetComponent<ScalarData>();
            newScalarData.UpdateData(scalarOutput);

            if (!outputPermanent) newScalarData.permanent = false;
        }

        if (v1 != null && !v1.permanent) Destroy(v1.gameObject);
        if (v2 != null && !v2.permanent) Destroy(v2.gameObject);
        if (s1 != null && !s1.permanent) Destroy(s1.gameObject);
        if (s2 != null && !s2.permanent) Destroy(s2.gameObject);

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
