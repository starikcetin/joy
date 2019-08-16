using Joy.Core;
using UnityEngine;

namespace Joy.Example
{
    public class ExampleSystem : ISystem
    {
        public Filter[] Filters { get; } =
        {
            FooFilter,
            BarFilter
        };

        private static Filter FooFilter { get; } = new Filter()
            .AllOf(typeof(int), typeof(string))
            .AnyOf(typeof(float), typeof(double))
            .NoneOf(typeof(decimal))
            .AllOf(typeof(int), typeof(string))
            .AnyOf(typeof(float), typeof(double))
            .NoneOf(typeof(decimal));

        private static Filter BarFilter { get; } = new Filter()
            .AllOf(typeof(int), typeof(string))
            .AnyOf(typeof(float), typeof(double))
            .NoneOf(typeof(decimal));

        public void Update()
        {
            var fooEntities = Manager.Query(FooFilter);
            foreach (var fooEntity in fooEntities)
            {
                var tr = EntityOperations.AddComponent<TrailRenderer>(fooEntity);
                tr.emitting = true;
            }
        }
    }
}
