using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BarScript : MonoBehaviour
{
    Vector3 storePosition;

    public Transform player;
    public bool moveInZ = true;

    bool initialized = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartPosition());
    }

    private void OnEnable()
    {
        StartCoroutine(StartPosition());
    }

    IEnumerator StartPosition()
    {
        yield return new WaitUntil(() => CreatingCartesian.allSet == true);
        storePosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialized = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!initialized) return;

        if (moveInZ)
            transform.position = new Vector3(storePosition.x, storePosition.y, player.position.z);
        else
            transform.position = new Vector3(player.position.x, storePosition.y, storePosition.z);
    }
}
