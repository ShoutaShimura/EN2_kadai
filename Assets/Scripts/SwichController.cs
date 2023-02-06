using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwichController : MonoBehaviour
{

    [SerializeField] UnityEvent _onEnter = default;
    /// <summary>àÍìxìÆçÏÇµÇΩÇ©Ç«Ç§Ç©</summary>
    bool _isFinished = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!_isFinished)
            {
                _onEnter.Invoke();
                _isFinished = true;
            }
        }
    }
}
