using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour {
  public ClientsManager clients;
  public Client tutorialClient;
  public TutorialConversationManager manager;
  public TutorialConversation conversation;

  void Awake () {
    clients.enabled = false;
  }

  IEnumerator Start () {
    yield return new WaitForSeconds(2);
    manager.Display(conversation);
    TutorialConversationManager.onConversationEnd += SendTutorialCustomer;
  }

  public void SendTutorialCustomer (TutorialConversation c) {
    TutorialConversationManager.onConversationEnd -= SendTutorialCustomer;
    tutorialClient = clients.SpawnClient();
    tutorialClient.waiting_time = 20;
  }
}
