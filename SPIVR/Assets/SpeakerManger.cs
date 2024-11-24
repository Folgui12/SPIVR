using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerManger : MonoBehaviour
{
    public static SpeakerManger Instance;

    [SerializeField] private List<AudioClip> Audios;
    private AudioSource speaker;  
    private int audiosIndex;
    private SirenBehaviour sirenMovement;

    private bool firstAudioCue;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        sirenMovement = GetComponent<SirenBehaviour>();
        sirenMovement.enabled = false;
        sirenMovement.gameObject.GetComponent<Collider>().enabled = false;
        speaker = GetComponent<AudioSource>();
        audiosIndex = 0;
        StartCoroutine("SirenIntro"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SirenIntro()
    {
        speaker.clip = Audios[audiosIndex];

        yield return new WaitForSeconds(3f);

        // Sonido suave de campanita
        speaker.Play();

        yield return new WaitForSeconds(speaker.clip.length);

        audiosIndex++; 

        speaker.clip = Audios[audiosIndex];

        // Intro Narrador
        speaker.Play();

        yield return new WaitForSeconds(speaker.clip.length + 1f);

        audiosIndex++;

        speaker.clip = Audios[audiosIndex];

        // Primera instrucción
        speaker.Play();

        sirenMovement.enabled = true;
        sirenMovement.gameObject.GetComponent<Collider>().enabled = true;
    }

    public void PlayNextSound()
    {
        audiosIndex++;

        speaker.clip = Audios[audiosIndex];

        speaker.Play();
    }

    public void FirstTimeWatchingSiren()
    {
        PlayNextSound();
    }

    public void IntroWorkArea()
    {
        PlayNextSound();
    }

    public void PlayerCloseToDesk()
    {
        PlayNextSound();
    }

    public void PlayerPickUpCup()
    {
        
    }

    public void PlayerPutCupInMachine()
    {
        
    }
}
