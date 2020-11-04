using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ALDS_1_3_A_Stack : MonoBehaviour
{
    RectTransform obj;
    Text lableText;

    int stackPointer = -1;
    int[] stackList;
    RectTransform[] stackListUI = new RectTransform[10];

    string optext = "2 3 + 2 5 - *";

    // Start is called before the first frame update
    IEnumerator Start()
    {
        obj = GameObject.Find("Panel").GetComponent<RectTransform>();
        lableText = GameObject.Find("Text").GetComponent<Text>();

        var yCenter = GetComponent<RectTransform>().sizeDelta.y / 2;
        obj.position = new Vector3(obj.position.x, yCenter, 0);

        stackList = Enumerable.Repeat<int>(-1, 10).ToArray();

        yield return StartCoroutine(initUI(10));
        yield return StartCoroutine(opExec());

    }

    IEnumerator initUI(int count)
    {

        for (int i = 0; i < count; i++)
        {
            stackListUI[i] = clone(obj);
            stackListUI[i].position = numToPos(i, stackListUI);
            stackListUI[i].position += new Vector3(50, 0);
        }

        yield return null;
    }


    IEnumerator opExec()
    {
        var opList = optext.Split(' ');

        for (int i = 0; i < opList.Length; i++)
        {
            int number = 0;
            if( int.TryParse(opList[i], out number) )
			{
                push(number);
            }
            else
			{
                //計算対象を赤く
                stackListUI[stackPointer].GetComponent<Image>().color = Color.red;
                stackListUI[stackPointer - 1].GetComponent<Image>().color = Color.red;
                yield return new WaitForSeconds(1f);

                if (opList[i] == "+")
				{
                    var r = pop() + pop();
                    push(r);
				}
                if (opList[i] == "*")
                {
                    var r = pop() * pop();
                    push(r);
                }
                if (opList[i] == "-")
                {
                    var r = pop() - pop();
                    push(r);
                }
            }

            render();
            yield return new WaitForSeconds(1f);

        }
        yield return null;
    }

    void push(int op)
	{
        if (stackList.Length <= stackPointer) return;

        stackPointer++;
        stackList[stackPointer] = op;

    }

    int pop()
    {
        if (stackPointer < 0) return -1;

        var op = stackList[stackPointer];

        stackList[stackPointer] = -1;


        stackPointer--;
        return op;
    }

    void render()
	{
        lableText.text = "optext=" + optext + "\nstackPointer=" + stackPointer.ToString();

        for(int i =0; i < stackList.Length; i++)
		{
            stackListUI[i].GetComponent<Image>().color = Color.white;

            this.stackListUI[i].GetComponentInChildren<Text>().text = stackList[i] == -1 ? "" : stackList[i].ToString();
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
