using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPosLerp))]
public class CameraSwitchSync : AudioSyncer
{

    private RandomPosLerp changer;

    // Start is called before the first frame update
    void Start()
    {
        changer = GetComponent<RandomPosLerp>();
    }

    public override void OnBeat()
    {
        base.OnBeat();
        changer.ChangePos();
        Debug.Log("POS CHANGE1");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;
    }

}
