using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CoreScript : MonoBehaviour
{
    public int countToWin = 36;
    public int countToLoss = 0;
    public int[] tagArray = new int[12];
    public List<GameObject> coreArray = new List<GameObject>();
    public List<GameObject> coreArraySave = new List<GameObject>();
    public bool actionTriggered;
    public void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject[] objectsWithTagCore = GameObject.FindGameObjectsWithTag("Core" + i);

            for (int j = 0; j < objectsWithTagCore.Length; j++)
            {
                coreArraySave.Add(objectsWithTagCore[j]);
            }
     
        }

        StartCoroutine(TransformObjectsWithDelay(coreArraySave));

        /*GameObject[] objectsWithTagCore = GameObject.FindGameObjectsWithTag("Core1")
                .Concat(GameObject.FindGameObjectsWithTag("Core2"))
                .Concat(GameObject.FindGameObjectsWithTag("Core3"))
                .Concat(GameObject.FindGameObjectsWithTag("Core4"))
                .Concat(GameObject.FindGameObjectsWithTag("Core5"))
                .Concat(GameObject.FindGameObjectsWithTag("Core6"))
                .Concat(GameObject.FindGameObjectsWithTag("Core7"))
                .Concat(GameObject.FindGameObjectsWithTag("Core8"))
                .Concat(GameObject.FindGameObjectsWithTag("Core9"))
                .Concat(GameObject.FindGameObjectsWithTag("Core10"))
                .Concat(GameObject.FindGameObjectsWithTag("Core11"))
                .Concat(GameObject.FindGameObjectsWithTag("Core0"))
                .ToArray();*/
        
        //StartCoroutine(TransformObjectsWithDelay(objectsWithTagCore));

    }

    IEnumerator TransformObjectsWithDelay(List<GameObject> coreArraySave)
    {
        for (int i = 0; i < coreArraySave.Count; i++)
        {
            coreArraySave[i].transform.position = new Vector3(
                Random.Range(-5f, 5f),
                3.5f,
                0f);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
