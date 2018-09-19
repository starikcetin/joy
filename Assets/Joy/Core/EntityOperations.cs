using UnityEngine;

namespace Joy.Core
{
    public static class EntityOperations
    {
        public static void EnableEntity(Entity entity)
        {
            if (entity.gameObject.activeSelf == true) return;

            entity.gameObject.SetActive(true);
            Manager.EntityEnabled(entity);
        }

        public static void DisableEntity(Entity entity)
        {
            if (entity.gameObject.activeSelf == false) return;

            entity.gameObject.SetActive(false);
            Manager.EntityDisabled(entity);
        }

        // @TODO: Creating from Entity works, but maybe consider an archetype class?
        public static Entity CreateEntity(Entity original)
        {
            if (original == null) return null;

            var newEntity = Object.Instantiate(original.gameObject).GetComponent<Entity>();
            Manager.EntityCreated(newEntity);
            return newEntity;
        }

        public static void DestroyEntity(Entity entity)
        {
            if (entity == null) return;

            Object.DestroyImmediate(entity.gameObject, false);
            Manager.EntityDestroyed(entity);
        }

        public static T AddComponent<T>(Entity entity) where T : Component
        {
            if (entity == null) return null;

            var newComponent = entity.gameObject.AddComponent<T>();
            Manager.ComponentAdded<T>(entity);
            return newComponent;
        }

        public static void RemoveComponent<T>(Entity entity) where T : Component
        {
            if (entity == null) return;

            T[] components = entity.GetComponents<T>();
            foreach (var c in components)
            {
                Object.DestroyImmediate(c, false);
            }

            Manager.ComponentRemoved<T>(entity);
        }
    }
}
