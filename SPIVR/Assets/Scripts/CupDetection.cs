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

    private bool startProcess;

    private XRGrabInteractable cupInteractable;

    private bool canPlaceCups;

    // Start is called before the first frame update
    void Start()
    {
        canPlaceCups = false;
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
        startProcess = true;
        AudioManager.Instance.PlayOneShot(machineWorking);
        ChangeLights(2);
    }

    public void ProcessFinish()
    {
        cupInteractable.enabled = true;
        startProcess = false;
        AudioManager.Instance.PublicSource.Stop();
        ChangeLights(3);
        StartCoroutine("EndProcess"); 
    }

    public void ChangeLights(int state)
    {
        switch (state)
        {
            case 1:
                Lights[0].GetComponent<MeshRenderer>().material.color = new Color(1f, .65f, .65f);
                Lights[1].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                Lights[2].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                break;

            case 2:
                Lights[0].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                Lights[1].GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, .65f);
                Lights[2].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                break;

            case 3:
                Lights[0].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                Lights[1].GetComponent<MeshRenderer>().material.color = new Color(.65f, .65f, .65f);
                Lights[2].GetComponent<MeshRenderer>().material.color = new Color(.65f, 1f, .65f);
                break;

            default:
                break;
        }
    }

    IEnumerator EndProcess()
    {
        yield return new WaitForSeconds(1.5f);

        ChangeLights(1);
        canPlaceCups = true;
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
                cupInteractable.transform.position = transform.position;
                cupInteractable.enabled = false;
                canPlaceCups = false;
            }
        }
    }

}
