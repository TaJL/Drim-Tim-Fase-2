using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using  UnityEngine.Events;

public class Client : MonoBehaviour
{
  public static event System.Action<Client> onAnyClientEnded;
  public event System.Action onPleased;
  public event System.Action onDisappointed;
  public event System.Action onStartTalking;
  public event System.Action onStopTalking;

    public enum  ClientStates
    {
        WalkIn,
        Ordering,
        Waiting,
        Walkout
    }

  public bool wasOk;
    public UnityAction OnClientStandUp;
    public UnityAction<int> OnClientEnded;
    public UnityAction<Recipe> OnRequest;

    public float walkin_time = 3;
    public float ordering_time = 3;
  public float waiting_time = 22;
    public float walkout_time = 3;

    [Header("Animation")] 
    public AnimationCurve text_globe_curve;
    public float text_globe_spawn_time= 0.25f;
    private ClientStates current_state;
    private Vector3 origin;
    private Vector3 destination;

    public Recipe order;
  public Animator animator;


    private TextMeshPro text_globe;

    private Coroutine waiting_coroutine;
    private Coroutine text_globe_coroutine;
    private Coroutine shutup_routine;

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
        text_globe.text = string.Format("Dame un <color=#8888EE>{0}</color>, por favor.",order.NameOfDrink);
        if (OnRequest != null) OnRequest(order);
    }
    public void ShoutOrder()
    {
        if (current_state == ClientStates.Ordering || current_state == ClientStates.Waiting)
        {
            text_globe.enabled = true;
            text_globe.transform.rotation =
                Quaternion.Euler(90, text_globe.transform.eulerAngles.y, transform.eulerAngles.z);
            
            if(text_globe_coroutine != null) {
                StopCoroutine(text_globe_coroutine);
            }
            if (onStartTalking != null) onStartTalking();
            text_globe_coroutine = StartCoroutine(AnimateTextGlobe(Quaternion.identity));
            ShutUp();
        }
    }

    private IEnumerator AnimateTextGlobe(Quaternion goal_rotation)
    {
        var start_rotation = text_globe.transform.localRotation;
        float counter = 0;
        while (counter < 1)
        {
            counter += Time.deltaTime/text_globe_spawn_time;
            text_globe.transform.localRotation = Quaternion.LerpUnclamped(start_rotation,goal_rotation, text_globe_curve.Evaluate(counter));
            yield return null;
        }
    }
    private void ShutUp()
    {
        if(shutup_routine != null)
            StopCoroutine(shutup_routine);
        shutup_routine = StartCoroutine(ShutUpRoutine());
    }

    private IEnumerator ShutUpRoutine()
    {
        yield return new WaitForSeconds(ordering_time);
        yield return StartCoroutine(AnimateTextGlobe(Quaternion.Euler(90, 0, 0)));
        text_globe.enabled = false;
        if (onStopTalking != null) onStopTalking();
    }
    private IEnumerator StartRoutine()
    {
        animator.SetBool("is walking", true);
        yield return StartCoroutine(WalkInRoutine());
        animator.SetBool("is walking", false);
        GetComponent<LookAtPlayer>().enabled = true;
        yield return StartCoroutine(OrderRoutine());
        yield return waiting_coroutine = StartCoroutine(WaitingRoutine());

        yield return StartCoroutine(WalkOutRoutine());
        //CALCULATE SCORE HERE
        // print("client ended");
    }

    private void End()
    {
        if(OnClientEnded != null)
            OnClientEnded(14);//TESTING VALUE
        if (onAnyClientEnded != null) onAnyClientEnded(this);

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
        yield return null;
        //ShoutOrder();
        // print("Can I have a: "+order.NameOfDrink);
        //yield return new WaitForSeconds(ordering_time);
    }

    private IEnumerator WaitingRoutine()
    {
        current_state = ClientStates.Waiting;

        yield return new WaitForSeconds(waiting_time);
    }
    private void WalkOut()
    {
        if(waiting_coroutine!= null)
            StopCoroutine(waiting_coroutine);
        StartCoroutine(WalkOutRoutine());
    }
    private IEnumerator WalkOutRoutine()
    {
        current_state = ClientStates.Walkout;
        GetComponent<LookAtPlayer>().enabled = false;
        animator.SetBool("is walking", true);
        transform.forward = destination - origin;
        if (OnClientStandUp != null)
            OnClientStandUp();
        var counter = 0.0f;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime/walkout_time;
            transform.position = Vector3.Lerp(destination, origin, counter);
            yield return null;
        }
        End();
    }
    public void RateBeberage (bool wasOk, List<string> left_out = null )
    {
        Invoke("WalkOut",ordering_time);
        if (wasOk)
        {
          if (onPleased != null) onPleased();
            text_globe.text = string.Format("<color=green>¡Gracias, está muy rico!</color>");
            ShoutOrder();
        }
        else
        {
          if (onDisappointed != null) onDisappointed();
            if (left_out != null && left_out.Count > 0)
            {
                text_globe.text = string.Format("Hey, el {0} no lleva ", order.NameOfDrink);
                for (int i = 0; i < left_out.Count; i++)
                {
                    text_globe.text += string.Format("<color=red>{0}</color>",left_out[i]);
                    if (i < left_out.Count - 1)
                        text_globe.text += " ni ";
                }

            }else
                text_globe.text = "<color=red>¡Esto sabe horrible!</color>";
            
            ShoutOrder();
        }
        current_state = ClientStates.Walkout;
    }
}
