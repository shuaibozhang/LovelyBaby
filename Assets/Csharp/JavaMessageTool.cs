using UnityEngine;
using System.Collections;

public class JavaMessageTool : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void JavaMessage(string message)
    {
        Debug.Log("message from java: " + message);
        PlatTools.AndroidPopTos("message from java: " + message);
    }
}
