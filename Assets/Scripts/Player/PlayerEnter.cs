using UnityEngine;
using UnityEngine.Events;

public class PlayerEnter : MonoBehaviour
{
    public UnityEvent unityEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            unityEvent.Invoke();
        }
    }
}
