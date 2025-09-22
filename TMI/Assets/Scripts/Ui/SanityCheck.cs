using UnityEngine;

public class TestDebug : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(">>> TestDebug.Awake");
    }

    private void Start()
    {
        Debug.Log(">>> TestDebug.Start");
    }
}
