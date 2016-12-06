using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameTransition : MonoBehaviour {

    [SerializeField] AudioSource fightingSource;
    [SerializeField] AudioSource slapSound;

    public static GameTransition instance;

    public bool transitioning = false;

    [SerializeField] private Light dimLight;

    [SerializeField] private TextMesh[] textObjects;

    public SteamVR_TrackedObject trackedObj1;
    public SteamVR_TrackedObject trackedObj2;

    public float dimLevel = 0.35f;

    private string[] afterGameText = new string[]
    {
        "Default Text",
        "20 people per minute are abused by their partner in the U.S.",
        "1 in 3 women and 1 in 4 men are victims of physical violence.",
        "1 in 15 children are exposed to intimate partner violence yearly.",
        "More than 20,000 calls reach domestic violence hotlines daily.",
        "This experience has multiple endings. What else can you do?",
        "Press the touchpad to try again."

    };

    void Awake() {

        if (instance == null) {
            instance = this;
        }
        else {
            GameObject.Destroy(instance.gameObject);
            instance = this;
        }

    }

    // Use this for initialization
    void Start () {


	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("r")) {

            SceneManager.LoadScene("GameScene");


        }

        // If they failed and waiting out the argument.s

        if (fightingSource.time >= fightingSource.clip.length-1.0f)
        {
            Debug.Log("entered");

            fightingSource.spatialBlend = 0.0f;
            fightingSource.Stop();

            slapSound.Play();

            GameTransition.instance.Transition("You did nothing when overhearing domestic abuse.");

        }
    }

    public void Transition(string optionalFirstMessage = "You have completed this experience.") {


        if (!transitioning)
        {
            afterGameText[0] = optionalFirstMessage;

            transitioning = true;

            StartCoroutine(TransitionCoroutine());
        }

    }

    public IEnumerator TransitionCoroutine() {

        float fightVolumeStart = fightingSource.volume;
        float fightEnd = 0.0f;

        float lightStart = dimLight.intensity;


        for (float i = 0; i < 5.0f; i+=Time.deltaTime) {

            fightingSource.volume = Mathf.Lerp(fightVolumeStart, fightEnd, i / 5.0f);
            dimLight.intensity = Mathf.Lerp(lightStart, dimLevel, i / 5.0f);

            yield return null;

        }

        fightingSource.volume = 0.0f;
        dimLight.intensity = dimLevel;


        foreach (string str in afterGameText)
        {

            yield return StartCoroutine(SetAllTextMessages(str));


            yield return new WaitForSeconds(5.0f);
        }

        StartCoroutine(WaitForTriggerPress());

    }

    public IEnumerator FadeTextToMessage(string newMessage, TextMesh text) {

        Color startAlpha = text.color;
        Color endAlpha = startAlpha;
        endAlpha.a = 0.0f;

        for (float i = 0; i < 2.0f; i+=Time.deltaTime) {

            Color newText = Color.Lerp(startAlpha, endAlpha, i / 2.0f);
            text.color = newText;
            yield return null;

        }

        text.text = newMessage;

        for(float i = 0; i < 2.0f; i += Time.deltaTime) {

            Color newText = Color.Lerp(endAlpha, startAlpha, i / 2.0f);
            text.color = newText;

            yield return null;

        }

        text.color = startAlpha;
    }

    public IEnumerator SetAllTextMessages(string message) {

        for(int i = 0; i < textObjects.Length; i++) {

            if(i < textObjects.Length - 1) {
                StartCoroutine(FadeTextToMessage(message, textObjects[i]));
            }
            else {
                yield return StartCoroutine(FadeTextToMessage(message, textObjects[i]));
            }
        }
    }

    public IEnumerator WaitForTriggerPress() {

        while (true) {

            var device1 = SteamVR_Controller.Input((int)trackedObj1.index);
            var device2 = SteamVR_Controller.Input((int)trackedObj2.index);

            if(device1.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) ||
                device2.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {


                SceneManager.LoadScene("GameScene");

            }

            yield return null;

        }
    }
}
