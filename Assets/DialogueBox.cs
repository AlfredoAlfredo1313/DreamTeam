using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    //Componentes do DialogueBox
    //[SerializeField] Image image;
    [SerializeField] TextMeshProUGUI txtName;
    [SerializeField] TextMeshProUGUI txtText;

    public void UpdateDialogueBox(Sprite sprite, string name)
    {
        //this.image.sprite = sprite;
        this.txtName.text = name.Equals("Kid")? "Doomsday Child" : name;
    }

    public void UpdateText(string text)
    {
        this.txtText.text = text;
    }
}
