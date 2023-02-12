using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSwingManager : MonoBehaviour,IManager
{

    public bool isFullSwingReady;

    // get all full swing references

    #region GameManager Functions

    public void OnGameInitialize()
    {
        StartCoroutine(ConncetToFullSwing());
    }

    public void OnGameChoose()
    {

    }
    public void OnGameStart()
    {

    }

    #endregion

    private IEnumerator ConncetToFullSwing()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Simulator connected successfully");
        isFullSwingReady = true;
    }

    
}
