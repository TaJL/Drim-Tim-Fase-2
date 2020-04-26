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

  public int index;
  public Client client;
    public State current_state = State.Free;
    public Vector3 position;

  public Seat(Vector3 pos, int index)
    {
        this.position = pos;
        this.index = index;
    }

    public bool IsFree
    {
        get
        {
            return current_state == State.Free;
        }
    }
    public void Occupy(Client client)
    {
        current_state = State.Ocuppied;
        this.client = client;
    }

    public void Free()
    {
        current_state = State.Free;
        this.client = null;
    }
}
