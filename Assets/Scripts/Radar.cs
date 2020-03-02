using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Componente auxiliar que utiliza un Collider esférico a manera de radar
// para comprobar colisiones con otros elementos.
// Las comprobaciones y métodos son análogos al componente (script) de Sensores.
public class Radar : MonoBehaviour
{
    
    private bool cercaDePared;
    private bool sembrado;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Pared")){
            cercaDePared = true;
        }
        if(other.gameObject.CompareTag("Semilla")){
            
            sembrado = true;
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("Pared")){
            cercaDePared = true;
        }
        if(other.gameObject.CompareTag("Semilla")){
            sembrado = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Pared")){
            cercaDePared = false;
        }
        if(other.gameObject.CompareTag("Semilla")){
            sembrado = false;
        }
    }

    public bool CercaDePared(){
        return cercaDePared;
    }

    public bool Sembrado(){
        return sembrado;
    }

}
