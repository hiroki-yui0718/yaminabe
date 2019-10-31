using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParamScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text msg = GameObject.Find("Text").GetComponent<Text>();
        string name = ButtonScript.getName();
        msg.text = "Hi! " + name + " Welcome!";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
