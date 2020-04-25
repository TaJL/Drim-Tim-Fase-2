using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.Events;

public class Client : MonoBehaviour
{
    public enum  ClientStates
    {
        WalkIn,
        Ordering,
        Waiting,
        Walkout
    }

    public UnityAction OnClientStandUp;
    public UnityAction<int> OnClientEnded;  

    public float walkin_time = 3;
    public float ordering_time = 3;
    public float waiting_time = 15;
    public float walkout_time = 3;

    private ClientStates current_state;
    private Vector3 origin;
    private Vector3 destination;


    public void SpawnAndSetDestination(Vector3 _destination)
    {
        origin = transform.position;
        destination = _destination;
        StartCoroutine(StartRoutine());
    }
    
    private IEnumerator StartRoutine()
    {
        yield return StartCoroutine(WalkInRoutine());
        yield return StartCoroutine(OrderRoutine());
        yield return StartCoroutine(WaitingRoutine());
        yield return StartCoroutine(WalkOutRoutine());
        //CALCULATE SCORE HERE
        print("client ended");
        if(OnClientEnded != null)
            OnClientEnded(14);//TESTING VALUE
        
        Destroy(this.gameObject);
    }
    private IEnumerator WalkInRoutine()
    {
        current_state = ClientStates.WalkIn;

        var counter = 0.0f;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime/walkin_time;
            transform.position = Vector3.Lerp(origin, destination, counter);
            yield return null;
        }
    }

    private IEnumerator OrderRoutine()
    {
        current_state = ClientStates.Ordering;

        yield return new WaitForSeconds(ordering_time);
    }

    private IEnumerator WaitingRoutine()
    {
        current_state = ClientStates.Waiting;

        yield return new WaitForSeconds(waiting_time);
    }
    
    private IEnumerator WalkOutRoutine()
    {
        current_state = ClientStates.Walkout;
        if (OnClientStandUp != null)
            OnClientStandUp();
        var counter = 0.0f;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime/walkout_time;
            transform.position = Vector3.Lerp(destination, origin, counter);
            yield return null;
        }
    }

    private int CalculateDrinkScore()
    {
        throw new NotImplementedException();
    }
}
