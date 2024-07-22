using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distancia = 3.0f;
    public Texture2D puntero;
    public GameObject TextDetect;
    GameObject ultimoReconocido = null;


    void Start()
    {
        mask = LayerMask.GetMask("RaycastDetect");
        TextDetect.SetActive(false);
    }


    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            Deselect();
            SelectedObject(hit.transform);

            if(hit.collider.tag == "Door")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<SystemDoor>().ChangeDoorState();
                }
            }

            //para visualizar el raycast como un punto rojo
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }

        else
        {
            Deselect();
        }
    }

    void SelectedObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
        ultimoReconocido = transform.gameObject;
    }

    void Deselect()
    {
        if(ultimoReconocido)
        {
            ultimoReconocido.GetComponent<Renderer>().material.color = Color.white;
            ultimoReconocido = null;
        }

    }

    void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
        GUI.DrawTexture(rect, puntero);

        if(ultimoReconocido)
        {
            TextDetect.SetActive(true);
        }

        else
        {
            TextDetect.SetActive(false);
        }
    }
}
