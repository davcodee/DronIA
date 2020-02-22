using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ENUMERACIÓN PARA MANEJAR LOS ESTADOS DE NUESTRO DRON 
 * **/
public enum DronState {
    Avanzar,
    Iniciar,
    BuscarEsquina,
    Sembrar,
    Sembrado,
    Obstaculo,
    Cargando

}