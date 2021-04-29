using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    [SerializeField]
    private Text timeKeeper = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeKeeper.GetComponent<UpdateTimeDisplay>().startTimer();
        }
    }
}
