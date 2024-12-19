using System.Collections;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    private Coroutine _coroutine;

    public bool IsActive => _coroutine != null;

    public void StartSpin(float speed)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Spinning(speed));
    }

    public void StopSpin()
    {
        if (_coroutine == null)
            throw new System.ArgumentNullException("Not found coroutine");

        StopCoroutine(_coroutine);
    }

    private IEnumerator Spinning(float speed)
    {
        while (true)
        {
            float rotationAmount = speed * Time.deltaTime;
            transform.Rotate(0, 0, rotationAmount);
            yield return new WaitForEndOfFrame();
        }
    }
}
