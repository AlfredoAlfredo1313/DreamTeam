using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Narrative : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract IEnumerator narrativeEvent();

    public static IEnumerator resourceGreaterThan(int index, float val)
    {
        //yield return new WaitUntil(() => Scene_Controller.Instance.resources[index] > val);
        yield break;
    }
}
