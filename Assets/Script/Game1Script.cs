using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Game1Script : MonoBehaviour,IManager
{
    private void OnEnable()
    {
        GameManager.instance.allOtherManagers.Add(GetComponent<MonoBehaviour>() as IManager);
        Debug.Log("List after added " + GameManager.instance.allOtherManagers.Count);
    }

    private void OnDisable()
    {
        GameManager.instance.allOtherManagers.RemoveAt(GameManager.instance.allOtherManagers.Count - 1);
        Debug.Log("List after removed  " + GameManager.instance.allOtherManagers.Count);
    }

    #region IManager Functions

    public void OnGameInitialize()
    {
        throw new System.NotImplementedException();
    }

    public void OnGameChoose()
    {
        throw new System.NotImplementedException();
    }

    public async void OnGameStart()
    {
        Debug.Log("Start Prepearing for shot");
        // full swing initialize
        // set tee and target and camera position
        await Task.Delay(5000);
        Debug.Log("Ready to take shot from game 1");
    }

    #endregion

    private void Start()
    {
        GameManager.instance.OnGameStart();
    }
}
