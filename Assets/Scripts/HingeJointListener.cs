using UnityEngine;
using UnityEngine.Events;

public class HingeJointListener : MonoBehaviour
{
    //angle threshold to trigger if we reached limit
    public float angleBetweenThreshold = 1f;
    //State of the hinge joint : either reached min or max or none if in between
    public HingeJointState hingeJointState = HingeJointState.None;



    public enum HingeJointState { Min, Max, None }
    private HingeJoint hinge;

    public GameObject toToggle;
    public float toggleLength = 10;

    public GameObject[] scenes;
    private int counter = 0;

    private bool maxHit = false;
    public float timer = 0;

    // gear was being weird so we're locking it's local position
    private Vector3 initPos;

    // prevents events from constantly triggering
    private bool doOnce = true;
    private bool timerOn = false;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.localPosition;
        hinge = GetComponent<HingeJoint>();
    }


    private void FixedUpdate()
    {
        transform.localPosition = initPos;
        float angleWithMinLimit = Mathf.Abs(hinge.angle - hinge.limits.min);
        float angleWithMaxLimit = Mathf.Abs(hinge.angle - hinge.limits.max);

        //Reached Min
        if (angleWithMinLimit < angleBetweenThreshold)
        {
            if (hingeJointState != HingeJointState.Min)
                maxHit = true;
            //OnMinLimitReached.Invoke();

            hingeJointState = HingeJointState.Min;
        }
        //Reached Max
        else if (angleWithMaxLimit < angleBetweenThreshold)
        {
            if (hingeJointState != HingeJointState.Max)
                //OnMaxLimitReached.Invoke();

            hingeJointState = HingeJointState.Max;
        }
        //No Limit reached
        else
        {
            hingeJointState = HingeJointState.None;
        }


        if (maxHit && doOnce)
        {
            toToggle.SetActive(true);
            scenes[counter].SetActive(false);
            counter += 1;
            counter %= scenes.Length;
            scenes[counter].SetActive(true);
            doOnce = false;
            timerOn = true;
            timer = 0;

        }

        if (timerOn)
        {
            timer += Time.deltaTime;
        }


        if (timer > toggleLength)
        {
            toToggle.SetActive(false);
            timerOn = false;
            maxHit = false;
            doOnce = true;
        }
    }
}