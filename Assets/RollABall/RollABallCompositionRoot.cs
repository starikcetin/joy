using Joy.Core;
using RollABall.Systems;
using UnityEditor;
using UnityEngine;

namespace RollABall
{
	public class RollABallCompositionRoot : MonoBehaviour
	{
		private Entity[] _entitiesAlreadyInScene;

		private void Awake()
		{
			_entitiesAlreadyInScene = FindObjectsOfType<Entity>();
		}

		private void Start ()
		{
			Manager.RegisterSystem(new WriteKeyboardInput());
			Manager.RegisterSystem(new MoveWithKeyboardInput());

			foreach (var entity in _entitiesAlreadyInScene)
			{
				Manager.EntityCreated(entity);
				Debug.Log("scene entity registered: " + entity.gameObject.name);
			}
		}

		private void Update ()
		{
			Manager.UpdateSystems();
		}
	}
}
