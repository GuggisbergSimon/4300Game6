using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class InkBehavior : MonoBehaviour
{
    [SerializeField] private float time;
    private float _timer;
    private Vector2 maxScale;

    private void Start()
    {
        StartCoroutine(InkExplosion(time));
    }

    IEnumerator InkExplosion(float time)
    {
        _timer = 0;
        Vector3 initScale = transform.localScale;
        while (true)
        {
            transform.localScale += Vector3.Lerp(initScale, maxScale, _timer/time);
            _timer += Time.deltaTime;
            yield return null;
        }
    }
}
