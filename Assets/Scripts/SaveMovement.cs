using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveMovement : MonoBehaviour
{

    [Serializable]
    public class Tupel
    {
        public Tupel(float time, float value)
        {
            Time = time;
            Value = value;
        }

        public float Time=0f;
        public float Value = 0f;
    }

    [SerializeField]
    private bool _isSaving=true;

    private List<Tupel> _rotation = new List<Tupel>();
    public List<Tupel> Rotation
    {
        get { return _rotation; }
    }

    [SerializeField]
    private List<Tupel> _jump = new List<Tupel>();
    public List<Tupel> Jump
    {
        get { return _jump; }
    }



    public void AddJump(Tupel jump)
    {
        if (_isSaving)
        {
            _jump.Add(jump);
        }
    }

    public void AddRotation(Tupel rotate)
    {
        if (_isSaving)
        {
            _rotation.Add(rotate);
        }

    }
  
}
