using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class SirenBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip SirenMoving;
    public Transform playerPosition;
    public bool canMove;
    public List<Transform> TravelPoints;  
    public int TravelPointsIndexRef => travelPointsIndex;
    public int travelPointsIndex;
    private bool reachTable;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        reachTable = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if(transform.position == TravelPoints[travelPointsIndex].position)
                travelPointsIndex++;
            else
                transform.position = Vector3.MoveTowards(transform.position, TravelPoints[travelPointsIndex].position, 0.003f);

            if(!AudioManager.Instance.PublicSource.isPlaying)
            {
                AudioManager.Instance.PlayOneShot(SirenMoving, .2f);
            }
        }

        if(travelPointsIndex == 5)
        {
            canMove = true;
        }
        
        if(travelPointsIndex == 6 && !reachTable)
        {
            canMove = false;
            if(AudioManager.Instance.PublicSource.isPlaying)
            {
                AudioManager.Instance.PublicSource.Stop();
            }

            reachTable = true;

            SpeakerManger.Instance.IntroWorkArea();
        }
    }

    void OnEnable()
    {
        
    }
}
