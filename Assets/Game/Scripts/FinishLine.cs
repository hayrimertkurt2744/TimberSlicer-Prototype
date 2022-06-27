using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam1;
    [SerializeField]
    private CinemachineVirtualCamera vcam2;
    public Transform finishPoint;
    private Vector3 finishPointVectoral;
    public GameObject player;
    bool followingCam=true;
    private void Start()
    {
        finishPointVectoral = new Vector3(finishPoint.position.x, finishPoint.position.y, finishPoint.position.z);
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag=="Player")
        {
            followingCam = false;
            SwitchCamPriority();
            player.transform.DOMove(finishPointVectoral, 3).OnComplete(()=> 
            {
                print("finishSequence");
                GameManager.onWinEvent?.Invoke();
            });
            
        }
    }
    private void SwitchCamPriority()
    {
        if (followingCam==true)
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        else
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
            followingCam = !followingCam;
        }
    }
}
