using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float Giro = 100f; //controle de giro no inspetor
    [SerializeField] float Aceleracao = 100f;//contole de aceleracao no inspetor 
    [SerializeField] ParticleSystem particulasMotor;//slot das particulas
    [SerializeField] ParticleSystem sparticulasSucesso;//slot das particulas
    [SerializeField] ParticleSystem particulasMorte;//slot das particulas

    Rigidbody rigidBody;//declarando o rigidbody    
    

    enum State { Vivo, Morto, Transicao }//declarando os estados do player
    State state = State.Vivo;//dizendo que o estado padrao é vivo

	public int contagem;
	public Text textoContagem;

	// Carrega na inicializacao
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();//chamando o rigidbody
		contagem = 0;
		SettextoContagem();
        
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

	void OnTriggerEnter(Collider other)
	
	{
	contagem = contagem + 1;
	SettextoContagem();
	
	}

	void SettextoContagem()
	{

	textoContagem.text = "Pontos: " + contagem.ToString();

	}

    void OnCollisionEnter(Collision collision)//sistema de deteccao de colisoes e acoes a serem tomadas
    {
        if (state != State.Vivo) { return; } //ignorar colisoes apos morto

        switch (collision.gameObject.tag)
        {
            case "Amigo"://se for tag amigo nao fazer nada
                
                break;
            case "Level2": //se for tag level2 comecar a sequencia do level
                ComecarSequenciaDoLevel2();
                break;

			case "Level3": //se for tag level2 comecar a sequencia do level
                ComecarSequenciaDoLevel3();
                break;

			case "Level4": //se for tag level4 comecar a sequencia do level
                ComecarSequenciaDoLevel4();
                break;
			
            default:
                IniciarSequenciaDeMorte();//se for untagged, morre!
                break;
        }
    }

	private void IniciarSequenciaDeMorte()
    {
        state = State.Morto;
        
        particulasMorte.Play();

		GetComponent<MeshRenderer>().enabled = false;
			
        Invoke("CarregarLevel1", 1f); 
    }

    private void ComecarSequenciaDoLevel2()
    {
        state = State.Transicao;
        
        sparticulasSucesso.Play();
        Invoke("CarregarLevel2", 1f); 
    }

	private void ComecarSequenciaDoLevel3()
    {
        state = State.Transicao;
        
        sparticulasSucesso.Play();
        Invoke("CarregarLevel3", 1f); 
    }

	private void ComecarSequenciaDoLevel4()
    {
        state = State.Transicao;
        
        sparticulasSucesso.Play();
        Invoke("CarregarLevel4", 1f); 
    }
	
    
		//para carregar levels
	
	 private void CarregarLevel1()
    {
        SceneManager.LoadScene(0);
    }

    private void CarregarLevel2()
    {
        SceneManager.LoadScene(1);
	}

	private void CarregarLevel3()
    {
        SceneManager.LoadScene(2);
	}

	private void CarregarLevel4()
    {
        SceneManager.LoadScene(3);
	}
    

   //parte dos comandos de teclado

    private void EfeitoAceleracao() // Aqui está o que acontece se a tecla espaco for pressionada
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SequenciaDeAceleracao(); // se espaco (space) mencionado acima for pressionado iniciar a sequencia de aceleracao
        }
        else
        {            
            particulasMotor.Stop();// apos soltar espaco parar as particulas
        }
    }

    private void SequenciaDeAceleracao() // aqui está a sequencia de aceleracao

    {
        rigidBody.AddRelativeForce(Vector3.up * Aceleracao); // aplicar forca no componente rigidbody em um dos eixos do vector3
      
        particulasMotor.Play(); //tocar as particulas
    }

    private void AplicarGiro()
    {
        rigidBody.freezeRotation = true; //para evitar bugs de colisao
       
        float girarNoFrame = Giro * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * girarNoFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * girarNoFrame);
        }

        rigidBody.freezeRotation = false;  //para evitar bugs de colisao
    }
}