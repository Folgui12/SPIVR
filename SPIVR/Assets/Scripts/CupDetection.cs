using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CupDetection : MonoBehaviour
{
    [SerializeField] private float coffeTime;

    [SerializeField] private float timeToMake;

    private bool startProcess;

    private XRGrabInteractable cupInteractable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startProcess)
        {
            timeToMake += Time.deltaTime;

            if (timeToMake > coffeTime)
            {
                cupInteractable.enabled = true;
                startProcess = false;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Cup"))
        {
            cupInteractable = other.gameObject.GetComponent<XRGrabInteractable>();
            HandDetection cupOnHand = other.gameObject.GetComponent<HandDetection>();

            if (!cupOnHand.OnHand)
            {
                cupInteractable.transform.position = transform.position;
                cupInteractable.enabled = false;
                startProcess = true;
            }
        }
    }

}
