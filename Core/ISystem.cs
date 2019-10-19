namespace starikcetin.Joy.Core
{
    public interface ISystem
    {
        Filter[] Filters { get; }
        void Update();
    }
}
