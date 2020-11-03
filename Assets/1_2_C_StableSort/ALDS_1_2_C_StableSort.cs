using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ALDS_1_2_C_StableSort : MonoBehaviour
{
    RectTransform obj;
    Text lableText;

    RectTransform[] objs;

    float yCenter = 0;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        obj = GameObject.Find("Panel").GetComponent<RectTransform>();
        lableText = GameObject.Find("Text").GetComponent<Text>();

        yCenter = GetComponent<RectTransform>().sizeDelta.y / 2;


        int count = 10;

        objs = new RectTransform[count];
        yield return StartCoroutine(genRandom(count));
        yield return StartCoroutine(selectSort());
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

            objs[i].position = numToPos(i);
            objs[i].sizeDelta += new Vector2(0, Random.Range(0f, 300f));

            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    int count = 0;

    IEnumerator selectSort()
    {
        for (int i=0;i<objs.Length; i++) 
        {
            int min = i;

            for (int j = i; j < objs.Length; j++)
            {
                min = objs[min].sizeDelta.y > objs[j].sizeDelta.y ? j : min;
            }

            var tmp = objs[i];
            objs[i] = objs[min];
            objs[min] = tmp;

            objs[min].position = numToPos(min);
            objs[i].position = numToPos(i);

            count++;
            lableText.text = count.ToString();

            yield return new WaitForSeconds(0.2f);

            objs[i].GetComponent<Image>().color = Color.red;
        }

        yield return null;
    }

    //添字から位置を導出
    public Vector3 numToPos(int i)
    {
        //間隔
        float dXPosition = 50f;

        //初期位置
        float firstXpos = 100;


        return new Vector3(firstXpos + dXPosition * i, yCenter, 0) ;
    }

    public RectTransform clone(RectTransform go)
    {
        var cloneGo = GameObject.Instantiate(go.gameObject) as GameObject;
        cloneGo.transform.parent = go.transform.parent;

        var clone = cloneGo.GetComponent<RectTransform>();
        clone.position = go.position;
        clone.sizeDelta = go.sizeDelta;
        return clone;
    }
}
