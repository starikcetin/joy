using System.Collections.Generic;
using UnityEngine;

namespace Joy.Core
{
    public static class Manager
    {
        private static readonly Dictionary<Filter, Group> FiltersAndGroups = new Dictionary<Filter, Group>();
        private static readonly List<ISystem> Systems = new List<ISystem>();


        public static void RegisterSystem(ISystem system)
        {
            Systems.Add(system);
            RegisterFilters(system.Filters);
        }

        public static void UpdateSystems()
        {
            Systems.ForEach(s => s.Update());
        }



        public static void EntityCreated(Entity entity)
        {
            UpdateGroupMemberships(entity);
        }

        public static void EntityEnabled(Entity entity)
        {
            UpdateGroupMemberships(entity);
        }

        public static void ComponentAdded<T>(Entity entity) where T : Component
        {
            UpdateGroupMemberships(entity);
        }

        public static void ComponentRemoved<T>(Entity entity) where T : Component
        {
            UpdateGroupMemberships(entity);
        }

        public static void EntityDisabled(Entity entity)
        {
            RemoveFromAllGroups(entity);
        }

        public static void EntityDestroyed(Entity entity)
        {
            RemoveFromAllGroups(entity);
        }



        private static void RegisterFilters(Filter[] filters)
        {
            foreach (var filter in filters)
            {
                if (!FiltersAndGroups.ContainsKey(filter))
                {
                    FiltersAndGroups.Add(filter, new Group());
                }
            }
        }

        private static void UpdateGroupMemberships(Entity entity)
        {
            foreach (var filterAndGroup in FiltersAndGroups)
            {
                var filter = filterAndGroup.Key;
                var group = filterAndGroup.Value;

                if (filter.IsCompatible(entity))
                {
                    group.RegisterEntity(entity);
                }
                else
                {
                    group.UnregisterEntity(entity);
                }
            }
        }

        private static void RemoveFromAllGroups(Entity entity)
        {
            foreach (var filterAndGroup in FiltersAndGroups)
            {
                var group = filterAndGroup.Value;
                group.UnregisterEntity(entity);
            }
        }

        public static IReadOnlyCollection<Entity> Query(Filter fooFilter)
        {
            return FiltersAndGroups[fooFilter].Members;
        }



        public static class ___DEBUG___
        {
            public static IReadOnlyDictionary<Filter, Group> AllGroups =>
                new Dictionary<Filter, Group>(FiltersAndGroups);
        }
    }
}
