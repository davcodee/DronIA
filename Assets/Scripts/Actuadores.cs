using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actuadores : MonoBehaviour
{
    private Rigidbody rb; // Componente para simular acciones físicas realistas
    private Bateria bateria; // Componente adicional (script) que representa la batería
    private Sensores sensor; // Componente adicional (script) para obtener información de los sensores
    public GameObject semilla;


    private float upForce; // Indica la fuerza de elevación del dron
    private float movementForwardSpeed = 50.0f; // Escalar para indicar fuerza de movimiento frontal
    private float wantedYRotation; // Auxiliar para el cálculo de rotación
    private float currentYRotation; // Auxiliar para el cálculo de rotación
    private float rotateAmountByKeys = 2.5f; // Auxiliar para el cálculo de rotación
    private float rotationYVelocity; // Escalar (calculado) para indicar velocidad de rotación
    private float sideMovementAmount = 250.0f; // Escalar para indicar velocidad de movimiento lateral
    private float gradoFijo = 270;

    // Asignaciones de componentes
    void Start(){
        rb = GetComponent<Rigidbody>();
        sensor = GetComponent<Sensores>();
        bateria = GameObject.Find("Bateria").gameObject.GetComponent<Bateria>();
    }

    // ========================================
    // A partir de aqui, todos los métodos definidos son públicos, la intención
    // es que serán usados por otro componente (Controlador)

    public void Ascender(){
        upForce = 190;
        rb.AddRelativeForce(Vector3.up * upForce);
    }

    public void Descender(){
        upForce = 10;
        rb.AddRelativeForce(Vector3.up * upForce);
    }

    public void Flotar(){
        upForce = 98.1f;
        rb.AddRelativeForce(Vector3.up * upForce);
    }

    public void Adelante(){
        rb.AddRelativeForce(Vector3.forward * movementForwardSpeed);
    }

    public void Atras(){
        rb.AddRelativeForce(Vector3.back * movementForwardSpeed);
    }

    public void GirarDerecha(){
        wantedYRotation += rotateAmountByKeys;
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
        rb.rotation = Quaternion.Euler(new Vector3(rb.rotation.x, currentYRotation, rb.rotation.z));
    }
    public void GirarIzquierda90(float grado){
        wantedYRotation += rotateAmountByKeys;
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
        rb.rotation = Quaternion.Euler(new Vector3(rb.rotation.x, gradoFijo - grado, rb.rotation.z));
    }

    public void GirarDerecha90(float grado){
        wantedYRotation += rotateAmountByKeys;
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
        rb.rotation = Quaternion.Euler(new Vector3(rb.rotation.x, gradoFijo + grado, rb.rotation.z));
    }

    public void GirarIzquierda(){
        wantedYRotation -= rotateAmountByKeys;
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
        rb.rotation = Quaternion.Euler(new Vector3(rb.rotation.x, currentYRotation, rb.rotation.z));
    }

    public void Derecha(){
        rb.AddRelativeForce(Vector3.right * sideMovementAmount);
    }

    public void Izquierda(){
        rb.AddRelativeForce(Vector3.left * sideMovementAmount);
    }

    public void Detener(){
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Sembrar(){
       if(sensor.ZonaDeSembrado() && !sensor.Sembrado()){
            Instantiate(semilla, new Vector3(transform.position.x,transform.position.y - 0.20f,transform.position.z), Quaternion.identity);
       }
    }

    public void VolverABase(){
        if (transform.position != GameObject.Find("BaseDeCarga").transform.position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, GameObject.Find("BaseDeCarga").transform.position, 5 * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
    }

    public void CargarBateria(){
        bateria.Cargar();
    }

}
