using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDetection : MonoBehaviour
{
    public bool OnHand;
    public bool CoffeReadyToClose;
    public bool StayOnCup;

    // Start is called before the first frame update
    void Start()
    {
        CoffeReadyToClose = false;
        StayOnCup = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CupOnHand()
    {
        OnHand = true;
        OnBoardingGameManager.Instance.PickedCup();
    }

    public void CupOffHand()
    {
        OnHand = false;
    }
}
