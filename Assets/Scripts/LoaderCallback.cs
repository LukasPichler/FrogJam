using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool _first = true;

    // Update is called once per frame
    void Update()
    {

        if (_first)
        {
            _first = false;
            Loader.LoaderCallback();
        }
    }
}
