using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Free : MonoBehaviour, IPointerEnterHandler
{
    // Start is called before the first frame update
    [SerializeField] private bool isFree = true;
    [SerializeField] public int id;
    public Color color;
    private void Start() {
        color = GetComponent<MeshRenderer>().material.color;
    }

    public void setFree(bool b, GameObject gameObject)
    {
        //Debug.Log(gameObject.name + " set " + this.gameObject.tag + " " + b);
        isFree = b;
    }

    public bool getFree()
    {
        return isFree;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PuzzleController.Instance.setHoverFree(this);
    }
}
