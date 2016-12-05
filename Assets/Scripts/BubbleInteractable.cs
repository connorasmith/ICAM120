using UnityEngine;
using System.Collections;

public class BubbleInteractable : MonoBehaviour {

    [SerializeField] private string bubbleMessage;

    public void OnTriggerEnter(Collider other) {

        bool isController = false;

        GameObject controller = other.gameObject;

        while (controller.transform.parent != null) {

            if (controller.GetComponent<SteamVR_TrackedObject>()) {

                isController = true;
            }

            controller = controller.transform.parent.gameObject;
        }

        if(isController) {

            ShowBubble();

        }
    }

    public void ShowBubble() {

        BubbleSpawner.instance.makeBubble(bubbleMessage, this.gameObject);

    }
}
