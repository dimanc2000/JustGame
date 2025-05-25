using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadToWinScript : MonoBehaviour
{
    public CoreScript coreScript;
    public RestartScript restartScript;
    int count;
    int maxIndex;

    public bool restartBool = true;
    public bool isClicked = false;

    public GameObject prefabCore;


    void OnMouseDown()
    {
        if (!isClicked)
        {
            coreScript.countToLoss += 1;
            coreScript.coreArray.Add(gameObject);
            string objectTag = gameObject.tag;

            transform.position = new Vector3(-4.3f + (1.45f * coreScript.coreArray.Count - 1.45f), 6.7f, 0);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            for (int i = 0; i < 12; i++)
            {
                if (objectTag == "Core" + i)
                {
                    coreScript.tagArray[i] += 1;
                }
                if (coreScript.tagArray[i] == 3)
                {
                    //System.Random rnd = new System.Random();
                    //int randomNumber = rnd.Next(0, 12);
                    string tagSave = "Core" + i;

                    GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tagSave);
                    //GameObject[] objectsToSvap = GameObject.FindGameObjectsWithTag("Core" + randomNumber);

                    for (int j = 0; j < objectsToDestroy.Length; j++)
                    {
                        //objectsToDestroy[j].tag = "Core" + randomNumber;
                        //objectsToSvap[j].tag = tagSave;

                        /*if (objectsToSvap != null && j < objectsToSvap.Length && objectsToSvap[j] != null)
                        {
                            objectsToSvap[j].tag = tagSave;
                        }*/

                        Vector3 position = new Vector3(20, 10, 0);
                        Quaternion rotation = Quaternion.identity;
                        GameObject newObject = Instantiate(prefabCore, position, rotation);
                        newObject.GetComponent<PolygonCollider2D>().enabled = true;
                        newObject.GetComponent<RoadToWinScript>().enabled = true;


                        //newObject.tag = "Core" + randomNumber;

                        Destroy(objectsToDestroy[j]);


                    }

                    for (int n = 0; n < coreScript.coreArray.Count; n++)
                    {
                        if (coreScript.coreArray[n] != null && coreScript.coreArray[n].tag == tagSave)
                        {
                            coreScript.coreArray[n] = null;
                        }
                    }

                    coreScript.actionTriggered = false;
                    coreScript.tagArray[i] = 0;
                    coreScript.countToWin -= 3;
                    coreScript.countToLoss -= 3;
                    if (coreScript.countToWin == 0)
                    {
                        SceneManager.LoadScene("WinScene");
                    }


                    /*foreach (GameObject obj in objectsToDestroy)
                    {
                        DestroyImmediate(obj);
                    }*/
                    
                    //coreScript.tagArray[i] = 0;
                }
            }
            if (coreScript.countToLoss > 6)
            {
                SceneManager.LoadScene("GameOverScene");
            }
            
        }
        

    }

    void OnMouseUp()
    {
        isClicked = true;
    }

    void Update()
    {
        if (!restartScript.actionTriggeredRestart && isClicked)
        {
            restartBool = false;
        }

        if (restartBool == false)
        {
            isClicked = false;
            restartBool = true;
            StartCoroutine(TransformObjectsWithDelay());
        }

        if (!coreScript.actionTriggered)
        {
            coreScript.coreArray.RemoveAll(item => item == null);
            count = coreScript.coreArray.Count;
            maxIndex = Mathf.Min(7, count);
            for (int j = 0; j < maxIndex; j++)
            {
                if (coreScript.coreArray[j] != null)
                {
                    coreScript.coreArray[j].transform.position = new Vector3(-4.3f + (1.45f * j), 6.7f, 0);
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            coreScript.actionTriggered = true;
        }

    }

    IEnumerator TransformObjectsWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        restartScript.actionTriggeredRestart = true;
    }
}
