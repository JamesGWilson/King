using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsGuard : MonoBehaviour
{
    public List<GameObject> guardList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, 2.7f))
            {
                if(hit.transform.tag == "TeamKing" && hit.transform.GetComponent<MinionMovement>().carefree){
                    hit.transform.GetComponent<MinionMovement>().followPlayer = true;
                    hit.transform.GetComponent<MinionMovement>().carefree = false;
                    guardList.Add(hit.transform.gameObject);
                }  
            }
        }

        if(Input.GetKeyDown(KeyCode.E)){
            var firstGuard = guardList[0];
            guardList.RemoveAt(0);
            guardList.Add(firstGuard);
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            var lastGuard = guardList[guardList.Count - 1];
            guardList.RemoveAt(guardList.Count - 1);
            guardList.Insert(0, lastGuard);
        }
    }
}
