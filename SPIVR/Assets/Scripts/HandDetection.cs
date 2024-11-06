using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDetection : MonoBehaviour
{
    public bool OnHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CupOnHand()
    {
        OnHand = true;
    }

    public void CupOffHand()
    {
        OnHand = false;
    }
}
