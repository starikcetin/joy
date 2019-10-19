using System.Collections.Generic;

namespace starikcetin.Joy.Core
{
    public class Group
    {
        private readonly HashSet<Entity> _members = new HashSet<Entity>();

        public IReadOnlyCollection<Entity> Members => _members;

        public void RegisterEntity(Entity entity)
        {
            _members.Add(entity);
        }

        public void UnregisterEntity(Entity entity)
        {
            _members.Remove(entity);
        }
    }
}
