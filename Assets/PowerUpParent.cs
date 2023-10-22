using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerUpParent : MonoBehaviour, IPointerClickHandler
{
    protected Action<Action<PieceScript>> switchAction;
    public static PowerUpParent Instance;
    public Color color;
    PieceScript piece1;
    PieceScript piece2;
    private void Awake() {
        if(Instance != null)
            Destroy(this);
        else
            Instance = this;
            color = GetComponent<MeshRenderer>().material.color;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        GetComponent<MeshRenderer>().material.color = Color.cyan;
        switchAction.Invoke(PowerMethod);
    }

    public void addToSwitch(Action<Action<PieceScript>> action)
    {
        switchAction += action; 
        Debug.Log("Registered " + switchAction?.GetInvocationList().Length);
    }

    public void PowerMethod(PieceScript pieceScript)
    {
        Debug.Log("PowerUpBassCannon");
        if(piece1 == null)
        {
            piece1 = pieceScript;
            piece1.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            return;
        }
        else if(piece2 == null)
        {
            if(piece1.Equals(pieceScript))
            {
                piece1.gameObject.GetComponent<MeshRenderer>().material.color = piece1.color;
                piece1 = null;
                return;
            }
            piece2 = pieceScript;
            piece2.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            Vector3 piece1Pos = piece1.gameObject.transform.position;
            Vector3 piece2Pos = piece2.gameObject.transform.position; 
            Free free1 = piece1.GetFree();
            Free free2 = piece2.GetFree();
            piece1.gameObject.GetComponent<MeshRenderer>().material.color = piece1.color;
            piece2.gameObject.GetComponent<MeshRenderer>().material.color = piece2.color;
            piece1.transform.position = piece2Pos;
            piece2.transform.position = piece1Pos;
            piece1.setSlotOwner(free2);
            piece2.setSlotOwner(free1);
            piece1 = null;
            piece2 = null;
            switchAction.Invoke(null);


        }
    }
}
