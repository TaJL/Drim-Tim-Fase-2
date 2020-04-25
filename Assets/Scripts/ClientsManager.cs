using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsManager : MonoBehaviour
{
    public GameObject client_prefab;

    public float spawn_delay = 4f;
    
    private bool is_spawning = false;

    private Bar bar;

    private Queue pending_clients = new Queue();
    void Start()
    {
        bar = transform.GetChild(0).GetComponent<Bar>();
        StartRoutine();
    }

    public void StartRoutine()
    {
        is_spawning = true;
        StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        while (is_spawning)
        {
            yield return new WaitForSeconds(spawn_delay);
            Seat random_seat = bar.GetRandomFreeSeat();
            if (random_seat != null)
            {
                SpawnClient(random_seat);
            }
            else
            {
                pending_clients.Enqueue(null);
            }
        }
    }

    private void SpawnClient(Seat seat)
    {
        seat.Occupy();
        InstantiateClient(seat);
    }
    public void ClientStandUp(Seat seat)
    {
        seat.Free();
        if(pending_clients.Count > 0)
        {
            print("pending client spawned");
            pending_clients.Dequeue();
            SpawnClient(seat);
        }
    }
    public void InstantiateClient(Seat destination)
    {
        Client client = (Instantiate(client_prefab, transform.position, Quaternion.identity) as GameObject).GetComponent<Client>();
        client.SpawnAndSetDestination(destination.position);
        client.OnClientStandUp += delegate() { ClientStandUp(destination);  };
    }
    void Update()
    {
        
    }
}
