using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
public class PieceScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public static bool specialMove = false;
    public Color color;
    public Action<PieceScript> onClick;
    public Action<PieceScript> onLift;
    TweenBase tween;
    [SerializeField] private Free free = null;
    [SerializeField] int id;
    static List<PieceScript> pieceScripts = new List<PieceScript>();
    public static bool solved = false;
    [SerializeField] int freeId;
    private void Start() {
        Debug.Log("Born " + this.name);
        if(free != null) freeId = free.id;
        pieceScripts.Add(this);
        color = GetComponent<MeshRenderer>().material.color;
        PowerUpParent.Instance.addToSwitch(setPowerUp);
    }

    public void tryMove(Free freee)
    {
        Vector3 dir = (transform.position - freee.transform.position);
        if(dir.x != 0 && (int)dir.y != 0) return;
        Debug.Log(dir + " " + ((int)dir.y != 0));
        Debug.DrawRay(transform.position, -dir, Color.green, 5);
        Physics.Raycast(new Ray(transform.position, -dir), out RaycastHit hitInfo, 20, 128);
        if(!hitInfo.transform.GetComponent<Free>().getFree()) return;
        tween = Tween.Position(transform, freee.transform.position, .1f, 0f);
        setSlotOwner(freee);
    }

    public void setSlotOwner(Free freee)
    {
        if(free != null) 
        {
            //Debug.Log(hit.collider.gameObject.tag);
            free.gameObject.GetComponent<MeshRenderer>().material.color = free.color;
            free.setFree(true, gameObject);
        }
        free = freee;
        free.setFree(false, gameObject);
        freeId = free.id;
        foreach (var item in pieceScripts)
        {
            if(item.id != item.freeId) return;
        }
        solved = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PuzzleController.Instance.setPieceClick(this);   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PuzzleController.Instance.setPieceClick(null);
    }


    public Free GetFree()
    {
        return this.free;
    }

    public void setPowerUp(Action<PieceScript> action)
    {
        Debug.Log("switched");
        onClick = action;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // Debug.Log("Clicked");
       onClick?.Invoke(this);
    }

    public void voidd(PieceScript pieceScript)
    {

    }
}
