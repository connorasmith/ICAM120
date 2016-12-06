using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

    [SerializeField] private AudioClip policeClip;
    [SerializeField] private AudioClip disconnectClip;

    public bool wireDone = false;

    private bool done = false;

    public void OnTriggerEnter(Collider other) {

        if (isController(other.gameObject)) {

            if (!GetComponent<AudioSource>().isPlaying && !done)
            {

                Debug.Log("Playing");

                if (Wire.wireComplete)
                {

                    GetComponent<AudioSource>().clip = policeClip;
                    GetComponent<AudioSource>().loop = false;
                    GetComponent<AudioSource>().volume = 0.5f;

                    GetComponent<AudioSource>().Play();

                    done = true;

                    GameTransition.instance.Transition("You chose to call the police, good job");

                }

                else
                {

                    GetComponent<AudioSource>().clip = disconnectClip;
                    GetComponent<AudioSource>().loop = true;
                    GetComponent<AudioSource>().volume = 0.13f;
                    GetComponent<AudioSource>().Play();

                }
            }
        }
    }

    public void Update()
    {

        if (Wire.wireComplete && !wireDone)
        {

            GetComponent<AudioSource>().Stop();
            wireDone = true;

        }

    }

    public void OnTriggerExit(Collider other)
    {

        if (isController(other.gameObject))
        {

            Debug.Log("Deactivated");

        }
    }


    public bool isController(GameObject other)
    {
        GameObject controller = other;

        while (controller.transform.parent != null)
        {

            if (controller.GetComponent<SteamVR_TrackedObject>())
            {

                return true;
            }

            controller = controller.transform.parent.gameObject;
        }

        return false;
    }
}
