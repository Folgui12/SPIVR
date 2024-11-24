using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OnBoardingGameManager : MonoBehaviour
{
    public static OnBoardingGameManager Instance;

    [SerializeField] private GameObject Player;

    [SerializeField] private GameObject textView;
    [SerializeField] private GameObject textMove;
    [SerializeField] private GameObject textRotation;
    [SerializeField] private GameObject textCanvas;
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip IntroAudio;
    [SerializeField] private AudioClip FirstStepsAudio;

    public float walkTime;
    private float walkTimer;

    public float rotationRounds;
    public float delayBetweenRotationTime;
    private float rotationCounter;
    private float delayBetweenRotationTimer;
    private bool FirstSteps;
    private bool FirstRotation;
    private bool IntroAudioPlaying;
    private CharacterController pjController;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        Source.clip = IntroAudio;
        Source.Play();
        IntroAudioPlaying = false;
        FirstSteps = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!Source.isPlaying && IntroAudioPlaying)
        {
            IntroAudioPlaying = false;
            Player.GetComponent<CharacterController>().enabled = true;
        }

        if(!FirstSteps && !IntroAudioPlaying)
        {
            
        }

        if(FirstSteps && !FirstRotation)
        {
            
        }
    }

    public void FirstRotationsDone()
    {
        FirstRotation = true;
        textCanvas.SetActive(false);
    }

    public void FirstStepsDone()
    {
        FirstSteps = true;
        textMove.SetActive(false);
        textRotation.SetActive(true);
    }

    public void VisionTutoDone()
    {
        IntroAudioPlaying = true;
    }
}
