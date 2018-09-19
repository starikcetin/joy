using System;
using UnityEngine;

namespace Joy.Core
{
    /// <summary>
    /// This will be attached to a GameObject to make it an Entity for Joy framework.
    /// </summary>
    public class Entity : MonoBehaviour, IEquatable<Entity>
    {
        //
        // Unique Entity ID assignment
        //

        private static int _uidHolder = 0;
        private readonly int _uid = _uidHolder++;

        //
        // Equality overrides and IEquatable<Entity> implementation
        // This makes HashSet<Entity> work as a proper hashset.
        //

        public bool Equals(Entity other)
        {
            return other != null && this._uid == other._uid;
        }

        public override bool Equals(object obj)
        {
            var castedObj = obj as Entity;
            return castedObj != null && this.Equals(castedObj);
        }

        public override int GetHashCode()
        {
            return _uid;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !Equals(left, right);
        }

        //
        // Joy helpers
        //

        public bool Has(Type t)
        {
            return GetComponent(t) != null;
        }

        public bool Has<T>() where T : Component
        {
            return GetComponent<T>() != null;
        }
    }
}
