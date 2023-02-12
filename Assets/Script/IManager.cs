

public interface IManager 
{
    void OnGameInitialize();
    void OnGameChoose();
    void OnGameStart();
    //void OnPreShot();
    //void OnShotTaken();
    //void OnShotEnd();
    //void OnShowResult();
    
}


public interface IStation
{
    void OnNoPlayerInStation();
    void OnGuestPlayerStation();
    void OnAcitvePlayerInStation();

}
