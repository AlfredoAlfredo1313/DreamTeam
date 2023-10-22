using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class narrativeController : MonoBehaviour
{
    public static narrativeController Instance;
    public bool eventFinish = false;
    public Dialobject dial;
    public List<Dialogues> dialogues;
    [SerializeField]public bool dontAutoStart;
    [SerializeField] public int skiptTo;
    [SerializeField] public string sceneName;
    public int dialogueIndex = 0;
    public bool isNarrating = false;
    // Start is called before the first frame update

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        if(!dontAutoStart)StartCoroutine(startNarrative());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startNarrativeForLoosers()
    {
        StartCoroutine("startNarrative");
    }

    public IEnumerator startNarrative()
    {
        if(this != Instance) {
            StartCoroutine(Instance.startNarrative());
            yield break;
        }
        isNarrating = true;
        //Debug.Log("FalandoMerda");
        dial = interactionLoader.Instance.getState(sceneName);
        dialogues = new List<Dialogues>(dial.dialogues);
        NewDialogueController dc = GetComponent<NewDialogueController>();
        for (int i = 0; i < dialogues.Count; i++)
        {
            dialogueIndex = i;
            var item = dialogues[i];
            if(item.Event.GetPersistentEventCount() > 0) {
                eventFinish = true;
                item.Event.Invoke();
                yield return new WaitForSeconds(item.timeUntilNextLine);
            }
            if(i != skiptTo && skiptTo > 0) continue;
            else skiptTo = 0;
            if(item.narrativeEvent != null) yield return StartCoroutine(item.narrativeEvent.narrativeEvent());
            //if(item.args.Count > 0) yield return StartCoroutine(Scene_Controller.Instance.getResourceGreaterThan(Int32.Parse(item.args[0]), (float)Double.Parse(item.args[1])));
            if(!item.text.Equals("")) {
                yield return StartCoroutine(dc.nextSentence(item));
            }
        }
        isNarrating = false;
    }

    public IEnumerator startNarrative(Dialobject dialobject)
    {
        if(this != Instance) {
            StartCoroutine(Instance.startNarrative());
            yield break;
        }
        isNarrating = true;
        //Debug.Log("FalandoMerda");
        dial = dialobject;
        dialogues = new List<Dialogues>(dial.dialogues);
        NewDialogueController dc = GetComponent<NewDialogueController>();
        for (int i = 0; i < dialogues.Count; i++)
        {
            dialogueIndex = i;
            var item = dialogues[i];
            if(item.Event.GetPersistentEventCount() > 0) {
                eventFinish = true;
                item.Event.Invoke();
                yield return new WaitForSeconds(item.timeUntilNextLine);
            }
            if(i != skiptTo && skiptTo > 0) continue;
            else skiptTo = 0;
            if(item.narrativeEvent != null) yield return StartCoroutine(item.narrativeEvent.narrativeEvent());
            //if(item.args.Count > 0) yield return StartCoroutine(Scene_Controller.Instance.getResourceGreaterThan(Int32.Parse(item.args[0]), (float)Double.Parse(item.args[1])));
            if(!item.text.Equals("")) {
                yield return StartCoroutine(dc.nextSentence(item));
            }
        }
        isNarrating = false;
    } 

    public void changeDialogue(int index, string dialogue)
    {
       Dialogues d = dialogues[index];
       d.setText(dialogue);
       dialogues[index] = d;
    }

    public void animationEnded()
    {
        eventFinish = false;
    }

    [System.Serializable]
    public struct Dialogues
    {
        [TextArea(10,20)] 
        public string text;
        public string speaker;
        public string speakerTag;
        public float pitchMod;
        public float timeUntilNextLine;
        public float typingSpeed;
        public bool newBlock;
        public Narrative narrativeEvent;
        public UnityEvent Event;
        public List<String> args;
        public Color color;
        public bool isSkipable;
        public void setText(string text)
        {
            this.text = text;
        }
    }
}