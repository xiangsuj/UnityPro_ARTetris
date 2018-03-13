using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class VBInputEvent : MonoBehaviour,IVirtualButtonEventHandler
{
    [HideInInspector]
    //0 1 2 3 null left right rotate
    public int whichButton = 0;

    private static VBInputEvent _instance;

    public static VBInputEvent Instance
    {
        get
        {
            return _instance;
            
        }
    }

    void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start ()
    {
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "Left":
                whichButton = 1;
                break;
            case "Right":
                whichButton = 2;
                break;
            case "Rotate":
                whichButton = 3;
                break;
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        whichButton = 0;
    }
}
