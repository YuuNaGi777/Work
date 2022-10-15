using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActter : MonoBehaviour
{
    int i ;
    public GameObject phree;
    public Transform PlayerTf;  
    public Rigidbody PhreeRb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attack();
        
    }

    private void attack()
    {
        GameObject objdel;
        GameObject Phree;
        if (Input.GetMouseButtonDown(0))
        {
            i++;
            Phree = Instantiate(phree, PlayerTf.position+new Vector3(0,3,0), PlayerTf.rotation);
            Phree.name = "phree" + i;
            Phree.GetComponent<Rigidbody>().AddForce(PlayerTf.forward * 10000);
            objdel = GameObject.Find("phree" + i);
            Destroy(objdel, 2.0f);

            
        }

        



    }
}
