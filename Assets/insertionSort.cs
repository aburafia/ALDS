using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insertionSort : MonoBehaviour
{
    public GameObject obj;

    GameObject[] objs;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        int count = 10;

        objs = new GameObject[count];
        yield return StartCoroutine(genRandom(count));
        yield return StartCoroutine(inserttionSort());
    }

    IEnumerator insertionSoftFunc(){
        yield return null;
    }

    IEnumerator genRandom(int count)
    {


        for (int i=0; i<count; i++)
        {
            objs[i] = clone(obj);

            objs[i].transform.localPosition = new Vector3(numToXpos(i), 0, 0);
            objs[i].transform.localScale += new Vector3(0, Random.Range(0.1f, 5), 0);

            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    IEnumerator inserttionSort()
    {
        for (int i=1; i<objs.Length; i++)
        {
            var currentObj = objs[i];

            int j = i - 1;
            while(j >= 0 && objs[j].transform.localScale.y > currentObj.transform.localScale.y)
            {
                objs[j + 1] = objs[j];

                //transformをj,j+1で入れ替えると、かなりややこしかったので、添字から導出するようにした
                objs[j + 1].transform.position = new Vector3(numToXpos(j + 1), 0, 0);

                j--;

                yield return new WaitForSeconds(0.2f);
            }
            objs[j+1] = currentObj;
            objs[j + 1].transform.position = new Vector3(numToXpos(j + 1), 0, 0);
        }

        yield return null;
    }

    //添字からxの位置を導出
    public float numToXpos(int i)
    {
        //間隔
        float dXPosition = 0.5f;

        //初期位置
        float firstXpos = -5;

        return firstXpos + dXPosition * i;
    }

    public GameObject clone( GameObject go )
    {
        var clone = GameObject.Instantiate( go ) as GameObject;
        clone.transform.parent = go.transform.parent;
        clone.transform.localPosition = go.transform.localPosition;
        clone.transform.localScale = go.transform.localScale;
        return clone;
    }
}