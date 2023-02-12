using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IManager
{
    public static GameManager instance;

    public List<IManager> allOtherManagers = new List<IManager>();
    public MonoBehaviour[] allMonoBehaviour;

    [HideInInspector] public StateManager stateManager;
    [HideInInspector] public GrpcManager grpcManager;
    [HideInInspector] public FullSwingManager fullSwingManager;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        stateManager = GetComponent<StateManager>();
        grpcManager = GetComponent<GrpcManager>();
        fullSwingManager = GetComponent<FullSwingManager>();
    }

    private void Start()
    {
        allMonoBehaviour = GetComponents<MonoBehaviour>();

        for (int i = 0; i < allMonoBehaviour.Length; i++)
        {
            if (allMonoBehaviour[i] is IManager && allMonoBehaviour[i] != this)
                allOtherManagers.Add(allMonoBehaviour[i] as IManager);
        }

        OnGameInitialize();


    }

    public void OnGameInitialize()
    {
        // choose state
        // start grpc conncetion
        // start full swing Conncetion
        foreach (IManager manager in allOtherManagers)
        {
            manager.OnGameInitialize();
        }

        // wait for those task to finished
        stateManager.currentGameMode = StateManager.GameModes.Game1Scene;
        StartCoroutine(WaitforInitializationComplete(OnGameChoose));
    }

    public async void OnGameChoose()
    {
        //Scene loader
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(stateManager.currentGameMode.ToString(), LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
            Debug.Log(" Loading percentage " + asyncLoad.progress);
        }

        // Start On Game Start from specific Game Scene
    }

    public void OnGameStart()
    {
        foreach (IManager manager in allOtherManagers)
        {
            manager.OnGameStart();
        }
    }


    private IEnumerator WaitforInitializationComplete(Action action)
    {
        while (!grpcManager.isGrpcReady || !fullSwingManager.isFullSwingReady)
            yield return null;

        action.Invoke();
    }


    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space))
        {
            stateManager.currentGameMode = StateManager.GameModes.Game1Scene;
            OnGameChoose();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            stateManager.currentGameMode = StateManager.GameModes.Game2Scene;
            OnGameChoose();

        }
    }
}
