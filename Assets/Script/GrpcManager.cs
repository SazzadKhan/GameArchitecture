using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GrpcManager : MonoBehaviour, IManager
{
    public bool isGrpcReady;



    private string BackOfficeApiLink = "52.203.236.235:9000"; // staging staging (New stage Insecure link)

    #region GameManager Functions

    public void OnGameInitialize()
    {
        BackOfficeGrpcConnection_Start();
    }

    public void OnGameChoose()
    {

    }

    public void OnGameStart()
    {

    }

    #endregion


    private async void BackOfficeGrpcConnection_Start()
    {
        await Task.Delay(5000);
        Debug.Log("Grpc connected successfully");
        isGrpcReady = true;

    }

    private async void GetStationIDAsync()
    {

    }

    private async void SetDevUserAsync() // 3 sec delay
    {

    }

    private async void GetHitIdAsync() // 3 sec delay
    {

    }

    private async void HitSingleAsync() // 3 sec delay
    {

    }

    private async void StartStreamingStationState()
    {
        // 
    }

    private async void GetGameData()
    {
        //
    }

    private async void GetBetNoToken()
    {
    }

}