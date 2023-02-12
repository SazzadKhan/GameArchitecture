using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEntry(StateController controller);
    void OnUpdate(StateController controller);
    void OnExit(StateController controller);
}




public class StateController : MonoBehaviour
{
    public IState currentState;
    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    void Start()
    {
        ChangeState(idleState);
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEntry(this);
    }
    void Update()
    {
        currentState.OnUpdate(this);
    }
}

public class IdleState : IState
{
    public void OnEntry(StateController controller)
    {
        // This will be called when first entering the state
    }
    public void OnUpdate(StateController controller)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.ChangeState(controller.patrolState);
        }
    }
    public void OnExit(StateController controller)
    {
        // This will be called on leaving the state
    }
}

[System.Serializable]
public class PatrolState : IState
{
    [SerializeField] float patrolSpeed = 5;
    [SerializeField] int waypoint;
    [SerializeField] List<Transform> waypoints;
    Transform myTransform;
    RaycastHit hitInfo;
    public void OnEntry(StateController controller)
    {
        myTransform = controller.transform;
    }
    public void OnUpdate(StateController controller)
    {
        Patrol();
        if (LookForPlayer())
        {
            controller.chaseState.SetTarget(hitInfo.transform);
            controller.ChangeState(controller.chaseState);
        }
    }
    public void OnExit(StateController controller)
    {
        // This will be called when first entering the state
    }
    bool LookForPlayer()
    {
        if (Physics.Raycast(myTransform.position, myTransform.forward, out hitInfo, 3))
        {
            if (hitInfo.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
    void Patrol()
    {
        if (myTransform.position != waypoints[waypoint].position)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, waypoints[waypoint].position, patrolSpeed * Time.deltaTime);
        }
        else
        {
            waypoint++;
            if (waypoint >= waypoints.Count)
            {
                waypoint = 0;
            }
        }
    }
}

[System.Serializable]
public class ChaseState : IState
{
    [SerializeField] float chaseSpeed = 8;
    [SerializeField] float loseDistance = 3;
    [HideInInspector] Transform myTransform;
    [HideInInspector] public Transform target;
    public void OnEntry(StateController controller)
    {
        myTransform = controller.transform;
    }
    public void OnUpdate(StateController controller)
    {
        if (PlayerLost())
        {
            controller.ChangeState(controller.patrolState);
        }
        else
        {
            Chase();
        }
    }
    public void OnExit(StateController controller)
    {
        // "Must've been the wind"
    }
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
    void Chase()
    {
        myTransform.position = Vector3.MoveTowards(myTransform.position, target.position, chaseSpeed * Time.deltaTime);
    }
    bool PlayerLost()
    {
        if (!target)
        {
            return true;
        }
        if (Vector3.Distance(myTransform.position, target.position) > loseDistance)
        {
            return true;
        }
        return false;
    }
}
