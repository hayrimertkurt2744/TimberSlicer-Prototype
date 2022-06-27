using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotate : MonoBehaviour
{
    public float angularVelocity=90;
    
    void Update()
    {
        if (GameManager.Instance.currentState==GameManager.GameState.Normal)
        {
            transform.Rotate(0, 0, angularVelocity * Time.deltaTime);
        }
        
    }
}
