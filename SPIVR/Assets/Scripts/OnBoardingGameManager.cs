using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OnBoardingGameManager : MonoBehaviour
{
    public static OnBoardingGameManager Instance;
    [SerializeField] private GameObject Player;
    [SerializeField] private AudioClip PickUpCup;

    private bool firstTimePickingUpACup;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        firstTimePickingUpACup = false;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void ActivateController()
    {
        Player.GetComponent<CharacterController>().enabled = true;
    }

    public void PickedCup()
    {
        if(!firstTimePickingUpACup)
        {
            SpeakerManger.Instance.PlayerPickUpCup();
            firstTimePickingUpACup = true;
        }
            
        AudioManager.Instance.PlayOneShot(PickUpCup);
    } 
}
