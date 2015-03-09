using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	void Update () {
	}

    public void OnScreenClick()
    {
        Event e = Event.current;
        if (e.isMouse)
        { 
            Debug.Log(e);
        }
    }
}
