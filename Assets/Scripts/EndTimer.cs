using UnityEngine;
using UnityEngine.UI;

public class EndTimer : MonoBehaviour
{
    [SerializeField]
    private Text timeKeeper = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (timeKeeper.GetComponent<UpdateTimeDisplay>().timerRunning)
                timeKeeper.GetComponent<UpdateTimeDisplay>().endTimer();
        }
    }
}
