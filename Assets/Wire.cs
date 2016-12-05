using UnityEngine;
using System.Collections;

public class Wire : MonoBehaviour {

    public static bool wireComplete = false;

    [SerializeField] private GameObject completeWire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other) {

        if (other.GetComponent<Tape>()) {

            GameObject.Destroy(other.gameObject);
        }

        completeWire.SetActive(true);

        Wire.wireComplete = true;

        this.gameObject.SetActive(false);

    }
}
