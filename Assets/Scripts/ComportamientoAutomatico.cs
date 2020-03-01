using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ejemplo de un comportamiento automático para el agente (basado en modelos)
public class ComportamientoAutomatico : MonoBehaviour {

	private Sensores sensor;
	private Actuadores actuador;
	private DronState currentState;
	private bool CambiandoParcela;


	void Start(){
		sensor = GetComponent<Sensores>();
		actuador = GetComponent<Actuadores>();
		currentState = DronState.Iniciar;
		CambiandoParcela = false;
	}
	void FixedUpdate() {
		if(sensor.Bateria() <= 0){
			currentState = DronState.Cargando;
			return;
		}
		switch(currentState){
			case DronState.Iniciar:
			{
			actuador.Ascender();
			currentState = DronState.Avanzar;
			break;
			}
			case DronState.Avanzar:
			{
			if(CambiandoParcela){
				if(sensor.ZonaDeSembrado() && !sensor.Sembrado() && sensor.FrenteAParedAbajo()){
					actuador.GirarDerecha90(90);
					Debug.Log("GiroDerecha90");
					CambiandoParcela = false;
					currentState = DronState.Sembrar;
				}
				if(sensor.ZonaDeSembrado() && !sensor.Sembrado()){
					actuador.GirarIzquierda90(90);
					Debug.Log("GiroIzquierda90");
					CambiandoParcela = false;
					currentState = DronState.Sembrar;
				}
				actuador.Adelante();
			}
			if(sensor.ZonaDeSembrado()){
				if(sensor.Sembrado()){
					currentState = DronState.Sembrado;
				} else {
					actuador.Flotar();
					actuador.Detener();
					currentState = DronState.Sembrar;
				}
			}
			if(sensor.FrenteAPared() || sensor.FrenteAParedAbajo()){
				actuador.Flotar();
				actuador.Detener();
				actuador.GirarIzquierda90(0);
				Debug.Log("GiroIzquierda90");
				currentState = DronState.CambiarParcela;
			} else {
				actuador.Flotar();
				actuador.Adelante();
			}
			break;
			}
			case DronState.Sembrar:
			{
				actuador.Sembrar();
				currentState = DronState.Sembrado;
				break;
			}
			case DronState.Sembrado:
			{
				if(sensor.Sembrado()){
					actuador.Flotar();
					actuador.Adelante();
				}
				currentState = DronState.Avanzar;
				break;
			}
			case DronState.CambiarParcela:
			{
				if(!sensor.ZonaDeSembrado() || sensor.Sembrado()){
					CambiandoParcela = true;
					currentState = DronState.Avanzar;
					Debug.Log("Adelante");
				}else{
					actuador.Flotar();
					actuador.Detener();
					currentState = DronState.Avanzar;
				}
			break;
			}
			case DronState.Cargando:
			{
				actuador.VolverABase();
			break;
			}
		}
	}
}
