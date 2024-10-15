using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoucerBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("The Challenger approaches! Reclaim Heaven's light to reignite the world!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Where do you go, Coward! You shun Heaven's Light!");
        }
    }
}
