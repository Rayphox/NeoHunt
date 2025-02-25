using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject explosion;
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "bullet")
        {
            if(collision.gameObject.tag == "Enemigo")
            {
                collision.gameObject.GetComponent<EnemigoControler>().DamagenEmigo(damage);
                LevelManager.instance.pointsGame += 2;
            }
            else
            {
                LevelManager.instance.pointsGame -= 1;
            }
            GameObject aux = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(aux, 1.5f);
            Destroy(this.gameObject);
        }
    }
}
