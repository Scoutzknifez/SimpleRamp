using UnityEngine;

public class DisableOnTrigger : MonoBehaviour
{
    public GameObject toDisableOnTrigger;
    public GameObject gate;
    public GameObject gateParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (toDisableOnTrigger.activeSelf)
            {
                toDisableOnTrigger.SetActive(false);
                gate.GetComponent<Animator>().enabled = true;
                gateParticles.SetActive(true);
            }
        }
    }
}
