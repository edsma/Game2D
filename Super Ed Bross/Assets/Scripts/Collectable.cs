using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //Variable para saber si la moneda fue recolectada o no.
    bool isCollected = false;
    public int value = 0;
    public AudioClip collectSound;

    public CollectableType type = CollectableType.money;

    //Metodo para mostrar el coleccionable
    void Show()
    {
        //Se activa la imagen de la moneda y la animación.
        this.GetComponent<SpriteRenderer>().enabled = true;
        //Activa el collider de la imagen.
        this.GetComponent<CircleCollider2D>().enabled = true;
        //Variable para indicar que no se encuentra recolectado el item.
        isCollected = false;
    }

    //Metodo para esconder el coleccionable
    void Hide()
    {
        //Desactiva la imagen de la moneda y la animación.
        this.GetComponent<SpriteRenderer>().enabled = false;
        //Desactiva el collider de la imagen.
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    void Collect()
    {
        isCollected = true;
        Hide();
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.PlayOneShot(this.collectSound);
        }
        else
        {

        }
        switch (this.type)
        {
            case CollectableType.healthPotion:
                //da vida al jugador
                PlayerController.sharedInstance.CollectHealth(value);
                break;
            case CollectableType.manaPotion:
                PlayerController.sharedInstance.CollectMana(value);
                break;
            case CollectableType.money:
                GameManager.sharedInstance.CollectObject(value);
                break;
            default:
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        switch (otherCollider.tag)
        {
            case "Player":
                Collect();
                break;
        }
    }
}