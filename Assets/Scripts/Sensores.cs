using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* CLASE QUE MANEJA LOS SENSORES DE NUESTRO DRON
*/
public class Sensores : MonoBehaviour
{
    private Radar radar; // Componente auxiliar (script) para utilizar radar esférico
    private Rayo rayo; // Componente auxiliar (script) para utilizar rayo lineal
    private Bateria bateria; // Componente adicional (script) que representa la batería
    private Actuadores actuador; // Componente adicional (script) para obtener información de los ac

    public GameObject estacionDeCarga;

    private bool tocandoPared; // Bandera auxiliar para mantener el estado en caso de tocar pared
    private bool cercaPared; // Bandera auxiliar para mantener el estado en caso de estar cerca de una pared
    private bool frentePared; // Bandera auxiliar para retomar el estado en caso de estar frente a una pared
    public bool zonaDeSembrado; // Bandera auxiliar para mantener el estado en caso de estar en una zona de sembrado
    public bool semillaCentrada;
    private bool frenteAParedAbajo;
    // Asignaciones de componentes
    void Start(){
        radar = GameObject.Find("Radar").gameObject.GetComponent<Radar>();
        rayo = GameObject.Find("Rayo").gameObject.GetComponent<Rayo>();
        bateria = GameObject.Find("Bateria").gameObject.GetComponent<Bateria>();
        actuador = GetComponent<Actuadores>();
    }

    void Update(){
      cercaPared = radar.CercaDePared();
      frentePared = rayo.FrenteAPared();
      zonaDeSembrado = rayo.ZonaDeSembrado();
      frenteAParedAbajo = rayo.FrenteAParedAbajo();
    }

    // ========================================
    // Los siguientes métodos permiten la detección de eventos de colisión
    // que junto con etiquetas de los objetos permiten identificar los elementos
    // La mayoría de los métodos es para asignar banderas/variables de estado.

    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Pared")){
            tocandoPared = true;
        }
    }

    void OnCollisionStay(Collision other){
        if(other.gameObject.CompareTag("Pared")){
            tocandoPared = true;
        }
        if(other.gameObject.CompareTag("BaseDeCarga")){
            actuador.CargarBateria();
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.CompareTag("Pared")){
            tocandoPared = false;
        }
    }

    
    // ========================================
    // Los siguientes métodos definidos son públicos, la intención
    // es que serán usados por otro componente (Controlador)

    /*
    * FUNCION QUE NO DICE SI SE ESTA TOCANDO PARED
    */
    public bool TocandoPared(){
        return tocandoPared;
    }
    /*
    * FUNCION QUE NOS DICE SI HAY UNA PARED CERCA
    */
    public bool CercaDePared(){
        return radar.CercaDePared();
    }

    public bool FrenteAPared(){
        return rayo.FrenteAPared();
    }
    /*
    * FUNCIÓN QUE NOS DICE SI HAY UN PARED  DEBAJO
    */
    public bool FrenteAParedAbajo(){
        return rayo.FrenteAParedAbajo();
    }

    /*
    * METODO PARA SABER SI ES ZONA DE SEMBRADO
    */
    public bool Sembrar(){
        return rayo.ZonaDeSembrado();
    }

    public bool Sembrado(){
        return radar.Sembrado();
    }

    // MÉTODO QUE NOS DICE CUANDO ESTAMOS DEL LADO DERECHO DE UNA PARED.
    public bool FrenteAParedDerecha() {
        return rayo.FrenteAparedDerecha();
    }

    //MÉTODO QUE NOS DICE CUANDO TENEMOS UNA PARED DE LADO IZQUIERDO
    public bool FrenteAParedIzquierda() {
        return rayo.FrenteAParedIzquierda();
    }

    /*
    * METODO QUE NOS DA LA ZONA DE SEMBRADO
    */
    public bool ZonaDeSembrado(){
        return zonaDeSembrado;
    }

    /*
    * METODO QUE NO DA EL ESTADO DE LA BATERÍA
    */
    public float Bateria(){
        return bateria.NivelDeBateria();
    }

    // Algunos otros métodos auxiliares que pueden ser de apoyo

    /*
    * METODO QUE NOS DA NUESTR UBICACION 
    */
    public Vector3 Ubicacion(){
        return transform.position;
    }

    /*
    * METODO QUE COLOCA UNA ZONA DE SEMBRADO
    */
    public void SetZonaDeSembrado(bool value){
        zonaDeSembrado = value;
    }

}
