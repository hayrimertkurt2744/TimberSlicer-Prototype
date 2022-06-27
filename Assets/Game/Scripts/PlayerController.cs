using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private bool isRotating=false;
    private bool isCollisionWithWall = false;
    

    public GameObject sawNormal;
    public GameObject sawBroken;
    public GameObject sawPieces;
    public ParticleSystem sparkleTrail;
    public Collider[] sawPiecesColliders;
    public float expForce=500,radius = 8;
    


    [Header("DG Settings")]
    public PathType pathType;
    public PathMode pathMode;
    public float actionSpeed=1f;
    public float shakeDuration ;
    public float shakeStrength = 3;
    private Transform gapJumpPoint;
    private Vector3 gapJumpVector;
    




    private void Start()
    {
        InputManager.Instance.onTouchStart += ProcessPlayerSwerve;
        InputManager.Instance.onTouchMove += ProcessPlayerSwerve;
        InputManager.Instance.onTouchStart += ProcessPlayerRotation;
        InputManager.Instance.onTouchMove += ProcessPlayerRotation;
        
        

    }
    private void OnDisable()
    {
        InputManager.Instance.onTouchStart -= ProcessPlayerSwerve;
        InputManager.Instance.onTouchMove -= ProcessPlayerSwerve;
        InputManager.Instance.onTouchStart -= ProcessPlayerRotation;
        InputManager.Instance.onTouchMove -= ProcessPlayerRotation;
        
    }
    private void Update()
    {

        ProcessPlayerMovement();
      
       
        

    }
    private void ProcessPlayerMovement()
    {
        if (GameManager.Instance.currentState==GameManager.GameState.Normal )
        {
            GetComponent<Mover>().MoveTo(new Vector3(0f, 0f, GameManager.Instance.forwardSpeed));
        }

    }
    private void ProcessPlayerSwerve()
    {
        if (GameManager.Instance.currentState==GameManager.GameState.Normal && isRotating==false)
        {
            
            GetComponent<Mover>().MoveTo(new
               Vector3(-InputManager.Instance.GetDirection().x *GameManager.Instance.horizontalSpeed, 0f, 0f));
           
        }

    }
    private void ProcessPlayerRotation()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Normal&& -InputManager.Instance.GetDirection().y>0.007 && isCollisionWithWall==false)
        {
            isRotating = true;
            transform.DORotate(new Vector3(0f, 0f, 90f), 0.2f, RotateMode.FastBeyond360);
            sparkleTrail.Play();
        }
        else if(GameManager.Instance.currentState == GameManager.GameState.Normal && -InputManager.Instance.GetDirection().y<-0.007 && isCollisionWithWall == false)
        {
            isRotating = true;
            transform.DORotate(new Vector3(0f, 0f, 0), 0.2f, RotateMode.FastBeyond360);
            sparkleTrail.Stop();
        }
        isRotating = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponent<Character>().currentCharacterID ==Character.CharacterID.Player && other.GetComponent<Character>() != null &&other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Wall)
        {
            isRotating = true;
            isCollisionWithWall = true;
            // CollisionHandler(other.GetComponent<Character>().currentCharacterID);
            if (other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Wall && GameManager.Instance.collisionCounter==0)
            {
                sparkleTrail.Stop();
                gameObject.transform.DOPath(other.GetComponent<Character>().wallPath, actionSpeed, pathType, pathMode, 10);
                CinemachineShake.Instance.ShakeCamera(0.8f, 0.8f);
                GameManager.Instance.collisionCounter += 1;
                sawNormal.SetActive(false);
                sawBroken.SetActive(true);
                sparkleTrail.Play();

            }
            else if (other.GetComponent<Character>().currentCharacterID == Character.CharacterID.Wall && GameManager.Instance.collisionCounter == 1)
            {
                sawBroken.SetActive(false);
                sawPieces.SetActive(true);
                KnockBack();
                GameManager.onLoseEvent?.Invoke();
                GameManager.Instance.collisionCounter -= 1;
                //sawNormal.SetActive(true);
                //sawPieces.SetActive(false);
            
            }
     
        }
        isRotating = false;
        isCollisionWithWall = false;
        if (other.gameObject.tag == "Gap")
        {
            print("triggered");
            sparkleTrail.Stop();
            gapJumpVector = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            gameObject.transform.DOJump(gapJumpVector, 8, 1, 2).OnComplete(()=> 
            {
                GameManager.onLoseEvent?.Invoke();
                GameManager.Instance.currentState = GameManager.GameState.Failed;
            });
        }

        

    }
    void KnockBack()
    {
        sawPiecesColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearby in sawPiecesColliders)
        {
            Rigidbody rigg = nearby.GetComponent<Rigidbody>();
            if (rigg!=null)
            {
                print("explosion");
                rigg.AddExplosionForce(expForce, transform.position, radius);
            }
        }
    }

  
}
