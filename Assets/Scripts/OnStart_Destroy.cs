using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart_Destroy : MonoBehaviour
{
    [SerializeField]
    private float _delay = 5.0f;
    private float _timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _delay)
        {
            Destroy(gameObject);
        }
    }
}
