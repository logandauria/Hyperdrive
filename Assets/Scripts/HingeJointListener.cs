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

    private bool maxHit = false;
    private float timer = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    private void FixedUpdate()
    {
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
        if (maxHit)
        {
            toToggle.SetActive(true);
            timer += Time.deltaTime;
            if(timer > toggleLength)
            {
                toToggle.SetActive(false);
                maxHit = false;
                timer = 0;
            }
        }
    }
}