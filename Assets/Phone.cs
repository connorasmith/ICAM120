using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

    [SerializeField] private AudioClip policeClip;
    [SerializeField] private AudioClip disconnectClip;


    public void OnTriggerEnter(Collider other) {


        bool isController = false;

        GameObject controller = other.gameObject;

        while(controller.transform.parent != null) {

            if(controller.GetComponent<SteamVR_TrackedObject>()) {

                isController = true;
            }

            controller = controller.transform.parent.gameObject;
        }

        if(isController) {

            if(Wire.wireComplete) {

                GetComponent<AudioSource>().clip = policeClip;
                GameTransition.instance.Transition();

            }
            else {

                GetComponent<AudioSource>().clip = disconnectClip;

            }

            GetComponent<AudioSource>().Play();


        }
    }
}
