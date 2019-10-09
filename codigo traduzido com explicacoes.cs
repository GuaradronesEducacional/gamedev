using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float Giro = 100f; //controle de giro no inspetor
    [SerializeField] float Aceleracao = 100f;//contole de aceleracao no inspetor

   

    [SerializeField] ParticleSystem mainEngineParticles;//slot das particulas
    [SerializeField] ParticleSystem successParticles;//slot das particulas
    [SerializeField] ParticleSystem deathParticles;//slot das particulas

    Rigidbody rigidBody;//declarando o rigidbody
    

    enum State { Vivo, Morto, Transicao }//declarando os estados do player
    State state = State.Vivo;//dizendo que o estado padrao é vivo

	// Carrega na inicializacao
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();//chamando o rigidbody
        
	}
	
	// Update é chamado a cada frame
	void Update ()
    {
        if (state == State.Vivo)
        {
            EfeitoAceleracao();
            AplicarGiro();
        }
    }

    void OnCollisionEnter(Collision collision)//sistema de deteccao de colisoes e acoes a serem tomadas
    {
        if (state != State.Vivo) { return; } //ignorar colisoes apos morto

        switch (collision.gameObject.tag)
        {
            case "Amigo"://se for tag amigo nao fazer nada
                
                break;
            case "Level2": //se for tah level2 comecar a sequencia do level
                ComecarSequenciaDoLevel2();
                break;
			
            default:
                ComecarSequenciaDeMorte();//se for untagged, morre!
                break;
        }
    }

	private void ComecarSequenciaDeMorte()
    {
        state = State.Morto;
        
        deathParticles.Play();
        Invoke("CarregarLevel1", 1f); 
    }

    private void ComecarSequenciaDoLevel2()
    {
        state = State.Transicao;
        
        successParticles.Play();
        Invoke("CarregarLevel2", 1f); 
    }

    
		//Classes para carregar levels
	
	 private void CarregarLevel1()
    {
        SceneManager.LoadScene(0);
    }

    private void CarregarLevel2()
    {
        SceneManager.LoadScene(1);
	}

   //parte dos comandos de teclado

    private void EfeitoAceleracao()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            AplicarAceleracao();
        }
        else
        {
            
            mainEngineParticles.Stop();
        }
    }

    private void AplicarAceleracao()
    {
        rigidBody.AddRelativeForce(Vector3.up * Acelerar);
      
        mainEngineParticles.Play();
    }

    private void AplicarGiro()
    {
        rigidBody.freezeRotation = true; 
       
        float girarNoFrame = Giro * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * girarNoFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * girarNoFrame);
        }

        rigidBody.freezeRotation = false; 
    }
}