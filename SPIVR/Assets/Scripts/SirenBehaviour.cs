using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip SirenMoving;
    public Transform playerPosition;
    public bool canMove;
    public bool firstDialog;
    public List<Transform> TravelPoints;  
    public int TravelPointsIndexRef => travelPointsIndex;
    private int travelPointsIndex;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
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
                AudioManager.Instance.PlayOneShot(SirenMoving, .4f);
            }
        }

        if(travelPointsIndex == 5)
        {
            canMove = true;
        }

        if(travelPointsIndex >= TravelPoints.Count)
        {
            canMove = false;
            if(AudioManager.Instance.PublicSource.isPlaying)
            {
                AudioManager.Instance.PublicSource.Stop();
            }
        }
    }

    void OnEnable()
    {
        
    }
}
