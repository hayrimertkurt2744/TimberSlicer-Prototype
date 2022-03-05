using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.onTouchStart += ProcessPlayerInput;
        InputManager.Instance.onTouchMove += ProcessPlayerInput;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        InputManager.Instance.onTouchStart -= ProcessPlayerInput;
        InputManager.Instance.onTouchMove -= ProcessPlayerInput;
    }
    private void ProcessPlayerInput()
    {   
        if(GameManager.Instance.currentState==GameManager.GameState.Normal)
        {
            GetComponent<Mover>().MoveTo(new Vector3(
            InputManager.Instance.GetDirection().x * GameManager.Instance.horizontalSpeed,
            0f,
            0f));
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.GetComponent<Character>()!=null)
        { 
            if (other.GetComponent<MeshRenderer>().material==GetComponent<Character>().currentMaterial)
            {
                other.GetComponent<MeshRenderer>().material==GetComponent().currentMaterial
            }
        }
    }
}
