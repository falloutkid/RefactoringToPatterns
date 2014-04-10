using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    public abstract class AttributeDescriptor
    {
        protected AttributeDescriptor() { }
    }


    public class BooleanDescriptor : AttributeDescriptor
    {
        public BooleanDescriptor()
            : base()
        {
        }
    }

    public class DefaultDescriptor : AttributeDescriptor
    {
        public DefaultDescriptor()
            : base()
        {
        }
    }

    public class ReferenceDescriptor : AttributeDescriptor
    {
        public ReferenceDescriptor()
            : base()
        {
        }
    }
}
