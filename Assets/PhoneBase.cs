using UnityEngine;
using System.Collections;

public class PhoneBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionStay(Collision other)
    {
        Debug.Log("Collide");

        if (other.collider.GetComponent<Phone>())
        {
            Debug.Log("Stopping");
            other.collider.GetComponent<AudioSource>().Stop();

        }
    }
}
