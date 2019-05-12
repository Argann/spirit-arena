using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *	Classe appelee par le Prefab "Blurp" pour detruire le splash de mort des creatures a la fin de l'animation
 */
public class Blurp : MonoBehaviour {

	public void DestroyMe() {
		Destroy(gameObject, 0f);
	}
}
