using UnityEngine;

public class Data : MonoBehaviour
{
    [HideInInspector] public bool canBePicked = true;
    public bool permanent = true;
    public virtual void UpdateData()
    { }
}
