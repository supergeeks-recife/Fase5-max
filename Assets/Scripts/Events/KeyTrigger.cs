using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyTrigger : MonoBehaviour
{
    public KeyCode key;

    public UnityEvent OnKeyPressed;
    public UnityEvent OnKeyReleased;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            OnKeyPressed.Invoke();
        }

        if (Input.GetKeyUp(key))
        {
            OnKeyReleased.Invoke();
        }
    }
}
