namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;

	public class SpawnOnMap : MonoBehaviour
	{
        public static SpawnOnMap Instance;

		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		float spawnScale = 100f;

		[SerializeField]
		GameObject[] objects;

		List<GameObject> spawnedObjects;
        List<Vector2d> locations;

        void Start()
		{
            Instance = this;
			spawnedObjects = new List<GameObject>();
            locations = new List<Vector2d>();
		}
        public void SpawnObject(int type, Vector2d location)
        {
            var instance = Instantiate(objects[type]);
            instance.transform.localPosition = _map.GeoToWorldPosition(location, true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            spawnedObjects.Add(instance);
            locations.Add(location);
        }
		private void Update()
		{
			int count = spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = spawnedObjects[i];
				var location = locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
			}
		}
	}
}