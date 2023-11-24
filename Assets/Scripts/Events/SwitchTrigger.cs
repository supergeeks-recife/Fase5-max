using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchTrigger : MonoBehaviour
{
    bool state;

    public UnityEvent OnTurnOn;
    public UnityEvent OnTurnOff;

    public void Toggle()
    {
        state = !state;

        if (state)
        {
            OnTurnOn.Invoke();
        }
        else
        {
            OnTurnOff.Invoke();
        }
    }
}
