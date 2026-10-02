using UnityEngine;

public class Data : MonoBehaviour
{
    [HideInInspector] public bool canBePicked = true;
    public virtual void UpdateData()
    { }
}
