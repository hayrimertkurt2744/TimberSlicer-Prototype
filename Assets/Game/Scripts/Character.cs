using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
   public enum CharacterID
    {
        Player,
        Wall,
        Timber,
        None
    }
    public CharacterID currentCharacterID = CharacterID.None;
   

    [HideInInspector]public Vector3[] wallPath;

    public Transform pathPoint;
    public Transform pathPoint1;
    public Transform pathPoint2;
    private Vector3 vectoralPathPoint;
    private Vector3 vectoralPathPoint1;
    private Vector3 vectoralPathPoint2;
   
    
    

    private void Start()
    {

        

        if (this.currentCharacterID==CharacterID.Wall)
        {
            vectoralPathPoint = new Vector3(pathPoint.position.x, pathPoint.position.y, pathPoint.position.z);
            vectoralPathPoint1 = new Vector3(pathPoint1.position.x, pathPoint1.position.y, pathPoint1.position.z);
            vectoralPathPoint2 = new Vector3(pathPoint2.position.x, pathPoint2.position.y, pathPoint2.position.z);

            wallPath =new Vector3[]{ vectoralPathPoint,vectoralPathPoint1,vectoralPathPoint2 };
        }
       
    }

    private void CollisionHandler(CharacterID characterID)
    {


        switch (characterID)
        {
            
            case CharacterID.Wall:
                print("this is wall");
                
                break;
            case CharacterID.Timber:
                print("this is a timber");
                break;
            default:
                break;
        }

    }


}
