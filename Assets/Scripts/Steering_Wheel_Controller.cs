using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Steering_Wheel_Controller : MonoBehaviour
{
    // Unity's new XR Interaction Toolkit input handling. See instantiation in Start()
    [SerializeField]
    private XRNode leftNode = XRNode.LeftHand;
    private XRNode rightNode = XRNode.RightHand;
    private InputDevice leftController;
    private InputDevice rightController;
    private List<InputDevice> devices = new List<InputDevice>();

    private bool rightGripped = false;
    private bool leftGripped = false;


    public GameObject rightHand;
    private Transform rightHandOriginalParent;
    private bool rightHandOnWheel = false;
    public GameObject leftHand;
    private Transform leftHandOriginalParent;
    private bool leftHandOnWheel = false;

    public Transform[] snapPositions;
    public Transform directonalObject;

    public GameObject Vehicle;
    private Rigidbody VehicleRigidBody;

    // for debug viewing
    public float currentWheelRotation = 0;

    // how quick rotation updates
    private float turnDampening = 250;


    

    // Start is called before the first frame update
    void Start()
    {

        VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();

    }

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(leftNode, devices);
        if (devices.Count > 0)
        {
            leftController = devices[0];
            Debug.Log("LEFT CONNECTED");
        }
        InputDevices.GetDevicesAtXRNode(rightNode, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
            Debug.Log("RIGHT CONNECTED");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!leftController.isValid /*|| !rightController.isValid*/)
        {
            GetDevice();
        }

        OnTriggerStay();

        ReleaseHandsFromWheel();

        ConvertRotation();

        TurnVehicle();

        currentWheelRotation = -transform.rotation.eulerAngles.z;

    }

    private void ConvertRotation()
    {
        float initx = transform.eulerAngles.x;
        float inity = transform.eulerAngles.y;
        if(rightHandOnWheel == true && leftHandOnWheel == false)
        {
            Quaternion newRot = Quaternion.Euler(initx, inity, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            directonalObject.rotation = newRot;
            this.transform.parent = directonalObject;
        } 
        else if (rightHandOnWheel == false && leftHandOnWheel == true)
        {
            Quaternion newRot = Quaternion.Euler(initx, inity, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            this.transform.rotation = newRot;
            //this.transform.parent = directonalObject;
        } 
        else if (rightHandOnWheel == true && leftHandOnWheel == true)
        {
            Quaternion rightRot = Quaternion.Euler(initx, inity, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion leftRot = Quaternion.Euler(initx, inity, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion finalRot = Quaternion.Slerp(leftRot, rightRot, 1.0f / 2.0f);
            directonalObject.rotation = finalRot;
            this.transform.parent = directonalObject;
        }
    }

    private void TurnVehicle()
    {
        var turn = -transform.rotation.eulerAngles.z;
        if(turn < -350)
        {
            turn = turn + 360;
        }
        VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, turn, 0), Time.deltaTime * turnDampening));
    }

    private void ReleaseHandsFromWheel()
    {
        if (rightHandOnWheel && rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripped) && !rightGripped)
        {
            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHand.transform.rotation = rightHandOriginalParent.rotation;
            rightHand.transform.localScale = rightHandOriginalParent.localScale;
            rightHandOnWheel = false;
        }
        if (leftHandOnWheel && leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGripped) && !leftGripped)
        {
            leftHand.transform.parent = leftHandOriginalParent;
            leftHand.transform.position = leftHandOriginalParent.position;
            leftHand.transform.rotation = leftHandOriginalParent.rotation;
            leftHand.transform.localScale = leftHandOriginalParent.localScale;
            leftHandOnWheel = false;
        }
        if(!leftHandOnWheel && !leftHandOnWheel)
        {
            transform.parent = null;
        }
    }

    private void OnTriggerStay()
    {

        if (rightHandOnWheel == false && rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripped) && rightGripped)
        {
            if ((rightHand.transform.position - this.transform.position).magnitude < 5f)
            {
                PlaceHandOnWheel(ref rightHand, ref rightHandOriginalParent, ref rightHandOnWheel);
            }
        }
        if (leftHandOnWheel == false && leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGripped) && leftGripped)
        {
            if ((leftHand.transform.position - this.transform.position).magnitude < 1f)
            {
                PlaceHandOnWheel(ref leftHand, ref leftHandOriginalParent, ref leftHandOnWheel);
            }
        }

    }

    private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnWheel)
    {
        var shortestDistance = Vector3.Distance(snapPositions[0].position, hand.transform.position);
        var bestSnap = snapPositions[0];

        foreach (var snap in snapPositions)
        {
            if (snap.childCount == 0)
            {
                var distance = Vector3.Distance(snap.position, hand.transform.position);
                if(distance < shortestDistance)
                {
                    shortestDistance = distance;
                    bestSnap = snap;
                }
            }
        }
        originalParent = hand.transform.parent;

        hand.transform.parent = bestSnap.transform;
        hand.transform.position = bestSnap.transform.position;

        handOnWheel = true;
    }

}
