using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeManager : MonoBehaviour
{
    // global speed multiplier value that will trigger the camera mode change
    public float multiplierTrigger = 5;
    // Game objects to disable/enable when triggered
    public GameObject leftParticleHand;
    public GameObject leftHand;
    public GameObject rightParticleHand;
    public GameObject rightHand;
    public GameObject steeringWheel;
    public GameObject particleSteeringWheel;
    public GameObject particleCar;
    // Camera offset object containing the RandomPosLerp and CameraSwitchSync components
    public GameObject cameraOffset;

    private RandomPosLerp rpl;
    private CameraSwitchSync csc;
    private Steering_Wheel_Controller swc;

    // Start is called before the first frame update
    void Start()
    {
        rpl = cameraOffset.GetComponent<RandomPosLerp>();
        csc = cameraOffset.GetComponent<CameraSwitchSync>();
        //swc = steeringWheel.GetComponent<Steering_Wheel_Controller>();
    }

    void On()
    {
        steeringWheel.SetActive(false);
        leftHand.SetActive(false);
        rightHand.SetActive(false);

        //leftParticleHand.SetActive(true);
        //rightParticleHand.SetActive(true);
       //particleSteeringWheel.SetActive(true);
        //particleCar.SetActive(true);
        //rpl.enabled = true;
        //csc.enabled = true;
        //swc.turnOnVehicleCamTrack();
    }

    void Off()
    {
        steeringWheel.SetActive(true);
        steeringWheel.SetActive(true);
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        leftParticleHand.SetActive(false);
        rightParticleHand.SetActive(false);
        particleSteeringWheel.SetActive(false);
        particleCar.SetActive(false);
        rpl.enabled = false;
        csc.enabled = false;
        //swc.turnOffVehicleCamTrack();
        lerpCameraOffsetToZero(1);
    }

    void lerpCameraOffsetToZero(float camReturnSpeed)
    {
        while (cameraOffset.transform.position.magnitude < 0.1f) {
            cameraOffset.transform.position = Vector3.Lerp(cameraOffset.transform.position, Vector3.zero, Time.deltaTime * camReturnSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalSpeed.multiplier > multiplierTrigger)
        {
            On();
        }
        else
        {
            Off();
        }
    }
}
