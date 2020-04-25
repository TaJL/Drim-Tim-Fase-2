using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NonPersistantSingleton<T> : MonoBehaviour where T : MonoBehaviour {
  static T _instance;

  public static T Instance {
    get {
      if (!_instance) {
        _instance = GameObject.FindObjectOfType<T>();
      }

      return _instance;
    }
  }

  void Awake () {
    if (Instance != this) {
      Destroy(gameObject);
    }
  }
}
