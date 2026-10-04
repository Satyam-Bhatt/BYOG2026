using UnityEngine;
using UnityEngine.InputSystem;

public class TestingCameraSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.easyMode) return;

        if(Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.right * 10 * Time.deltaTime;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            transform.position += Vector3.up * 10 * Time.deltaTime;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.left * 10 * Time.deltaTime;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform.position += Vector3.down * 10 * Time.deltaTime;
        }
        if (Keyboard.current.qKey.isPressed)
        {
            transform.position += Vector3.forward * 10 * Time.deltaTime;
        }
        if (Keyboard.current.eKey.isPressed)
        {
            transform.position -= Vector3.forward * 10 * Time.deltaTime;
        }
    }
}
