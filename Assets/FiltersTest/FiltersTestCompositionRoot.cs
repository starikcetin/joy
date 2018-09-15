using Joy.Core;
using UnityEngine;

namespace FiltersTest
{
	public class FiltersTestCompositionRoot : MonoBehaviour
	{
		private Entity[] _entitiesAlreadyInScene;

		private void Awake()
		{
			RecordSceneEntities();
			RegisterSystems();
		}

		private void Start ()
		{
			RegisterSceneEntities();
		}

		private void Update ()
		{
			Manager.UpdateSystems();
		}


		private void RegisterSceneEntities()
		{
			foreach (var entity in _entitiesAlreadyInScene)
			{
				Manager.EntityCreated(entity);
				Debug.Log("scene entity registered: " + entity.gameObject.name);
			}
		}

		private void RecordSceneEntities()
		{
			_entitiesAlreadyInScene = FindObjectsOfType<Entity>();
		}

		private void RegisterSystems()
		{
			Manager.RegisterSystem(new FiltersTestSystem());
		}
	}
}
