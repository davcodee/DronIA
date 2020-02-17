using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoAutomatico : MonoBehaviour {

	private Sensores sensor;
	private Actuadores actuador;
    // Enumeración que maneja el estado en el cual se encuentra el dron.
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
					currentState = DronState.Avanzar;
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
							actuador.Adelante();
						}else{
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

						if (sensor.FrenteAPared())
						{
							actuador.Detener();
							actuador.GirarDerecha();

						}
						else {
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

/*
 * ENUMERACIÓN QUE MANEJA LOS ESTADOS DE NUESTRO DRON
 */
public enum DronState
{
    Avanzar,
    Iniciar,
    Obstaculo,
    Cargando
}
