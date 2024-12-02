using System.Collections;
using UnityEngine;

namespace Game.Interfaces
{
    public interface ICharacterView
    {
        public IEnumerator MoveToTarget(GameObject target);
        public IEnumerator MoveToStartPosition(GameObject target);
    }
}