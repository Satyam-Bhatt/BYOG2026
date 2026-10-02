using TMPro;
using UnityEngine;

public class CreatingCartesian : MonoBehaviour
{
    public GameObject bars;
    public int numberOfBars = 10;
    public Transform barContainerTransform;
    public Transform vertical, horizontal;

    public static bool allSet = false;

    private void Start()
    {
        for (int i = 0; i < numberOfBars; i++)
        {
            GameObject bar = Instantiate(bars, new Vector3(i, 0, 0), Quaternion.identity, barContainerTransform);

            GameObject bar1 = Instantiate(bars, new Vector3(-i, 0, 0), Quaternion.identity, barContainerTransform);

            GameObject bar2 = Instantiate(bars, new Vector3(0, i, 0), Quaternion.Euler(0,0,90), barContainerTransform);

            GameObject bar3 = Instantiate(bars, new Vector3(0, -i, 0), Quaternion.Euler(0, 0, 90), barContainerTransform);

        }
        vertical.transform.position = Vector3.zero;
        horizontal.transform.position = Vector3.zero;

        allSet = true;
    }
}
