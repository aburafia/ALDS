using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ALDS_1_2_C_StableSort : MonoBehaviour
{
    RectTransform obj;
    Text lableText;

    RectTransform[] objsOrig = new RectTransform[10];
    RectTransform[] objs = new RectTransform[10];
    RectTransform[] objs2 = new RectTransform[10];

    // Start is called before the first frame update
    IEnumerator Start()
    {
        obj = GameObject.Find("Panel").GetComponent<RectTransform>();
        lableText = GameObject.Find("Text").GetComponent<Text>();

        var yCenter = GetComponent<RectTransform>().sizeDelta.y / 2;
        obj.position = new Vector3(obj.position.x, yCenter, 0);

        yield return StartCoroutine(genRandom(10));
        yield return StartCoroutine(bubbleSort(objs));
        yield return StartCoroutine(selectSort(objs2));

        isStable(objs, objs2);
    }

    IEnumerator insertionSoftFunc()
    {
        yield return null;
    }

    IEnumerator genRandom(int count)
    {
        var markColor = new Color[] { Color.red, Color.yellow, Color.cyan, Color.green };
        var markName = new string[] { "R", "Y", "C", "G" };

        for (int i = 0; i < count; i++)
        {
            var num = Random.Range(0, 13);
            var mark = Random.Range(0, 4);

            objs[i] = clone(obj);

            objs[i].position = numToPos(i, objs);
            objs[i].position += new Vector3(0, num * 10,0);

            objs[i].GetComponent<Image>().color = markColor[mark];
            objs[i].GetComponentInChildren<Text>().text = markName[mark] + num.ToString();

            objs2[i] = clone(objs[i]);
            objs2[i].position -= new Vector3(0, 200, 0);
            objsOrig[i] = clone(objs[i]);
            objsOrig[i].position += new Vector3(0, 200, 0);

            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    int count = 0;

    IEnumerator bubbleSort(RectTransform[] objs)
    {
        int head = 0;
        bool flg = true;
        while (flg)
        {
            flg = false;

            for (int i = objs.Length - 1; i > head; i--)
            {
                if (objs[i].position.y < objs[i - 1].position.y)
                {
                    var tmp = objs[i];
                    objs[i] = objs[i - 1];
                    objs[i - 1] = tmp;

                    objs[i - 1].position = numToPos(i - 1,objs);
                    objs[i].position = numToPos(i, objs);

                    flg = true;

                    yield return new WaitForSeconds(0.2f);
                }
            }

            head++;
        }

        yield return null;
    }

    IEnumerator selectSort(RectTransform[] objs)
    {
        for (int i=0;i<objs.Length; i++) 
        {
            int min = i;

            for (int j = i; j < objs.Length; j++)
            {
                min = objs[min].position.y > objs[j].position.y ? j : min;
            }

            var tmp = objs[i];
            objs[i] = objs[min];
            objs[min] = tmp;

            objs[min].position = numToPos(min, objs);
            objs[i].position = numToPos(i, objs);

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    void isStable(RectTransform[] objs, RectTransform[] objs2)
    {
        for (int i = 0; i < 10; i++)
        {
            if(objs[i].GetComponentInChildren<Text>().text != objs2[i].GetComponentInChildren<Text>().text)
            {
                lableText.text = "not stable";
                return;
            }
            lableText.text = "stable";
        }
    }

    //添字から位置を導出
    public Vector3 numToPos(int i, RectTransform[] objs)
    {
        //間隔
        float dXPosition = 50f;

        //初期位置
        float firstXpos = 100;


        return new Vector3(firstXpos + dXPosition * i, objs[i].position.y, 0) ;
    }

    public RectTransform clone(RectTransform go)
    {
        var cloneGo = GameObject.Instantiate(go.gameObject) as GameObject;
        cloneGo.transform.parent = go.transform.parent;

        var clone = cloneGo.GetComponent<RectTransform>();
        clone.position = go.position;
        return clone;
    }
}
