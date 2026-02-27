using UnityEngine;
using System.Collections;

namespace CardMatch.Infrastructure
{
    public sealed class CoroutineRunner : MonoBehaviour
    {
        public void Run(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}