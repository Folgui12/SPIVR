using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLogo : MonoBehaviour
{
    [SerializeField] private SirenBehaviour Siren;
    public float fullTime;

    private RaycastHit hit;

    private SirenBehaviour sirenAux;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                sirenAux = hit.collider.GetComponent<SirenBehaviour>();

                if(sirenAux.GetInstanceID() == Siren.GetInstanceID())
                {
                    Siren.canMove = true;
                }
            }
            else
                Siren.canMove = false;
        }
        else
                Siren.canMove = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100.0f);
    }
}
