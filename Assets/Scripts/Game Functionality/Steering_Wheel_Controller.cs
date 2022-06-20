using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Track the interaction & rotation of the steering wheel to turn and move the vehicle
public class Steering_Wheel_Controller : MonoBehaviour
{
    // Unity's new XR Interaction Toolkit input handling. See instantiation in Start()
    private XRNode leftNode = XRNode.LeftHand;
    private XRNode rightNode = XRNode.RightHand;
    private InputDevice leftController;
    private InputDevice rightController;
    private List<InputDevice> devices = new List<InputDevice>();

    // Keep track of hands
    private bool rightGripped = false;
    private bool leftGripped = false;
    private Transform rightHandOriginalParent;
    private bool rightHandOnWheel = false;
    private Transform leftHandOriginalParent;
    private bool leftHandOnWheel = false;

    private Rigidbody VehicleRigidBody;

    // store inits for later use
    private Vector3 initPos;
    private Vector3 initRot;
    private Vector3 initRightHandPos;
    private Vector3 initLeftHandPos;
    private Vector3 initCarPos;

    private Vector3 curCarPos;

    // INSPECTOR VALUES
    // makes the turning of the wheel feel heavier
    public float turnDamper = 2f;
    // reduces the movement made by the vehicle when the wheel is turned
    public float moveDamper = 400f;
    // hand objects
    public GameObject rightHand;
    public GameObject leftHand;
    // positions on the wheel for hands to snap to
    public Transform[] snapPositions;
    // vehicle (should be XR Origin) to rotate and move
    public GameObject Vehicle;
    // Game objects that represent the bounds of the driving area
    public GameObject XLimit1;
    public GameObject XLimit2;
    // pushable button object
    public GameObject buttonPush;

    private bool turnVehicleCam = false;

    // track last hand positions
    private Vector3 lastLeft;
    private Vector3 lastRight;
    // for debug viewing
    public float currentWheelRotation = 0;
    [SerializeField]
    public float observeMove;

    // Start is called before the first frame update
    void Start()
    {
        // store inits
        initPos = this.transform.position;
        initRot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        initCarPos = Vehicle.transform.localPosition;
        //Physics.IgnoreCollision(this.GetComponent<Rigidbody>(), rightHand.GetComponentInParent<BoxCollider>(), true);
        //Physics.IgnoreCollision(this.GetComponent<Rigidbody>(), leftHand.GetComponentInParent<BoxCollider>(), true);
        //List<Collider> snapColliders = this.transform.GetChild(1).gameObject.GetComponentsInChildren <Collider>()

        // ignore collisions between snaps and hands to prevent physics bug while handling the wheel
        foreach (Collider snap in this.transform.GetChild(1).gameObject.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(snap, rightHand.GetComponentInParent<Collider>(), true);
            Physics.IgnoreCollision(snap, leftHand.GetComponentInParent<Collider>(), true);
            Physics.IgnoreCollision(snap, buttonPush.GetComponent<Collider>(), true);
        }

        //Physics.IgnoreCollision(leftHand.GetComponent<Collider>(), rightHand.GetComponent<Collider>(), true);
        //Physics.IgnoreCollision(rightHand.GetComponent<Collider>(), leftHand.GetComponent<Collider>(), true);

    }

    /// <summary>
    /// Retrieve controller devices
    /// </summary>
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
        // retrieve controllers if either are invalid
        if (!leftController.isValid || !rightController.isValid)
        {
            GetDevice();
        }

        OnTriggerStay();

        ReleaseHandsFromWheel();

        //ConvertRotation();

        TurnVehicle();

        MoveVehicle();

        currentWheelRotation = -transform.rotation.eulerAngles.z;

