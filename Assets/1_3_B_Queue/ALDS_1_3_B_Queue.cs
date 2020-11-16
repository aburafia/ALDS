using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ALDS_1_3_B_Queue : MonoBehaviour
{
    struct QProc {
        public int q;
        public string name;
        public QProc(string name,int q ) { this.q = q; this.name = name; }
        public void clear() { q = 0; name = null; }
    };

    RectTransform obj;
    Text lableText;

    static int RINGSIZE = 10;
    static int CPUTIME = 100;

    int head = 0;
    int tail = 0;

    QProc[] ringList = new QProc[RINGSIZE];
    RectTransform[] stackListUI = new RectTransform[10];

    // Start is called before the first frame update
    IEnumerator Start()
    {
        obj = GameObject.Find("Panel").GetComponent<RectTransform>();
        lableText = GameObject.Find("Text").GetComponent<Text>();

        var yCenter = GetComponent<RectTransform>().sizeDelta.y / 2;
        obj.position = new Vector3(obj.position.x, yCenter, 0);

        //testdata
        enqueue(new QProc("p1", 150));
        enqueue(new QProc("p2", 80));
        enqueue(new QProc("p3", 200));
        enqueue(new QProc("p4", 350));
        enqueue(new QProc("p5", 20));

        yield return StartCoroutine(initUI(RINGSIZE));

        render();
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(calc());

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

    IEnumerator calc()
    {
        int progressTime = 0;
        while (!isEmpty())
        {
            //swift のguard的な？
            if (dequeue() is QProc d) {

                if(d.q > CPUTIME)
                {
                    d.q -= CPUTIME;
                    enqueue(d);
                }
                else {
                    lableText.text += "\n" + d.name + " is finish. time is " + (progressTime + d.q).ToString();
                }

                progressTime += CPUTIME;
            }

            if (isEmpty())
            {
                lableText.text += "\n finish";
            }

            render();
            yield return new WaitForSeconds(1f);

        }
        yield return null;
    }

    void enqueue(QProc d)
	{
        if (isFull())
        {
            lableText.text += "\nError:overflow";
            forceStop();
            return;
        }

        ringList[tail] = d;

        //tail = tail + 1 == RINGSIZE ? 0 : tail + 1;
        tail = (tail + 1) % RINGSIZE;
    }

    QProc? dequeue()
    {
        if (isEmpty())
        {
            lableText.text += "\nError:null";
            forceStop();
            return null;
        }

        var d = ringList[head];
        ringList[head].clear();

        //head = head + 1 == RINGSIZE ? 0 : head + 1;
        head = (head + 1) % RINGSIZE;

        return d;
    }

    bool isFull()
    {
        //tailを一つすすめたらheadと同じになる
        return ((tail + 1) % RINGSIZE) == head;
    }

    bool isEmpty()
    {
        return head == tail;
    }

    void forceStop()
    {
        //クリアがあった方がよい
        head = 0;
        tail = 0;
    }

    void render()
	{
        for(int i =0; i < RINGSIZE; i++)
		{
            stackListUI[i].GetComponent<Image>().color = Color.white;
            this.stackListUI[i].GetComponentInChildren<Text>().text = ringList[i].name + "\n" + ringList[i].q.ToString();
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
