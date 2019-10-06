using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Spawner : MonoBehaviour
{
    [SerializeField] GameObject Top_Jaw = null;
    [SerializeField] GameObject Bottom_Jaw = null;

    private void Update()
    {
        if(TimeLease.CheckTime(gameObject.GetInstanceID().ToString(), 15))
        {
            StartCoroutine("EnemySpawn");
        }
    }

    IEnumerator EnemySpawn()
    {
        Top_Jaw.GetComponent<Animator>().SetTrigger("EnemySpawn");
        Bottom_Jaw.GetComponent<Animator>().SetTrigger("EnemySpawn");

        yield return new WaitForSeconds(0.45f);

        Debug.Log("Spawn enemy");
        //Instantiate enemy
    }

}
