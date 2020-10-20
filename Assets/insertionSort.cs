using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insertionSort : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("genRandom");
    }

    IEnumerator insertionSoftFunc(){
        yield return null;
    }

    IEnumerator genRandom()
    {
        float lastXPosition = 0;

        while (lastXPosition < 10)
        {
            lastXPosition += 0.1f;
            var currentObj = clone(obj);
            currentObj.transform.localPosition += obj.transform.localPosition + new Vector3(lastXPosition, 0, 0);
            currentObj.transform.localScale += new Vector3(0, Random.Range(0.1f, 5), 0);

            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
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
