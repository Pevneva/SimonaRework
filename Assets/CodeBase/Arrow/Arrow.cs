using System.Collections;
using UnityEngine;

namespace CodeBase.Arrow
{
    public class Arrow : MonoBehaviour
    {
        private Coroutine _enablingCoroutine;
        private const int LifeTime = 3;

        private void OnEnable() => 
            _enablingCoroutine = StartCoroutine(StartEnablingTimer());

        private void OnDisable()
        {
            if (_enablingCoroutine != null) 
                StopCoroutine(_enablingCoroutine);
        }

        private IEnumerator StartEnablingTimer()
        {
            yield return new WaitForSeconds(LifeTime);
            gameObject.SetActive(false);
        }
    }
}