using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class SirenBehaviour : MonoBehaviour
{
    public Transform playerPosition;

    public bool canMove;

    public List<Transform> TravelPoints;

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
            transform.LookAt(playerPosition);

            if(transform.position == TravelPoints[travelPointsIndex].position)
                travelPointsIndex++;
            else
                transform.position = Vector3.MoveTowards(transform.position, TravelPoints[travelPointsIndex].position, 0.003f);
        }

        if(travelPointsIndex == 4)
        {
            OnBoardingGameManager.Instance.VisionTutoDone();
            canMove = true;
        }

        if(travelPointsIndex >= TravelPoints.Count)
        {
            canMove = false;
        }

    }
}
