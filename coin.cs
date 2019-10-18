using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
	[SerializeField] ParticleSystem particulasSucesso;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, 0, 150 * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other)

	{		
		
		particulasSucesso.Play();
		GetComponent<MeshRenderer>().enabled = false;
		Destroy(gameObject, 1f);
		
		
	}
}
