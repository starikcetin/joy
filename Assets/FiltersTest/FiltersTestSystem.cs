using starikcetin.Joy.Core;

namespace FiltersTest
{
    public class FiltersTestSystem : ISystem
    {
        public Filter[] Filters { get; } =
        {
            Filter,
            FilterA,
            FilterB
        };

        private static readonly Filter Filter = new Filter()
            .AllOf(typeof(All_1_Component), typeof(All_2_Component))
            .AnyOf(typeof(Any_1A_Component), typeof(Any_1B_Component))
            .AnyOf(typeof(Any_2A_Component), typeof(Any_2B_Component))
            .NoneOf(typeof(None_1_Component), typeof(None_2_Component));

        private static readonly Filter FilterA = new Filter();
        private static readonly Filter FilterB = new Filter();

        public void Update()
        {
            var entities = Manager.Query(Filter);
            var entitiesA = Manager.Query(FilterA);
            var entitiesB = Manager.Query(FilterB);
        }
    }
}