        curCarPos = Vehicle.transform.localPosition;
    }

    /// <summary>
    /// move the vehicle sideways depending on rotation and the boundary limits
    /// </summary>
    private void MoveVehicle()
    {
        //Debug.Log("angle:" + angle);
        // get vehicles y rotation
        // 200 is speed dampen


        float move = Vehicle.transform.eulerAngles.y;
        if(move > 180)
        {
            move = move - 360;
        }
        move /= moveDamper;

        if(move > 0 && Mathf.Abs(Vehicle.transform.position.x - XLimit1.transform.position.x) < 1)
        {
            move = 0;
        }
        if(move < 0 && Mathf.Abs(Vehicle.transform.position.x - XLimit2.transform.position.x) < 1)
        {
            move = 0;
        }
        //if (Vehicle.transform.position.x < threshold) move *= -1;
        //if (this.transform.localEulerAngles.z > 0) move = -move;
        observeMove = move;
        // move vehicle smoothly
        //Vehicle.transform.position = new Vector3(Vehicle.transform.position.x + move, Vehicle.transform.position.y, );

        // move environment
        GameObject[] environmentObjs = GameObject.FindGameObjectsWithTag("env");
        foreach(GameObject g in environmentObjs)
        {
            g.transform.position = new Vector3(g.transform.position.x - move, g.transform.position.y, g.transform.position.z);

        }
        //Vehicle.transform.position = Vector3.Lerp(Vehicle.transform.position, new Vector3(Vehicle.transform.position.x + move, Vehicle.transform.position.y, Vehicle.transform.position.z), Time.deltaTime);
    }

    /// <summary>
    /// update vehicle's y rotation based on the steering wheel's x rotation
    /// </summary>
    private void TurnVehicle()
    {
        // get steering wheel x rotation
        var turn = -transform.transform.localEulerAngles.z;
        //turn /= turnDamper;

        if (turn < -350)
        {
            turn = turn + 360;
        }
        Vehicle.transform.eulerAngles = new Vector3(Vehicle.transform.eulerAngles.x, turn, Vehicle.transform.eulerAngles.z);
        //VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, turn, 0), Time.deltaTime * turnDampening));
    }

    /// <summary>
    /// Detect if controllers are no longer gripping, if so, release hands from wheel and return them to their original state
    /// </summary>
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
            //transform.parent = null;
        }
    }


    /// <summary>
    /// Read controller grip trigger input and check if its close enough to object
    /// </summary>
    private void OnTriggerStay()
    {
        // min distance from object the hand has to be
        float minGrabDist = .4f;

        if (rightHandOnWheel == false && rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripped) && rightGripped)
        {
            if ((rightHand.transform.position - this.transform.position).magnitude < minGrabDist)
            {
                PlaceHandOnWheel(ref rightHand, ref rightHandOriginalParent, ref rightHandOnWheel);
                lastRight = rightHand.transform.position;
            }
        }
        if (leftHandOnWheel == false && leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGripped) && leftGripped)
        {
            if ((leftHand.transform.position - this.transform.position).magnitude < minGrabDist)
            {
                PlaceHandOnWheel(ref leftHand, ref leftHandOriginalParent, ref leftHandOnWheel);
                lastLeft = leftHand.transform.position;
            }
        }

    }

    /// <summary>
    /// Place the hand on the object by finding the nearest snap position. Used for both hands by having reference parameters
    /// </summary>
    /// <param name="hand">Reference to the hand object that was used to grab</param>
    /// <param name="originalParent">Reference to the original parent of the hand</param>
    /// <param name="handOnObject">Reference to the bool that tracks if the hand is on the object</param>
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

        //hand.transform.parent = bestSnap.transform;
        hand.transform.position = bestSnap.transform.position;

        //hand.transform.parent = bestSnap.transform;
        //hand.transform.scale
        //initRightHandPos = rightHand.transform.position;

        handOnWheel = true;
    }

    public void turnOffVehicleCamTrack()
    {
        turnVehicleCam = false;
    }

    public void turnOnVehicleCamTrack()
    {
        turnVehicleCam = true;
    }
}