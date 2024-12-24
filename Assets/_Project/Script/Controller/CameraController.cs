using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Transform cameraMovePoint;
    public Vector3 cameraRecentPos;
    public GameObject mainCam;

    public void MoveCamera()
    {
        PlayerMovement.instance.isCameraMoving = true;
        Controller.instance.cameraController.mainCam.transform.GetComponent<CameraFollow>().enabled = false;
        Controller.instance.cameraController.cameraRecentPos = Controller.instance.cameraController.mainCam.transform.position;
        Controller.instance.cameraController.mainCam.transform.DOMove(Controller.instance.cameraController.cameraMovePoint.position, 0.7f).OnComplete(()=>
        {
            Invoke(nameof(MoveCamToOrignalPos), 2f);
        });
    }

    public void MoveCamToOrignalPos()
    {
        Controller.instance.cameraController.mainCam.transform.DOMove(Controller.instance.cameraController.cameraRecentPos, 0.7f).OnComplete(()=>
        {
            PlayerMovement.instance.isCameraMoving = false;
            Controller.instance.cameraController.mainCam.transform.GetComponent<CameraFollow>().enabled = true;
        });
    }
}
