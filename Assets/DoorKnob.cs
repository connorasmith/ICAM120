using UnityEngine;
using System.Collections;

public class DoorKnob : MonoBehaviour {

    [SerializeField] private TextMesh doorText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");

        if (other.GetComponent<Key>())
        {
            Debug.Log("Key entered");

            doorText.text = "Open";
            doorText.color = Color.green;
            GameObject.Destroy(other);
            GameTransition.instance.Transition("You chose to intervene, good job.");

        }
    }
}
