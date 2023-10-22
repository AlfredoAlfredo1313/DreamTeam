using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewDialogueController : MonoBehaviour
{
    StringBuilder text;
    StringBuilder sb;
    [SerializeField] public AudioClip talkingSound;
    [SerializeField] public GameObject textObject;
    [SerializeField] public bool quickRead;
    [SerializeField] GameObject parentCanvas;
    [SerializeField] List<AudioClip> clips;
    static Dictionary<string, AudioClip> charVoices;
    public DialogueBox db;
    public GameObject textInstance;
    narrativeController.Dialogues Dialogo;
    public bool completedSentence = true;
    public bool onGoingDialogue = false;
    public bool nextLine = false;
    public int dialogueIndex = 0;
    public bool isSkipable;
    Coroutine routine;
    Color currentColor = Color.yellow;

    public static NewDialogueController Instance;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            if(charVoices == null)
            {
                charVoices = new Dictionary<string, AudioClip>();
                charVoices.Add("Doomsday Child", clips[0]);
                charVoices.Add("Marcus", clips[1]);
                charVoices.Add("Eugene", clips[0]);
                charVoices.Add("Lucerio", clips[0]);
                charVoices.Add("Clara", clips[0]);
            }

        }
        else
        {
            Destroy(gameObject);
        }
        text = new StringBuilder();
        sb = new StringBuilder();
    }

    public void OnDialogueProceed(InputValue value)
    {
        if(completedSentence) return;
        if(onGoingDialogue)
        {
            if(Dialogo.isSkipable) return;
            StopCoroutine(routine);
            db.UpdateText(sb.ToString());
            onGoingDialogue = false;
        } else
        {
            nextLine = true;
        }
    }

    public IEnumerator nextSentence(narrativeController.Dialogues dialogue)
    {
        onGoingDialogue = true;
        Dialogo = dialogue;
        routine = StartCoroutine(TextConstructor(dialogue));
        yield return new WaitUntil(() => nextLine);
        completedSentence = true;
        nextLine = false;
    }

    public IEnumerator TextConstructor(narrativeController.Dialogues dialogue)
    {
        int alphaIndex = 1;
        string text = dialogue.text;
        completedSentence = false;
        if(dialogue.newBlock)
        {
            if(textInstance != null) Destroy(textInstance);
            textInstance = Instantiate(textObject, parentCanvas.transform);
            db = textInstance.GetComponent<DialogueBox>();
            db.UpdateDialogueBox(null, dialogue.speaker);
            if(!dialogue.color.Equals(new Color(0, 0, 0, 0))){
                currentColor = dialogue.color;
                textInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = dialogue.color + new Color(0, 0, 0, 1);
            } else {
                textInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = currentColor + new Color(0, 0, 0, 1);
            }
            string hideText = text;
            hideText = hideText.Insert(0, "<color=#00000000>");
            db.UpdateText(hideText);
            yield return new WaitForEndOfFrame();
        }
        else
        {
            text = text.Insert(0, sb.ToString());
            alphaIndex += sb.ToString().Length;
        }
        sb.Clear();
        sb.Append(text);
        string t = text;
        char[] c_array = dialogue.text.ToCharArray();
        StartCoroutine("playTalkingSound"); 
        for(int i = 0; i < c_array.Length; i++)
        {
            if(c_array[i] == '<') {alphaIndex += 3; i+=3; continue;}
            if(alphaIndex <= t.Length) t = t.Insert(alphaIndex, "<color=#00000000>");
            db.UpdateText(t);
            if(!quickRead) yield return new WaitForSeconds(dialogue.typingSpeed);
            t = text;
            alphaIndex++;
        }
        onGoingDialogue = false;
        if(SceneManager.GetActiveScene().name.Equals("World")) 
        StartCoroutine("ifKeyBoardFuckedShitPoo");
    }

    public void destroyBox()
    {
        if(this != Instance) {Instance.destroyBox(); return;}
        //Debug.Log("Destroying");
        Destroy(textInstance);
    }

    IEnumerator ifKeyBoardFuckedShitPoo()
    {
        Debug.Log("Going");
        yield return new WaitForSeconds(5);
        nextLine = true;
    }

    IEnumerator playTalkingSound()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = charVoices[Dialogo.speakerTag];
        while(onGoingDialogue)
        {
            source.Play();
            yield return new WaitForSeconds(Dialogo.typingSpeed * 3);
        }
    }
}
