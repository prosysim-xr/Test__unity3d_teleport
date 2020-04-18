using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject previousPos;
    public GameObject teleportCue;
    public GameObject temp;
    public GameObject player ;
    public GameObject env;

    public Vector3 originalPlayerScale;
    public Vector3 originalPreviousPosScale;
    //public GameObject ;

    public bool isInsideCar = false;
    public bool isTranslating = false;
    // Start is called before the first frame update
    void Start()
    {
        originalPlayerScale = player.transform.localScale;
        originalPreviousPosScale = previousPos.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Toggle_isInsideCar();
        }

        if (isInsideCar==true  &&Input.GetKeyDown(KeyCode.M))
        {
            player.transform.Translate(new Vector3(0, 0, 0.5f));
        }

        if (isInsideCar == false&&Input.GetKeyDown(KeyCode.N))
        {
            isTranslating = true;
        }

        if (isTranslating == true)
        {
            player.transform.Translate(new Vector3(0, 0, 0.01f));
        }
        if (isInsideCar == false /*&& (-1f > player.transform.localPosition.z || player.transform.localPosition.z > 1f)*/)//we can decrese or even make 0 change but it will be doing too much hard work and will be less optimized
        {
            PackUnpack();
        }
    }

    public void Toggle_isInsideCar()
    {
        isTranslating = false;
        if(isInsideCar == false)
        {
            isInsideCar = true;
            player.transform.parent = teleportCue.transform;
            player.transform.localScale = originalPlayerScale;
            player.transform.localPosition = new Vector3(0,0,0);
            player.transform.localRotation = Quaternion.identity;
        }
        else if(isInsideCar == true)
        {
            isInsideCar = false;
            player.transform.parent = previousPos.transform;
            player.transform.localScale = originalPlayerScale;
            player.transform.localPosition = new Vector3(0, 0, 0);
            player.transform.localRotation = Quaternion.identity;
            /* ePrefabs.Add(Instantiate(Resources.Load(tempString, typeof(GameObject))) as GameObject);
            var originalScale : Vector3 = ePrefabs[i].transform.localScale;
            ePrefabs[i].transform.parent = Platform.transform;
            ePrefabs[i].transform.localScale = originalScale;
            ePrefabs[i].transform.localPosition = pos;*/
        }
    }

    public void PackUnpack()
    {
        // this will create a problem if other ways to teleport are there being used.
        //getting ready to pack
        player.transform.parent = temp.transform;
        player.transform.localScale = originalPlayerScale;
        //packing
        previousPos.transform.parent = player.transform;
        previousPos.transform.localScale = originalPreviousPosScale;
        previousPos.transform.localPosition = new Vector3(0, 0, 0);
        previousPos.transform.localRotation = Quaternion.identity;

        //getting ready to unpack
        previousPos.transform.parent = temp.transform;
        previousPos.transform.localScale = originalPreviousPosScale;
        //unpacking
        player.transform.parent = previousPos.transform;
        player.transform.localScale = originalPlayerScale;
        player.transform.localPosition = new Vector3(0, 0, 0);
        player.transform.localRotation = Quaternion.identity;

        if (isInsideCar == false)
        {
            
        }
        //player.transform.SetPositionAndRotation(new Vector3(0,0,5f), Quaternion.identity);
        
    }

    
}
