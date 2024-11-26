using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CupDetection : MonoBehaviour
{
    [SerializeField] private float coffeTime;

    [SerializeField] private float timeToMake;

    [SerializeField] private List<GameObject> Lights;

    [SerializeField] private AudioClip machineWorking;
    [SerializeField] private AudioClip ButtonPress;
    [SerializeField] private AudioClip LiquidPouring;
    [SerializeField] private AudioClip CupInMachine;
    [SerializeField] private AudioClip CoffeReady;

    private bool startProcess;

    private XRGrabInteractable cupInteractable;

    private bool canPlaceCups;

    private bool cupFullOfCoffe;

    // Start is called before the first frame update
    void Start()
    {
        canPlaceCups = true;
        cupFullOfCoffe = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startProcess)
        {
            timeToMake += Time.deltaTime;

            if (timeToMake > coffeTime)
            {
                ProcessFinish();
                timeToMake = 0;
            }
        }

    }

    public void StartProcess()
    {
        if(!canPlaceCups)
        {
            startProcess = true;
            AudioManager.Instance.PlayOneShot(machineWorking);
            ChangeLights(2);
        }
        
    }

    public void ProcessFinish()
    {
        cupInteractable.enabled = true;
        startProcess = false;
        AudioManager.Instance.PublicSource.Stop();
        StartCoroutine("EndProcess"); 
    }

    public void ChangeLights(int state)
    {
        switch (state)
        {
            case 1:
                Lights[0].GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f);
                Lights[1].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                Lights[2].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                break;

            case 2:
                Lights[0].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                Lights[1].GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 0f);
                Lights[2].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                break;

            case 3:
                Lights[0].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                Lights[1].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                Lights[2].GetComponent<MeshRenderer>().material.color = new Color(0f, 1f, 0f);
                break;

            default:
                break;
        }
    }

    IEnumerator EndProcess()
    {
        AudioManager.Instance.PlayOneShot(LiquidPouring);

        yield return new WaitForSeconds(5f);

        AudioManager.Instance.PublicSource.Stop();

        AudioManager.Instance.PlayOneShot(CoffeReady);

        ChangeLights(3);

        SpeakerManger.Instance.CoffeReadyToGrab();

        yield return new WaitForSeconds(SpeakerManger.Instance.PublicSource.clip.length);

        ChangeLights(1);

        canPlaceCups = true;
        cupFullOfCoffe = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cup") && canPlaceCups)
        {
            cupInteractable = other.gameObject.GetComponent<XRGrabInteractable>();
            HandDetection cupOnHand = other.gameObject.GetComponent<HandDetection>();

            if (!cupOnHand.OnHand)
            {
                SpeakerManger.Instance.PlayerPutCupInMachine();
                AudioManager.Instance.PlayOneShot(CupInMachine);
                cupInteractable.transform.position = transform.position;
                //cupInteractable.transform.rotation = transform.rotation;
                cupInteractable.enabled = false;
                canPlaceCups = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cup") && cupFullOfCoffe)
        {
            other.GetComponent<HandDetection>().CoffeReadyToClose = true;
            SpeakerManger.Instance.GrabCupOfCoffeFromMachine();
        } 
    }

}
