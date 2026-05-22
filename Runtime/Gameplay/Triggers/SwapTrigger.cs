using UnityEngine;

public class SwapTrigger : MonoBehaviour
{
    public GameObject objectToSwapFrom;
    public GameObject objectToSwapTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Swap();
        }
    }

    public void Swap()
    {
        if (objectToSwapFrom.activeSelf)
        {
            objectToSwapFrom.SetActive(false);
            objectToSwapTo.SetActive(true);
        }
        else if (objectToSwapTo.activeSelf)
        {
            objectToSwapFrom.SetActive(true);
            objectToSwapTo.SetActive(false);
        }
        else // Neither were active, fall back to turn both off
        {
            objectToSwapFrom.SetActive(false);
            objectToSwapTo.SetActive(false);
        }
    }
}
