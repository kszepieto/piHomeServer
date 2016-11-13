using System;

namespace piHome.Utils.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string ID { get; private set; }
        public Type ObjecType { get; private set; }

        public EntityNotFoundException(string id, Type objecType)
        {
            ID = id;
            ObjecType = objecType;
        }
    }
}