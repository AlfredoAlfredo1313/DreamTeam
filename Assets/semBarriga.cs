using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class semBarriga : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject gob;
    public void OnPointerClick(PointerEventData eventData)
    {
        callBoard();
        transform.parent.parent.gameObject.SetActive(false);
    }

    async void callBoard()
    {
        GameObject gob2 = Instantiate(gob);
        while(!PieceScript.solved) await Task.Yield();
        PieceScript.solved = false;
        Destroy(gob2);
        gameObject.SetActive(true);
    }
}
