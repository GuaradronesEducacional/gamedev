using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilador : MonoBehaviour {

    [SerializeField] Vector3 vetorDeMovimento = new Vector3(10f, 10f, 10f);
    [SerializeField] float periodo = 2f;

    float fatorDeMovimento; // 0 para sem movimento 1 para movimento total
    Vector3 startingPos;

    // Use this for initialization
	void Start () {
        startingPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
       
        float ciclos = Time.time / periodo; //crescem continuamente de zero

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(ciclos * tau);

        fatorDeMovimento = rawSinWave / 2f + 0.5f;
        Vector3 offset = fatorDeMovimento * vetorDeMovimento;
        transform.position = startingPos + offset;
	}
}