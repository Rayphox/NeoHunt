using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject explosion;//efecto de explosion
    public int damage;//da;a
    private void OnCollisionEnter(Collision collision)//cuando colisiona con algo
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "bullet")//mientras no sea el player o otra bala
        {
            if(collision.gameObject.tag == "Enemigo")//si es contra un enemigo
            {
                collision.gameObject.GetComponent<EnemigoControler>().DamagenEmigo(damage);//le cargamos el da;o al enemigo
                LevelManager.instance.pointsGame += 2;//ganamos putnos
            }
            else
            {
                LevelManager.instance.pointsGame -= 1;//perdemos puntos 
            }
            GameObject aux = Instantiate(explosion, this.transform.position, Quaternion.identity);//creamos una explosion en el lugar
            SoundManager.instance.newEfect("explosion");
            Destroy(aux, 1.5f);//la explosion se destruye despues de 1.5 segundos
            Destroy(this.gameObject);//la bala se destruye
        }
    }
}
