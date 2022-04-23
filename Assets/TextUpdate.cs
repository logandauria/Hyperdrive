using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdate : MonoBehaviour
{

    public TextMeshProUGUI curr;

    // Start is called before the first frame update
    void Start()
    {
        //curr = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        curr.text = (int)((GlobalSpeed.multiplier * 10) / 2) + " mph";
    }
}
