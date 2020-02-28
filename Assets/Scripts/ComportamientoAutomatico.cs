using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ejemplo de un comportamiento automático para el agente (basado en modelos)
public class ComportamientoAutomatico : MonoBehaviour {

	private Sensores sensor;
	private Actuadores actuador;
	private DronState currentState;


	void Start(){
		sensor = GetComponent<Sensores>();
		actuador = GetComponent<Actuadores>();
		currentState = DronState.Iniciar;
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
			if(sensor.ZonaDeSembrado()){
				if(sensor.Sembrado()){
					currentState = DronState.Sembrado;
				} else {
					actuador.Flotar();
					actuador.Detener();
					currentState = DronState.Sembrar;
				}
			}
			if(sensor.FrenteAPared()){
				actuador.Flotar();
				actuador.Detener();
				currentState = DronState.Obstaculo;
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
			case DronState.Obstaculo:
			{
			if(sensor.FrenteAPared()){
			actuador.Flotar();
			actuador.Detener();
			} else {
			currentState = DronState.Girar;
			}
			break;
			}
			case DronState.Girar:
			{
			actuador.Izquierda();
			currentState = DronState.Avanzar;
			break;
			}
			case DronState.Cargando:
			{
			break;
			}
		}
	}
}
