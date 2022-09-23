using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class BarricadeEntity : Entity
{
    private bool flagInvincible = false;
    public GameObject GhostBarricade;
    // public GameObject spike;
    //private Color couleur;

    private void Awake()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) //si on meurt, on émet le son et on lance la coroutine de mort
        {
            Instantiate(GhostBarricade, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie") //si on percute un joueur, le monstre meurt
        {
            if (!flagInvincible)
            {
                currentHealth -= other.GetComponent<Zombie>().GetZombieDamage();
                StartCoroutine("getHit");
            }
        }

        if (currentHealth <= 0) //si on meurt, on émet le son et on lance la coroutine de mort
        {
            Instantiate(GhostBarricade, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    IEnumerator getHit() //La coroutine sert à désactiver partiellement le monstre pour jouer le son de mort avant de le supprimer pour de bons à la fin
    {
        flagInvincible = true;
        yield return new WaitForSeconds(1f);
        flagInvincible = false;
    }

}
