using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Joy.Core
{
    // @TODO: Move cache/register scene entity methods to here.

    // @TODO: Create a composition root base class with repetitive stuff handled?

	// @TODO: Create a partial class on a seperate file with a .Debug extension for Debug nested class.
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

        public static Entity[] Query(Filter fooFilter)
        {
            /*
             * TODO PERFORMANCE: Optimize so we don't call ToArray() every frame
             * options: take in a fill array or make a double buffer system.
             */
            return FiltersAndGroups[fooFilter].Members.ToArray();
        }



        public static class ___DEBUG___
        {
            public static IReadOnlyDictionary<Filter, Group> AllGroups =>
                new Dictionary<Filter, Group>(FiltersAndGroups);
        }
    }
}
