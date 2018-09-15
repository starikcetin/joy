using System;
using System.Collections.Generic;
using System.Linq;
using Joy.Core;
using UnityEngine;

namespace Joy.Debugging
{
    public class JoyDebugger : MonoBehaviour
    {
        [Tooltip("If true, keeps updating debug info every frame. Otherwise only updates one time at the start.")]
        public bool UpdateDebugInfoEachFrame = false;

        [Space]
        public List<FilterGroupInspector> FilterGroups = new List<FilterGroupInspector>();

        private void Start()
        {
            UpdateEntitiesList();
        }

        private void Update()
        {
            if (UpdateDebugInfoEachFrame)
            {
                UpdateEntitiesList();
            }
        }

        private void UpdateEntitiesList()
        {
            FilterGroups = Manager.___DEBUG___.AllGroups.Select(fg => new FilterGroupInspector(fg)).ToList();
        }
    }

    [Serializable]
    public class FilterGroupInspector
    {
        [SerializeField] public FilterInspector Filter;
        [SerializeField] public List<Entity> Group;

        public FilterGroupInspector(KeyValuePair<Filter, Group> filterGroupPair)
        {
            Filter = new FilterInspector(filterGroupPair.Key);
            Group = filterGroupPair.Value.Members.ToList();
        }
    }

    [Serializable]
    public class FilterInspector
    {
        [SerializeField] public List<TypeInspector> AllOf;
        [SerializeField] public List<AnyOfInspector> AnyOfs;
        [SerializeField] public List<TypeInspector> NoneOf;

        public FilterInspector(Filter filter)
        {
            AllOf = filter.___DEBUG___.All.Select(t => new TypeInspector(t)).ToList();
            AnyOfs = filter.___DEBUG___.Any.Select(a => new AnyOfInspector(a)).ToList();
            NoneOf = filter.___DEBUG___.None.Select(t => new TypeInspector(t)).ToList();
        }
    }

    [Serializable]
    public class AnyOfInspector
    {
        [SerializeField] public List<TypeInspector> AnyOf;

        public AnyOfInspector(IEnumerable<Type> mess)
        {
            AnyOf = mess.Select(t => new TypeInspector(t)).ToList();
        }
    }

    [Serializable]
    public class TypeInspector
    {
        [SerializeField] public string Name;
        [SerializeField] public string FullName;
        [SerializeField] public string Namespace;
        [SerializeField] public string AssemblyFullName;
        [SerializeField] public string AssemblyLocation;

        public TypeInspector(Type type)
        {
            Name = type.Name;
            FullName = type.FullName;
            Namespace = type.Namespace;
            AssemblyFullName = type.Assembly.FullName;
            AssemblyLocation = type.Assembly.Location;
        }
    }
}
