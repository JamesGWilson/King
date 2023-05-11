using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    public float speed = 2;
    public Transform player;
    public bool followPlayer = false;
    public bool charge = false;
    public bool utility = false;
    public bool carefree = true;

    private Rigidbody rb;
    private Vector3 targetPosition;   
    private Quaternion lookPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(followPlayer){
            var list = player.transform.GetComponent<KingsGuard>().guardList;
            var index = list.IndexOf(transform.gameObject);
            var behindPlayer = player.position - (player.forward * (1 + index));

            targetPosition = behindPlayer;
            targetPosition.y = transform.position.y;

            if(transform.position == targetPosition){
                lookPos = player.rotation;           
            } else {
                lookPos = Quaternion.LookRotation(targetPosition - transform.position);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, lookPos, Time.deltaTime * 5);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

            if(Input.GetMouseButtonDown(0) && transform.position == targetPosition){
                if(index == 0){
                    followPlayer = false;
                    charge = true;
                    targetPosition = player.position + (player.forward * 10);
                    targetPosition.y = transform.position.y;
                    list.RemoveAt(0);
                }
            }    
        } 

        if(charge){
            if(Input.GetMouseButtonDown(1)){
                returnFromCharge();
            }

            if(transform.position == targetPosition){
                returnFromCharge();       
            } else {
                lookPos = Quaternion.LookRotation(targetPosition - transform.position);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, lookPos, Time.deltaTime * 5);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        }

        if(utility){
            if(Input.GetMouseButtonDown(1)){
                returnFromCharge();
            }
        }
    }

    public void returnFromCharge(){
        var list = player.transform.GetComponent<KingsGuard>().guardList;
        charge = false;
        utility = false;
        followPlayer = true;
        list.Add(transform.gameObject); 
    }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "TeamKing")
      {
        Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
      } else if( charge == true){
        returnFromCharge();
      }
    }
}
