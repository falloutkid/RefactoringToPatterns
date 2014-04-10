using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    public abstract class AttributeDescriptor
    {
        protected AttributeDescriptor(string name, Type use_class, Type type) { }
        public static AttributeDescriptor forInteger(string name, Type use_class)
        {
            return new DefaultDescriptor(name, use_class, typeof(Int32));
        }
        public static AttributeDescriptor forDatetime(string name, Type use_class)
        {
            return new DefaultDescriptor(name, use_class, typeof(DateTime));
        }
    }


    public class BooleanDescriptor : AttributeDescriptor
    {
        public BooleanDescriptor(string name, Type use_class, Type type)
            : base(name, use_class, type)
        {
        }
    }

    class DefaultDescriptor : AttributeDescriptor
    {
        public DefaultDescriptor(string name, Type use_class, Type type)
            : base(name, use_class, type)
        {
        }
    }

    public class ReferenceDescriptor : AttributeDescriptor
    {
        public ReferenceDescriptor(string name, Type use_class, Type type)
            : base(name, use_class, type)
        {
        }
    }

    class Client
    {
        protected List<AttributeDescriptor> createAttributeDescriptors()
        {
            List<AttributeDescriptor> result = new List<AttributeDescriptor>();
            result.Add(AttributeDescriptor.forInteger("remoteId", this.GetType()));
            result.Add(AttributeDescriptor.forDatetime("createdDate", this.GetType()));
            result.Add(AttributeDescriptor.forDatetime("lastChangedDate", this.GetType()));
            return result;
        }
    }
}
