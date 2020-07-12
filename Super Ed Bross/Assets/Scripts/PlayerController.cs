using Assets.Scripts.Constans;
using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Singlenton
    public static PlayerController sharedInstance;

    //Variables
    public float JumpForce = 60f;
    private Rigidbody2D rigidBody;
    public Animator animator;
    public LayerMask GroundLayer; //Detecta la capa del suelo.
    public float runningSpeed = 1.5f;
    private UnityEngine.Vector3 startPosition;
    public string SelectedLanguage { get; set; }
    private int healthPoints, manaPoints;


    // Start is called before the first frame update
    private void Awake()
    {
        sharedInstance = this;
        //Consulta el componente en unity
        rigidBody = GetComponent<Rigidbody2D>();
        //Toma la posición donde inicia el personaje.
        startPosition = this.transform.position;
    }



    // Start is called before the first frame update
    public void StartGame()
    {
        animator.SetBool(Constants.States.isDead, true);
        animator.SetBool(Constants.States.isAlive, true);
        animator.SetBool(Constants.States.isGrounded, true);
        //animator.SetBool("AbsDead", true);
        this.transform.position = startPosition;
        manaPoints = Constants.CollectableLimits.MaxMana;
        healthPoints = Constants.CollectableLimits.MaxHealth;

        StartCoroutine(Constants.courrutines.TirePlayer);
    }

    IEnumerator TirePlayer()
    {
        while (this.healthPoints > 0)
        {
            this.healthPoints--;
            yield return new WaitForSeconds(2.0f);
           
        }
        this.animator.SetBool(Constants.States.isDead, false);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Solo deja ejecutar si el juego se encuentra en modo inGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Preciona la tecla espacio
                Jump(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                //Preciona la tecla espacio
                Jump(true);
            }

            animator.SetBool(Constants.States.isGrounded, IsTouchingTheGround());
            //Se cambia la animación de salto.
        }


    }

    private void FixedUpdate()
    {
       
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            float currentSpeed = (runningSpeed - 1.5f) * this.healthPoints / 100.0f;
            //if (rigidBody.velocity.x < currentSpeed)
            //{
            if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    rigidBody.velocity = new Vector2(currentSpeed,//Velocidad en X
                                rigidBody.velocity.y); //Velocidad en Y
                    SetGiroCharacter(0);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    rigidBody.velocity = new Vector2(-currentSpeed,//Velocidad en X
                                rigidBody.velocity.y); //Velocidad en Y
                    SetGiroCharacter(-160);
                }
            //}
                    
        }
           
    }


    void Jump(bool isSuperJump)
    {
        //Aplica fuerza de salto
        if(IsTouchingTheGround())
        {
            if(isSuperJump && this.manaPoints >= 5)
            {
                this.manaPoints -= 5;
                rigidBody.AddForce(Vector2.up * JumpForce * 1.5f, ForceMode2D.Impulse);
            }
            else
            {
                rigidBody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            }
            
            
        }
    }

    void SetGiroCharacter(int value)
    {
        rigidBody.transform.eulerAngles = new Vector3(
                       rigidBody.transform.eulerAngles.x,
                       value,
                       rigidBody.transform.eulerAngles.z);
    }

    bool IsTouchingTheGround() //Este metodo valida si se esta tocando el suelo.
    {
        if (Physics2D.Raycast(this.transform.position,//Posiciona un rayo para ver si 
                                                      //se encuentra con la capa del suelo
            Vector2.down,
            0.2f, //20 cm
            GroundLayer))
        {//Suelo
            return true;
        }
        else
        {
            return false;
        }
    }


    public void Kill()
    {
        GameManager.sharedInstance.GamerOver();
        
        float currentMaxScore = PlayerPrefs.GetFloat(Constants.CollectableLimits.maxScore, 0);
        float actualDistance = this.GetDistance();
        if (currentMaxScore < actualDistance)
        {
            PlayerPrefs.SetFloat(Constants.CollectableLimits.maxScore, actualDistance);
        }
        StopCoroutine(Constants.courrutines.TirePlayer);
        this.animator.SetBool(Constants.States.isDead, false);
    }

    public float GetDistance()
    {
        float traveledDistance = Vector2.Distance(new Vector2(startPosition.x,0),
                                                   new Vector2(this.transform.position.x,0));
        return traveledDistance;
    }

    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        if(this.healthPoints >= Constants.CollectableLimits.MaxHealth)
        {
            this.healthPoints = Constants.CollectableLimits.MaxHealth;
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= Constants.CollectableLimits.MaxMana)
        {
            this.manaPoints = Constants.CollectableLimits.MaxMana;
        }
    }

    public int GetHealth()
    {
        return this.healthPoints;
    }

    public int GetMana()
    {
        return this.manaPoints;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        switch (otherCollider.tag)
        {
            case Constants.tags.enemy:
                this.healthPoints -= 20;
                break;
            case Constants.tags.rock:
                this.healthPoints -= 15;
                break;


            default:
                break;
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame && this.healthPoints <= 0)
        {
            Kill();
        }
    }
}
