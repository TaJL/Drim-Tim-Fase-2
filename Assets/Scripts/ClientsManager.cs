using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsManager : MonoBehaviour
{
  public static event System.Action<Client, int> onClientSpawned;

    public GameObject client_prefab;

    public float spawn_delay = 4f;

    [SerializeField] private float spawn_reduction = 0.5f;

    [SerializeField] private float spawn_delay_min = 10f;
    
    private bool is_spawning = false;

    private Bar bar;

    private Queue pending_clients = new Queue();
    void Start()
    {
      Initialize();
        StartRoutine();
    }

  void OnEnable () {
    GameManager.onGameOver += () => {
      this.enabled = false;
      bar.enabled = false;
      pending_clients = new Queue();
      StopAllCoroutines();
    };
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
            Seat random_seat = bar.GetRandomFreeSeat();
            if (random_seat != null)
            {
                SpawnClient(random_seat);
            }
            else
            {
                pending_clients.Enqueue(null);
            }
            yield return new WaitForSeconds(spawn_delay);
        }
    }



  public Client SpawnClient () {
    if (!bar) Initialize();

    return SpawnClient(bar.GetRandomFreeSeat());
  }

    private Client SpawnClient(Seat seat)
    {
        Client spawned = InstantiateClient(seat);
        seat.Occupy(spawned);

        if (onClientSpawned != null) onClientSpawned(spawned, seat.index);
        return spawned;
    }
    public void ClientStandUp(Seat seat)
    {
        seat.Free();
        if (bar.OccipiedSeatsCount() == 0)
        {
            Seat random_seat = bar.GetRandomFreeSeat();
            if (random_seat != null)
            {
                SpawnClient(random_seat);
            }
        }

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

    public void ReduceSpawnTime()
    {
      if (spawn_delay_min <= spawn_delay)
      {
        return;
      }

      spawn_delay -= spawn_reduction;

      if (spawn_delay < spawn_delay_min)
      {
        spawn_delay = spawn_delay_min;
      }
      
    }
}
