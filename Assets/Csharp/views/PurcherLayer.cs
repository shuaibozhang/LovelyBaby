using UnityEngine;
using System.Collections;

public class PurcherLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btnOnBuyTili()
    {
        PlatTools.buy("test", 100);
    }
}
