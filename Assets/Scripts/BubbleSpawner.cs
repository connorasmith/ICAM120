using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour {

    public GameObject bubblePrefab;

    public static BubbleSpawner instance;

    void Awake() {

        if (instance == null) {

            instance = this;

        }

    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public GameObject makeBubble(string message, GameObject connectedObject) {
        //Create the bubble above the object.
        GameObject bubble = Instantiate(instance.bubblePrefab, connectedObject.transform.position, new Quaternion()) as GameObject;

        bubble.GetComponent<TextBubbleScript>().fullMessage = message;

        bubble.transform.SetParent(connectedObject.transform, true);
        bubble.transform.localPosition = new Vector3(0, 0.2f, 0);
        bubble.transform.localScale *= 0.25f;
        bubble.transform.SetParent(null);

        return bubble;
    }
}
