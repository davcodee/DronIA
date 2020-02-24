using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Componente auxiliar para genera rayos que detecten colisiones de manera lineal
// En el script actual se dibuja y comprueban colisiones con un rayo al frente del objeto
// sin embargo, es posible definir más rayos de la misma manera.
public class Rayo : MonoBehaviour
{
    public float longitudDeRayo;
    private bool frenteAPared;
    private bool frenteAParedIzquierda;
    private bool frenteAParedDerecha;

    void Update(){
        // Se muestra el rayo únicamente en la pantalla de diseño (Scene)
        Debug.DrawLine(transform.position, transform.position + (transform.forward * longitudDeRayo), Color.blue);
        Debug.DrawLine(transform.position, transform.position + (transform.right * longitudDeRayo), Color.red);
        Debug.DrawLine(transform.position, transform.position + ((transform.right * -1 ) * longitudDeRayo), Color.green);
	//PARA SABER DONDE PONER SEMILLAS
	Debug.DrawLine(transform.position, transform.position + ((transform.up * -1) * longitudDeRayo), Color.yellow);
    }

    void FixedUpdate(){
        // Similar a los métodos OnTrigger y OnCollision, se detectan colisiones con el rayo:
        frenteAPared = false;
	frenteAParedDerecha = false;
        frenteAParedIzquierda = false;

        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, longitudDeRayo))
            if(raycastHit.collider.gameObject.CompareTag("Pared"))
                frenteAPared = true;
	// CASO CUANDO TENEMOS UN OBJETO DEL LADO IZQUIERDO
        if (Physics.Raycast(transform.position, (transform.right * -1), out raycastHit, longitudDeRayo)) {
            if (raycastHit.collider.gameObject.CompareTag("Pared"))
                frenteAParedIzquierda = true;
        }
        // CASO CUANDO TENEMOS UN OBJETO DEL LADO DERECHO
        if (Physics.Raycast(transform.position, transform.right , out raycastHit, longitudDeRayo))
        {
            if (raycastHit.collider.gameObject.CompareTag("Pared"))
                frenteAParedIzquierda = true;
	}
    }

    // Ejemplo de métodos públicos que podrán usar otros componentes (scripts):
    public bool FrenteAPared(){
        return frenteAPared;
    }
// METODO QUE NOS DICE SI HAY UNA PARED IZQUIERDA
    public bool FrenteAParedIzquierda(){
        return frenteAParedIzquierda;
    }

    //MÉTODO QUE NOS DICE SI HAY UNA PARED DERECHA.
    public bool FrenteAparedDerecha() {
        return frenteAParedDerecha;
    }
}
