using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hinter : MonoBehaviour
{
    private Interactable last_pointed;
    private TextMeshPro text_mesh;

    private bool is_ready = false;
    // Start is called before the first frame update
    void Start()
    {
        Events.OnRockolaPowered += Ready; 
        text_mesh = GetComponent<TextMeshPro>();
        text_mesh.text = "";
    }

    public void Ready()
    {
        is_ready = true;
        Events.OnRockolaPowered -= Ready;
    }
    // Update is called once per frame
    void Update()
    {
        if (!is_ready)
            return;
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("interactables")))
        {
            if (hit.collider.transform.parent != null)
            {
                Interactable interactable = hit.collider.transform.parent.GetComponent<Interactable>();
                if (last_pointed != interactable)
                {
                    last_pointed = interactable;
                    if (interactable != null)
                    {
                        if (interactable.target != null)
                        {
                            text_mesh.text = interactable.target.GetComponent<Bottle>().hint;
                        }
                    }
                }
            }
        }
        else if(last_pointed != null)
        {
            last_pointed = null;
            text_mesh.text = "";
        }
    }
}
