using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBubbleScript : MonoBehaviour
{

    public GameObject[] bubbleSprites;
    //public GameObject raycastSource;

    public float cloudShiftTime = .1f;
    public float textSpeed = .1f;

    private float timeSinceLastCharacter = 0f;
    private float timeSinceLastCloud = 0f;

    private int activeCloudIndex = 0;

    public float timeAfterDoneToDestroy = 100f;

    //Message variables
    public string fullMessage;
    public string messageSoFar;

    private int currentStringLength = 0;

    public Text textBox;

    public Animator animator;

    private float bubbleOffset = 10;

    // Use this for initialization
    void Start()
    {
        fullMessage = fullMessage.Replace("\\n", "\n");
        animator = GetComponent<Animator>();

        //Temporarily setting the message in order to get the preferred height.
        textBox.text = fullMessage;

        float textHeight = textBox.preferredHeight;

        //Then update the images and the text itself.
        //Images

        //Stores the new size for UI elements.
        Vector2 newRectSize;

        for (int i = 0; i < bubbleSprites.Length; i++)
        {
            RectTransform messageRect = bubbleSprites[i].GetComponent<RectTransform>();

            newRectSize = messageRect.sizeDelta;
            newRectSize.y = textHeight + bubbleOffset;

            messageRect.sizeDelta = new Vector2(newRectSize.x, newRectSize.y);
        }


        //Text
        newRectSize = textBox.rectTransform.sizeDelta;
        newRectSize.y = textHeight;

        textBox.rectTransform.sizeDelta = new Vector2(newRectSize.x, newRectSize.y);

        textBox.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);

        timeSinceLastCharacter -= Time.deltaTime;
        timeSinceLastCloud -= Time.deltaTime;

        //Shift to the next cloud in the array.
        if (timeSinceLastCloud < 0f)
        {
            //Hide old cloud.
            timeSinceLastCloud = cloudShiftTime;
            bubbleSprites[activeCloudIndex].SetActive(false);

            //Show new cloud.
            activeCloudIndex = (activeCloudIndex + 1) % (bubbleSprites.Length);
            bubbleSprites[activeCloudIndex].SetActive(true);
            //bubbleSprites[activeCloudIndex].transform.Rotate(0, 0, 10f);
        }

        //Add new character if time has elapsed and there are characters left.
        if (timeSinceLastCharacter < 0f && ((currentStringLength) != fullMessage.Length))
        {
            timeSinceLastCharacter = textSpeed;
            messageSoFar = fullMessage.Substring(0, ++currentStringLength);
            textBox.text = messageSoFar;

            //The last one. Destroy in a bit.
            if ((currentStringLength) == fullMessage.Length)
            {
                animator.SetBool("Ending", true);
                Destroy(this.gameObject, timeAfterDoneToDestroy);
            }
        }
    }

    /// <summary>
    /// Animate the bubble's destruction.
    /// </summary>
    public void destroy()
    {
        Destroy(this.gameObject);
    }

}
