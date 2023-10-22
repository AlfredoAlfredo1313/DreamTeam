using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PuzzleController Instance; 
    [SerializeField] public Vector3 pos1;
    [SerializeField] public float rotx1;
    [SerializeField] public Vector3 pos2;
    [SerializeField] public float rotx2;
    Free HoverFree;
    PieceScript ClickedPiece;
    Action enableHover;
    private void Awake() {
        if(Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public void setHoverFree(Free free)
    {
        HoverFree = free;
        if(!free.getFree()) return;
        ClickedPiece?.tryMove(free);
    }

    public void setPieceClick(PieceScript piece)
    {
        ClickedPiece = piece;
    }

    
}
