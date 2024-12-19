using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Coroutine _coroutine;

    public bool IsActive => _coroutine != null;

    public void Activate(float speed)
    {
        _coroutine = StartCoroutine(Spinning(speed));
    }

    public void Deactivate()
    {
        if (_coroutine == null)
            throw new System.ArgumentNullException("Not found coroutine");

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator Spinning(float speed)
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
    }
}
