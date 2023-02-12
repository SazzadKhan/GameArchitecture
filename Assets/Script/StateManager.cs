using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour, IManager, IStation
{
    public enum GameModes
    {
        LobbyScene,
        Game1Scene,
        Game2Scene
    }

    public enum GameLoopStates
    {
        initializeState,
        GameChooseState,
        GameStartState,
        PreShotState,
        ShotTakenState,
        ShotEndState,
        ResultState
    }

    public enum StationStates
    {
        none,
        NoPlayerState,
        GuestPlayerState,
        KnownPlayerState,
    }

    public enum BetStates
    {
        none,
        ActiveBet,
        NoActiveBet
    }

    public GameModes currentGameMode;
    public GameLoopStates currentGameLoopState;
    public StationStates currentStationState;
    public BetStates currentBetState;

    #region GameManager Function

    public void OnGameInitialize()
    {
        currentGameMode = GameModes.LobbyScene;
        currentGameLoopState = GameLoopStates.initializeState;
        currentStationState = StationStates.none;
        currentBetState = BetStates.none;

        Debug.Log("Game Mode " + currentGameMode);
    }

    public void OnGameChoose()
    {
        currentGameMode = GameModes.LobbyScene;
        currentGameLoopState = GameLoopStates.GameChooseState;
        currentStationState = StationStates.none;
        currentBetState = BetStates.none;
    }

    public void OnGameStart()
    {
        currentGameLoopState = GameLoopStates.GameStartState;
        currentStationState = StationStates.none;
        currentBetState = BetStates.none;
    }

    public void OnNoPlayerInStation()
    { 
        currentStationState = StationStates.NoPlayerState;
    }

    public void OnGuestPlayerStation()
    {
        currentStationState = StationStates.GuestPlayerState;
        currentBetState = BetStates.none;
    }

    public void OnAcitvePlayerInStation()
    {
        currentStationState = StationStates.KnownPlayerState;
        currentBetState = BetStates.none;
    }

    #endregion

}
