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
    private float timer = 0;

    // gear was being weird so we're locking it's local position
    private Vector3 initPos;

    // prevents events from constantly triggering
    private bool doOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.localPosition;
        hinge = GetComponent<HingeJoint>();
    }

    private IEnumerator StartTimer()
    {
       timer += Time.deltaTime;
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
            timer = 0;
            StartCoroutine("StartTimer");
        }

        if (timer > toggleLength)
        {
            StopCoroutine("StartTimer");
            toToggle.SetActive(false);
            maxHit = false;
            timer = 0;
            doOnce = true;
        }
    }
}