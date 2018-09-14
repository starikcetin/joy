using System;
using System.Collections.Generic;
using System.Linq;
using Joy.UnityExtensions;

namespace Joy.Core
{
    public class Filter
    {
        private readonly HashSet<Type> _all = new HashSet<Type>();
        private readonly HashSet<HashSet<Type>> _any = new HashSet<HashSet<Type>>();
        private readonly HashSet<Type> _none = new HashSet<Type>();

        /// <summary>
        /// The entity must have "all" of these component types. <para/>
        /// Returns this instance (for chaining).
        /// </summary>
        public Filter AllOf(params Type[] componentTypes)
        {
            _all.UnionWith(componentTypes);
            return this;
        }

        /// <summary>
        /// The entity must have "at least one" of these component types. <para/>
        /// Returns this instance (for chaining).
        /// </summary>
        public Filter AnyOf(params Type[] componentTypes)
        {
            /*
             * @BUG hashset cannot decide if child hashsets contain the same elements.
             * we need a custom collection or a custom equality comparer.
             */

            _any.Add(new HashSet<Type>(componentTypes));
            return this;
        }

        /// <summary>
        /// The entity must have "none" of these component types. <para/>
        /// Returns this instance (for chaining).
        /// </summary>
        public Filter NoneOf(params Type[] componentTypes)
        {
            _none.UnionWith(componentTypes);
            return this;
        }

        /// <summary>
        /// Returns true if the Entity satisfies this Filter's requirements.
        /// </summary>
        public bool IsCompatible(Entity entity)
        {
            return AllCheck(entity) && NoneCheck(entity) && AnyCheck(entity);
        }

        //
        // Compatibility checks
        //

        private bool AllCheck(Entity entity) => _all.All(entity.Has);

        private bool AnyCheck(Entity entity) => _any.All(a => a.Any(entity.Has));

        private bool NoneCheck(Entity entity) => !_none.Any(entity.Has);
    }
}
