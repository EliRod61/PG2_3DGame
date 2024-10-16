using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CycleContinues : MonoBehaviour
{
    void OnTriggerEnter(Collider myCollision)
    {
        for (int i = 0; i < 40; i++)
        {
            Debug.Log("You are trapped here...thanks for playing");
        }
    }
}
