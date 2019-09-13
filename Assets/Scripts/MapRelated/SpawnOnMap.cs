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

		List<GameObject> spawnedObjects;
        List<Vector2d> locations;

        void Start()
		{
            Instance = this;
			spawnedObjects = new List<GameObject>();
            locations = new List<Vector2d>();
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

            if (Input.GetKeyDown(KeyCode.M))
            {
                for (int i = 0; i < spawnedObjects.Count; i++)
                {
                    DataSaver.SaveMapUnits(spawnedObjects[i]);
                }
            }
        }
        public void SpawnObject(GameObject type, Vector2d location, float altitude)
        {
            MapUnit instance = Instantiate(type).GetComponent<MapUnit>();
            instance.transform.localPosition = _map.GeoToWorldPosition(location, true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            instance.latitude = location.x;
            instance.longitude = location.y;
            instance.altitude = altitude;
            spawnedObjects.Add(instance.gameObject);
            locations.Add(location);
        }
	}
}