using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum Mode{
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    Camera main;

    [SerializeField] private Mode mode;
    
    private void Awake() {
        main = Camera.main;
    }
    private void LateUpdate() {
        switch (mode){
            case Mode.LookAt: 
                transform.LookAt(main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCam = transform.position - main.transform.position;
                transform.LookAt(transform.position + dirFromCam);
                break;
            case Mode.CameraForward:
                transform.forward = main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -main.transform.forward;
                break;
            default:
                break;
        }
    }
}
