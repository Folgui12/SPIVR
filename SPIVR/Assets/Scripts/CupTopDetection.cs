using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Transformation;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CupTopDetection : MonoBehaviour
{
    [SerializeField] private AudioClip LidClose; 

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CupTop"))
        {
            HandDetection cupOnHand = other.gameObject.GetComponent<HandDetection>();

            if (!cupOnHand.OnHand)
            {
                AudioManager.Instance.PlayOneShot(LidClose);
                other.transform.parent = transform;
                other.transform.position = transform.position;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                other.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                other.gameObject.GetComponent<Collider>().enabled = false;

                if (transform.GetComponentInParent<HandDetection>().CoffeReadyToClose)
                {
                    SpeakerManger.Instance.CoffeReady();
                }
            }
        }
    }
}
