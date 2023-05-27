using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public Transform[] waypoint;
    private Vector3 origem, destino;
    float inicio, comprimento;
    int indice=0;
    // Start is called before the first frame update
    void Start()
    {
        origem = waypoint[0].position;
        destino = waypoint[1].position;
        inicio = Time.time;
        comprimento= Vector3.Distance(origem, destino);
    }

    // Update is called once per frame
    void Update()
    {
        float tempo = Time.time- inicio;
        float velocidade = (tempo/comprimento)*3;
        this.transform.position = Vector3.Lerp(origem,destino,velocidade);
        if(Vector3.Distance(this.transform.position,destino)==0){
            indice++;
            origem=destino;
            if(indice%waypoint.Length == 0){
                indice=0;
            }
            destino= waypoint[indice].position;
            comprimento= Vector3.Distance(origem,destino);
            inicio=Time.time;

        }

    }
}
