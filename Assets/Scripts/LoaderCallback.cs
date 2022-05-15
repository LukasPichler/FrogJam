using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool _first = true;

    [SerializeField]
    private float _timeWaiting=3f;

    // Update is called once per frame
    void Update()
    {

        if (_first)
        {
            _first = false;
            StartCoroutine(WaitForCallback());
        }
    }

    private IEnumerator WaitForCallback()
    {
        yield return new WaitForSeconds(_timeWaiting);
        Loader.LoaderCallback();
    }
}
