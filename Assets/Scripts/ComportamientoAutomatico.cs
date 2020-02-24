using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoAutomatico : MonoBehaviour {

	private Sensores sensor;
	private Actuadores actuador;
	private float grado = 90;
	private bool cercaPared = true;
	private DronState currentState;

	void Start(){
		sensor = GetComponent<Sensores>();
		actuador = GetComponent<Actuadores>();
	}
	
	void FixedUpdate () {
		switch (currentState)
        {

			case DronState.Iniciar:
            {
					
					actuador.Ascender();
					currentState = DronState.BuscarEsquina;
					Debug.Log("Estado inicial ");
					return;
            }
            // Estado de diambular falta el nivel de bateriía
			case DronState.Avanzar:
			{
					
					if (sensor.Bateria() != 0)
					{
						
						// Mientras nuestro dron no encuentre algo que avanze
						if (!sensor.FrenteAPared())
						{   
							actuador.Flotar();
							actuador.Adelante();
						}else{
							actuador.Detener();

							currentState = DronState.Obstaculo;
						}
						
					}else {
						currentState = DronState.Cargando;
                    }
                    
					return;
                    // busca un c
            }

            //Nos va a decir 
			case DronState.Obstaculo:
			{       // Mientras el dron este tocando pared gira hasta encontrar un camino
					if (sensor.Bateria() != 0)
					{

						if (sensor.FrenteAPared() && sensor.FrenteAParedDerecha() || sensor.FrenteAParedIzquierda())
						{
							Debug.Log("Estoy en el estado detener");
							actuador.Detener();
						} else {
						    actuador.Gira();
						    currentState = DronState.Avanzar;
                        }
					}
					else {
						currentState = DronState.Cargando;
                    }

					return;
            }


			case DronState.Cargando:
            {
                    // falta la rutina  para ir a cargar
					return;
            }

			


        }

	

	}
}
