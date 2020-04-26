using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public UnityAction<Recipe> OnRequest;

    public float walkin_time = 3;
    public float ordering_time = 3;
    public float waiting_time = 15;
    public float walkout_time = 3;

    private ClientStates current_state;
    private Vector3 origin;
    private Vector3 destination;

    public Recipe order;
  public Animator animator;


    private TextMeshPro text_globe;

    private void Start()
    {
        text_globe = GetComponentInChildren<TextMeshPro>();
        text_globe.enabled = false;
    }

    public void SpawnAndSetDestination(Vector3 _destination)
    {
        origin = transform.position;
        destination = _destination;
        StartCoroutine(StartRoutine());
    }

    private void SelectDrink()
    {
        order = GameManager.GetRandomDrink();
        text_globe.text = string.Format("Can I have a: {0} please?",order.NameOfDrink);
        if (OnRequest != null) OnRequest(order);
    }
    public void ShoutOrder()
    {
        text_globe.enabled = true;
    }

    private void ShutUp()
    {
        text_globe.enabled = false;
    }
    private IEnumerator StartRoutine()
    {
        animator.SetBool("is walking", true);
        yield return StartCoroutine(WalkInRoutine());
        animator.SetBool("is walking", false);
        Vector3 forward = transform.position -
          PlayerInteracter.Instance.transform.position;
        forward.y = 0;
        transform.forward = forward;
        yield return StartCoroutine(OrderRoutine());
        yield return StartCoroutine(WaitingRoutine());
        animator.SetBool("is walking", true);
        transform.forward = destination - origin;
        yield return StartCoroutine(WalkOutRoutine());
        //CALCULATE SCORE HERE
        // print("client ended");
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
            transform.forward = origin - destination;
            yield return null;
        }
    }

    private IEnumerator OrderRoutine()
    {
        current_state = ClientStates.Ordering;
        SelectDrink();
        ShoutOrder();
        // print("Can I have a: "+order.NameOfDrink);
        yield return new WaitForSeconds(ordering_time);
        ShutUp();
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
