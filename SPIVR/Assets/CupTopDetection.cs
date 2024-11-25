using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Transformation;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CupTopDetection : MonoBehaviour
{
    [SerializeField] private AudioClip LidClose; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CupTop"))
        {
            HandDetection cupOnHand = other.gameObject.GetComponent<HandDetection>();

            if (!cupOnHand.OnHand)
            {
                AudioManager.Instance.PlayOneShot(LidClose);
                other.transform.parent = transform;
                other.transform.position = transform.position;

                if(transform.GetComponentInParent<HandDetection>().CoffeReadyToClose)
                {
                    SpeakerManger.Instance.CoffeReady();
                }
            }
        }
    }
}
