using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public List<GameObject> coreArrayRestart = new List<GameObject>();
    public List<GameObject> coreArraySave = new List<GameObject>();

    //public GameObject[] objectsWithTagSaveCore;

    public CoreScript coreScript;

    public int n;

    public List<int> tagNumbers = new List<int>();

    public bool actionTriggeredRestart = true;

    public void Restart()
    {
        
        n = coreScript.countToWin / 3;
        coreArrayRestart.Clear();
        coreArraySave.Clear();
        //GameObject.Clear(objectsWithTagSaveCore, 0, objectsWithTagSaveCore.Length);
        coreScript.coreArray.Clear();
        coreScript.countToLoss = 0;
        tagNumbers.Clear();

        for (int i = 0; i < coreScript.tagArray.Length; i++)
        {
            coreScript.tagArray[i] = 0;
        }



        int totalTags = 12;
        
        for (int i = 0; i < totalTags; i++)
        {
            tagNumbers.Add(i);
        }
        System.Random rnd = new System.Random();
        for (int i = tagNumbers.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(0, i + 1);
            int temp = tagNumbers[i];
            tagNumbers[i] = tagNumbers[j];
            tagNumbers[j] = temp;
        }

        for (int i = 0; i < totalTags; i++)
        {
            GameObject[] objectsWithTagSaveCore = GameObject.FindGameObjectsWithTag("Core" + i);

            for (int j = 0; j < objectsWithTagSaveCore.Length; j++)
            {
                coreArraySave.Add(objectsWithTagSaveCore[j]);
            }
        }


        for (int i = 0; i < totalTags; i++)
        {
            string tagName = "Core" + tagNumbers[i];

            if ((i*3 + 2) < totalTags*3)
            {
                coreArraySave[i*3].tag = tagName;
                coreArraySave[i*3 + 1].tag = tagName;
                coreArraySave[i*3 + 2].tag = tagName;
            }
        }

        StartCoroutine(RestartTransformObjectsWithDelay(coreArraySave));


    }

    IEnumerator RestartTransformObjectsWithDelay(List<GameObject> coreArraySave1)
    {
        for (int i = 0; i < coreArraySave1.Count; i++)
        {
            coreArraySave1[i].transform.position = new Vector3(
                20f,
                10f,
                0f);

            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < n; i++)
        {
            GameObject[] objectsWithTagCore = GameObject.FindGameObjectsWithTag("Core" + i);

            for (int j = 0; j < objectsWithTagCore.Length; j++)
            {
                coreArrayRestart.Add(objectsWithTagCore[j]);
            }

        }
        StartCoroutine(TransformObjectsWithDelay(coreArrayRestart));
    }

    IEnumerator TransformObjectsWithDelay(List<GameObject> coreArrayRestart1)
    {
        for (int i = 0; i < coreArrayRestart1.Count; i++)
        {
            coreArrayRestart1[i].transform.position = new Vector3(
                Random.Range(-5f, 5f),
                3.5f,
                0f);
            
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        actionTriggeredRestart = false;
    }
}
