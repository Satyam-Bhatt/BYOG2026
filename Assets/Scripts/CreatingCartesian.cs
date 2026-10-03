using TMPro;
using UnityEngine;

public class CreatingCartesian : MonoBehaviour
{
    public GameObject bars;
    public GameObject barsX;
    public int numberOfBars = 10;
    public Transform barContainerTransform;
    public Transform barXContainerTransform;
    public Transform vertical, horizontal;
    public static bool allSet = false;

    private void Start()
    {
        for (int i = 0; i < numberOfBars; i++)
        {
            GameObject bar = Instantiate(bars, new Vector3(i, 0, 0), Quaternion.identity, barContainerTransform);

            GameObject bar1 = Instantiate(bars, new Vector3(-i, 0, 0), Quaternion.identity, barContainerTransform);

            GameObject bar2 = Instantiate(bars, new Vector3(0, i, 0), Quaternion.Euler(0, 0, 90), barContainerTransform);

            GameObject bar3 = Instantiate(bars, new Vector3(0, -i, 0), Quaternion.Euler(0, 0, 90), barContainerTransform);

            GameObject bar4 = Instantiate(barsX, new Vector3(0, i, 0), Quaternion.Euler(0, 90, 90), barXContainerTransform);

            GameObject bar5 = Instantiate(barsX, new Vector3(0, -i, 0), Quaternion.Euler(0, 90, 90), barXContainerTransform);

            GameObject bar6 = Instantiate(barsX, new Vector3(0, 0, i), Quaternion.Euler(0, 90, 0), barXContainerTransform);

            GameObject bar7 = Instantiate(barsX, new Vector3(0, 0, -i), Quaternion.Euler(0, 90, 0), barXContainerTransform);

        }
        vertical.transform.position = Vector3.zero;
        horizontal.transform.position = Vector3.zero;

        allSet = true;
    }

    private void Update()
    {
        barContainerTransform.gameObject.SetActive(GameManager.Instance.XYview);
        barXContainerTransform.gameObject.SetActive(!GameManager.Instance.XYview);
    }
}
