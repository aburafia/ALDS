using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ALDS_1_2_A_BubbleSort : MonoBehaviour
{
    public GameObject obj;
    public Text countText;

    GameObject[] objs;
    

    // Start is called before the first frame update
    IEnumerator Start()
    {
        int count = 10;

        objs = new GameObject[count];
        yield return StartCoroutine(genRandom(count));
        yield return StartCoroutine(bubbleSort());
    }

    IEnumerator insertionSoftFunc()
    {
        yield return null;
    }

    IEnumerator genRandom(int count)
    {
        for (int i = 0; i < count; i++)
        {
            objs[i] = clone(obj);

            objs[i].transform.localPosition = new Vector3(numToXpos(i), 0, 0);
            objs[i].transform.localScale += new Vector3(0, Random.Range(0.1f, 5), 0);

            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    int count = 0;

    IEnumerator bubbleSort()
    {
        int head = 0;
        bool flg = true;
		while (flg)
		{
            flg = false;

            for(int i = objs.Length - 1; i > head; i--) {
                if(objs[i].transform.localScale.y < objs[i - 1].transform.localScale.y)
				{
                    var tmp = objs[i];
                    objs[i] = objs[i - 1];
                    objs[i - 1] = tmp;

                    objs[i - 1].transform.position = new Vector3(numToXpos(i - 1), 0, 0);
                    objs[i].transform.position = new Vector3(numToXpos(i), 0, 0);

                    flg = true;

                    count++;
                    countText.text = count.ToString();

                    yield return new WaitForSeconds(0.2f);
                }
            }

            //判定しない先頭位置が迫り上がっていく
            objs[head].GetComponent<Renderer>().material.color = Color.red;
            head++;
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

    public GameObject clone(GameObject go)
    {
        var clone = GameObject.Instantiate(go) as GameObject;
        clone.transform.parent = go.transform.parent;
        clone.transform.localPosition = go.transform.localPosition;
        clone.transform.localScale = go.transform.localScale;
        return clone;
    }
}
