using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    private float attackTime = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 2.7f))
        {
            if(hit.transform.tag == "Boulder"){
                transform.GetComponent<MinionMovement>().charge = false;
                transform.GetComponent<MinionMovement>().utility = true;
                attackTime++;
                if(hit.transform.GetComponent<Stats>().health == 0){
                    attackTime = 1f;
                    transform.GetComponent<MinionMovement>().returnFromCharge();
                }

                if(attackTime % 1000 == 0){
                    hit.transform.GetComponent<Stats>().health -= 1;
                }
            } 
        }
    }
}
