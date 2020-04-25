using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat
{
    public enum State
    {
        Free,
        Ocuppied
    }

    public State current_state = State.Free;
    public Vector3 position;

    public Seat(Vector3 pos)
    {
        this.position = pos;
    }

    public bool IsFree
    {
        get
        {
            return current_state == State.Free;
        }
    }
    public void Occupy()
    {
        current_state = State.Ocuppied;
    }

    public void Free()
    {
        current_state = State.Free;
    }
}
