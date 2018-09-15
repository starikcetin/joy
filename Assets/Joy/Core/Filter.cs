using System;
using System.Collections.Generic;
using System.Linq;

namespace Joy.Core
{
	// @TODO: Create a partial class on a seperate file with a .Debug extension for Debug nested class.
	
    // @TODO: Implement IEquatable<Filter> with filter value equality. Otherwise all filters will be different due to reference comparison.
    public class Filter
    {
        private readonly HashSet<Type> _allOf = new HashSet<Type>();
        private readonly HashSet<HashSet<Type>> _anyOfs = new HashSet<HashSet<Type>>();
        private readonly HashSet<Type> _noneOf = new HashSet<Type>();

        public Filter()
        {
            ___DEBUG___ = new Debug(this);
        }

        /// <summary>
        /// The entity must have "all" of these component types. <para/>
        /// Returns this instance (for chaining).
        /// </summary>
        public Filter AllOf(params Type[] componentTypes)
        {
            _allOf.UnionWith(componentTypes);
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

            _anyOfs.Add(new HashSet<Type>(componentTypes));
            return this;
        }

        /// <summary>
        /// The entity must have "none" of these component types. <para/>
        /// Returns this instance (for chaining).
        /// </summary>
        public Filter NoneOf(params Type[] componentTypes)
        {
            _noneOf.UnionWith(componentTypes);
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

        private bool AllCheck(Entity entity) => _allOf.All(entity.Has);

        private bool AnyCheck(Entity entity) => _anyOfs.All(a => a.Any(entity.Has));

        private bool NoneCheck(Entity entity) => !_noneOf.Any(entity.Has);



        public readonly Debug ___DEBUG___;

        public class Debug
        {
            private readonly Filter _filter;

            public Debug(Filter filter)
            {
                _filter = filter;
            }

            public Type[] AllOf => _filter._allOf.ToArray();

            public Type[][] AnyOfs => _filter._anyOfs.Select(x => x.ToArray()).ToArray();

            public Type[] NoneOf => _filter._noneOf.ToArray();
        }
    }
}
