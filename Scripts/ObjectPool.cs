using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<Object> : MonoBehaviour where Object : MonoBehaviour
{
	private List<Object> _poolObjects = new List<Object>();

	protected void Initialize(Object prefab, int capacity)
	{
		for (int i = 0; i < capacity; i++)
		{
            Object spawnObject = Instantiate(prefab);
			spawnObject.gameObject.SetActive(false);

			_poolObjects.Add(spawnObject);
		}
	}

	protected bool TryGetDisabledObject(out Object disabledObject)
	{
		disabledObject = _poolObjects.FirstOrDefault(poolObject => poolObject.gameObject.activeSelf == false);

		return disabledObject != null;
	}
}