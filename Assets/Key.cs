using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other) {

        if (other.GetComponent<DoorKnob>()) {

            GameTransition.instance.Transition();

        }


    }
}
