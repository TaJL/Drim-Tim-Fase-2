using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text_mesh;
  public bool Zoommed { get => zoommed; }
    private bool zoommed = false;
    private Vector3 start_position;
    private Quaternion start_rotation;
  public AudioSource speaker;
  public AudioClip sfx;

    void Start()
    {
        text_mesh = GetComponentInChildren<TextMeshPro>();
        start_position = transform.position;
        start_rotation = transform.rotation;
    }

    public void Toggle()
    {
      speaker.PlayOneShot(sfx);
        zoommed = !zoommed;
        if (zoommed)
        {
            StartCoroutine(ZoomIn());
        }
        else
        {
            StartCoroutine(ZoomOut());
        }
    }
    
    private IEnumerator ZoomIn()
    {
        transform.SetParent(GameManager.camera_reference.transform);
        var start = transform.localPosition;
        var start_rot = transform.localRotation;
        float counter = 0.0f;
        while (counter < 1)
        {
            counter += Time.deltaTime / 0.25f;
            transform.localPosition = Vector3.Lerp(start, Vector3.zero + GameManager.camera_reference.transform.InverseTransformDirection(GameManager.camera_reference.transform.forward), counter);
            transform.localRotation = Quaternion.Lerp(start_rot,Quaternion.identity, counter);
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        transform.SetParent(null);
        var start = transform.position;
        var start_rot = transform.rotation;
        float counter = 0.0f;
        while (counter < 1)
        {
            counter += Time.deltaTime / 0.25f;
            transform.localPosition = Vector3.Lerp(start, start_position, counter);
            transform.localRotation = Quaternion.Lerp(start_rot,start_rotation, counter);
            yield return null;
        }
    }
}
