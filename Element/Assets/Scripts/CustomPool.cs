using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomPool
{
    public struct CustomPool<T> where T : MonoBehaviour
    {
        T _prefab;
        List<T> _objects;
        public List<T> Objects { get { return _objects; } }
        Transform _parent;

        public CustomPool(T prefab, Transform parent)
        {
            _prefab = prefab;
            _objects = new List<T>();
            _parent = parent;
        }
    
        public T Get()
        {
            var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (obj == null)
            {
                obj = Create();
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private T Create()
        {
            var obj = GameObject.Instantiate(_prefab, _parent);
            _objects.Add(obj);
            return obj;
        }
    }
}