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
      Initialize();
        StartRoutine();
    }

  void Initialize () {
    bar = transform.GetChild(0).GetComponent<Bar>();
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

  public Client SpawnClient () {
    if (!bar) Initialize();

    return SpawnClient(bar.GetRandomFreeSeat());
  }

    private Client SpawnClient(Seat seat)
    {
        seat.Occupy();
        return InstantiateClient(seat);
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
    public Client InstantiateClient(Seat destination)
    {
        Client client = (Instantiate(client_prefab, transform.position, Quaternion.identity) as GameObject).GetComponent<Client>();
        client.SpawnAndSetDestination(destination.position);
        client.OnClientStandUp += delegate() { ClientStandUp(destination);  };

        return client;
    }
    void Update()
    {
        
    }
}
