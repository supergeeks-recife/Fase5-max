using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent OnAreaEnter;
    public UnityEvent OnAreaExit;

    public GameObjectEvent OnObjectEnter;
    public GameObjectEvent OnObjectExit;
    public GameObjectEvent OnObjectCollide;

    public string detectTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(detectTag))
        {
            OnAreaEnter.Invoke();
            OnObjectEnter.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(detectTag))
        {
            OnAreaExit.Invoke();
            OnObjectExit.Invoke(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(detectTag))
        {
            OnObjectCollide.Invoke(collision.gameObject);
        }
    }
}
