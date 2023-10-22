using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class interactionLoader : MonoBehaviour
{
    public static interactionLoader Instance;
    string Language = "Ingles/";
    Dictionary<string, int> stateMap = new Dictionary<string, int>();
    [SerializeField] public bool hasLeftFirstTrigger = false;
    int itensAcquired = 0;
    private void Awake() {
        if(Instance != null) Destroy(this);
        else{
            Instance = this;
            DontDestroyOnLoad(this);
            stateMap.Add("Vamp", 0);
            stateMap.Add("Mino", 0);
            stateMap.Add("Cat", 0);
            stateMap.Add("Home", 0);
            stateMap.Add("World", 0);
        }
    }

    public Dialobject getState(string scene)
    {
        string path = Language + scene + stateMap[scene].ToString();
        Debug.Log(path);
        return Resources.Load<Dialobject>(path);
    }

    public void updateState(string scene)
    {
        if(Instance != this)
        {
            Instance.updateState(scene);
            return;
        }
        Debug.Log(scene);
        stateMap[scene]++;
        Debug.Log("UPDATED " + scene + stateMap[scene].ToString());
    }

    public bool hasAllItems()
    {
        Debug.Log("Has all itens = " + ((stateMap["Vamp"] > 1) && (stateMap["Cat"] > 0) && (stateMap["Mino"] > 0)));
        if(Instance != this)
        {
            return Instance.hasAllItems();
        }
        return itensAcquired > 0 || (stateMap["Vamp"] > 1) && (stateMap["Cat"] > 0) && (stateMap["Mino"] > 0);
    }

    [ContextMenu("setState")]
    public void setState()
    {
        itensAcquired = 1;
    }

}
