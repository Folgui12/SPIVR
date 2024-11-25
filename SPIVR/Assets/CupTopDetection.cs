using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CupTopDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CupTop"))
        {
            HandDetection cupOnHand = other.gameObject.GetComponent<HandDetection>();

            if (!cupOnHand.OnHand)
            {
                other.transform.parent = transform;
                other.transform.position = transform.position;
            }
        }
    }
}
