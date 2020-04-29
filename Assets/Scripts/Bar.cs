using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bar : MonoBehaviour
{
    public UnityAction OnSeatFreed;
    private Seat[] seats;
    void Start()
    {
        seats = new Seat[transform.childCount];
        for (int i = 0; i < seats.Length; i++)
        {
          seats[i] = new Seat(transform.GetChild(i).position, i);
        }
    }
    public int OccipiedSeatsCount()
    {
        int x = 0;
        for (int i = 0; i < seats.Length; i++)
        {
            if (!seats[i].IsFree)
                x += 1;
        }
        return x;
    }
  public Seat GetSeatAt (int index) {
    return seats[index];
  }

    public Seat GetRandomFreeSeat()
    {
        var free_seats = new List<Seat>();
        foreach (var t in seats)
        {
            if (t.IsFree)
                free_seats.Add(t);
        }
        return free_seats.Count > 0? free_seats[Random.Range(0, free_seats.Count)]: null;
    }
    public Seat GetFreeSeat()
    {
        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i].IsFree)
                return seats[i];
        }
        return null;
    }
}
