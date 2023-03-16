using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode {
        Normal,Inverted,LookAtCamera,LookAtCameraInverted
    }

    [SerializeField] Mode mode;

    void LateUpdate()
    {
        switch(mode)
        {
            case Mode.Normal :
                transform.forward = Camera.main.transform.position;
                break;
            case Mode.Inverted :
                transform.forward = -Camera.main.transform.position;
                break;
            case Mode.LookAtCamera :
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtCameraInverted :
                Vector3 directionFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(directionFromCamera);
                break;
        }
               
    }
}
