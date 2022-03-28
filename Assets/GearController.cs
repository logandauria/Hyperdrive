using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GearController : MonoBehaviour
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

    // to track the initial position of the hands when they grab the object
    private Vector3 initRightHandPos;
    private Vector3 initLeftHandPos;
    private Vector3 initHandPos;

    // INSPECTOR INPUT
    // hand objects
    public GameObject rightHand;
    public GameObject leftHand;
    // object transform array of grabbable positions
    public Transform[] snapPositions;
    public Transform directonalObject;

    private bool rightHandOnObject = false;
    private bool leftHandOnObject = false;
    private Transform leftHandOriginalParent;
    private Transform rightHandOriginalParent;

    public GameObject Vehicle;
    private Rigidbody VehicleRigidBody;

    // for debug viewing
    public float currentWheelRotation = 0;

    // how quick rotation updates
    private float turnDampening = 250;

    private Vector3 initPos;
    private Vector3 initRot;

    // Start is called before the first frame update
    void Start()
    {
        initPos = this.transform.position;
        initRot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
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

        currentWheelRotation = transform.rotation.eulerAngles.z;

    }

    private void ConvertRotation()
    {
        const float maxRot = -12; // max object rotation from original position
        const float maxMove = -0.15f; // z direction
        float move = 0;

        if (rightHandOnObject == true && leftHandOnObject == false)
        {
            move = (initHandPos.z - rightHand.transform.position.z) / maxMove;
            Debug.Log("Move " + move);
            this.transform.eulerAngles = new Vector3(initRot.x, initRot.y, initRot.z + (-12 * move));
            this.transform.position = new Vector3(initPos.x, initPos.y, initPos.z + move); 
        }
        else if (rightHandOnObject == false && leftHandOnObject == true)
        {
            move = (initHandPos.z - leftHand.transform.position.z) / maxMove;
            this.transform.eulerAngles = new Vector3(initRot.x, initRot.y, initRot.z + (-12 * move));
            this.transform.position = new Vector3(initPos.x, initPos.y, initPos.z + move);
        }
        if(move > 1)
        {
            ForceHandRelease();
            portal();
            // EVENT!
        }
    }

    private void portal()
    {
        Debug.Log("Portal executed");
    }

    private void ForceHandRelease()
    {
        rightHand.transform.parent = rightHandOriginalParent;
        rightHand.transform.position = rightHandOriginalParent.position;
        rightHand.transform.rotation = rightHandOriginalParent.rotation;
        rightHand.transform.localScale = rightHandOriginalParent.localScale;
        rightHandOnObject = false;
        leftHand.transform.parent = leftHandOriginalParent;
        leftHand.transform.position = leftHandOriginalParent.position;
        leftHand.transform.rotation = leftHandOriginalParent.rotation;
        leftHand.transform.localScale = leftHandOriginalParent.localScale;
        leftHandOnObject = false;
        transform.parent = null;
    }

    private void ReleaseHandsFromWheel()
    {
        if (rightHandOnObject && rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripped) && !rightGripped)
        {
            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHand.transform.rotation = rightHandOriginalParent.rotation;
            rightHand.transform.localScale = rightHandOriginalParent.localScale;
            rightHandOnObject = false;
        }
        if (leftHandOnObject && leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGripped) && !leftGripped)
        {
            leftHand.transform.parent = leftHandOriginalParent;
            leftHand.transform.position = leftHandOriginalParent.position;
            leftHand.transform.rotation = leftHandOriginalParent.rotation;
            leftHand.transform.localScale = leftHandOriginalParent.localScale;
            leftHandOnObject = false;
        }
        if (!leftHandOnObject && !leftHandOnObject)
        {
            transform.parent = null;
            this.transform.position = Vector3.Lerp(this.transform.position, initPos, Time.deltaTime);
            this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, initRot, Time.deltaTime);
        }
    }

    private void OnTriggerStay()
    {
        // the minimum magnitude of hand distance to object required
        float minDist = .4f;

        if (rightHandOnObject == false && rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripped) && rightGripped)
        {
            if ((rightHand.transform.position - this.transform.position).magnitude < minDist)
            {
                initRightHandPos = rightHand.transform.localPosition;
                PlaceHandOnObject(ref rightHand, ref rightHandOriginalParent, ref rightHandOnObject);
            }
        }
        if (leftHandOnObject == false && leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGripped) && leftGripped)
        {
            if ((leftHand.transform.position - this.transform.position).magnitude < minDist)
            {
                initLeftHandPos = rightHand.transform.localPosition;
                PlaceHandOnObject(ref leftHand, ref leftHandOriginalParent, ref leftHandOnObject);
            }
        }

    }

    private void PlaceHandOnObject(ref GameObject hand, ref Transform originalParent, ref bool handOnWheel)
    {

        var shortestDistance = Vector3.Distance(snapPositions[0].position, hand.transform.position);
        var bestSnap = snapPositions[0];

        foreach (var snap in snapPositions)
        {
            if (snap.childCount == 0)
            {
                var distance = Vector3.Distance(snap.position, hand.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    bestSnap = snap;
                }
            }
        }
        originalParent = hand.transform.parent;

        hand.transform.parent = bestSnap.transform;
        hand.transform.position = bestSnap.transform.position;
        initHandPos = hand.transform.position;
        //hand.transform.scale

        handOnWheel = true;
    }
}