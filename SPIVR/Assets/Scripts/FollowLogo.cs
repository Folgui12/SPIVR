using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLogo : MonoBehaviour
{
    [SerializeField] private SirenBehaviour Siren;

    private RaycastHit hit;

    private SirenBehaviour sirenAux;
    private bool firstTimeWatchingSiren;

    private void Start()
    {
        firstTimeWatchingSiren = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                if(!firstTimeWatchingSiren)
                {
                    SpeakerManger.Instance.FirstTimeWatchingSiren();
                    firstTimeWatchingSiren = true;
                }

                sirenAux = hit.collider.GetComponent<SirenBehaviour>();

                if(sirenAux.GetInstanceID() == Siren.GetInstanceID())
                {
                    Siren.canMove = true;
                }
            }
            else if(Siren != null && Siren.TravelPointsIndexRef < 5)
            {
                Siren.canMove = false;
                if(AudioManager.Instance.PublicSource.isPlaying)
                {
                    AudioManager.Instance.PublicSource.Stop();
                }
            }
                
        }
        else if(Siren != null && Siren.TravelPointsIndexRef < 5)
        {
            Siren.canMove = false;
            if(AudioManager.Instance.PublicSource.isPlaying)
            {
                AudioManager.Instance.PublicSource.Stop();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100.0f);
    }
}
