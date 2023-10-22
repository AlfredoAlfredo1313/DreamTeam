using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Interaction", menuName = "Dial", order = 0)]
public class Dialobject : ScriptableObject
{
    [SerializeField] public List<narrativeController.Dialogues> dialogues;
    [ContextMenu("Fix")]
    public void Fix()
    {
        var dialogues = this.dialogues;
        for (int j = 0; j < dialogues.Count; j++)
        {
            var jtem = dialogues[j];
            if(jtem.speaker.Equals("Kid"))
                jtem.speaker = "Doomsday Child";
            if(jtem.speaker.Equals("???") || jtem.speaker.Equals("Vampire"))
                jtem.speakerTag = "Lucerio";
            else if(jtem.speaker.Equals("Cat"))
                jtem.speakerTag = "Clara";
            else if(jtem.speaker.Equals(""))
            {
            }
            else
            {
                jtem.speakerTag = jtem.speaker;   
            }
            dialogues[j] = jtem;
            Debug.Log(jtem.speakerTag);
        }
    }

    //[ContextMenu("duplicate")]
    //public void duplicate()
    //{
    //    Dialobject copy = CreateInstance<Dialobject>();
    //    copy.dialogues = new List<narrativeController.Dialogues>(dialogues);
    //    AssetDatabase.CreateAsset(copy, "Assets/Resources/Portugues/" + this.name + ".asset");
    //}
}
