using System.Collections;
using UnityEngine;

public class BarScript : MonoBehaviour
{
    Vector3 storePosition;

    bool initialized = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartPosition());
    }

    IEnumerator StartPosition()
    {
        yield return new WaitUntil(() => CreatingCartesian.allSet == true);
        storePosition = transform.position;
        initialized = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!initialized) return;
            transform.position = storePosition;
    }
}
