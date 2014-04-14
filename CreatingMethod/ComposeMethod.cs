using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    public class MyExpandableList
    {
        private object[] elements_ = new object[10];
        private bool readOnly_;
        private int size_ = 0;

        public void Add(object child)
        {
            if (readOnly_)
                return;

            if (atCapacity())
            {
                grow();
            }

            addElement(child);
        }

        private void addElement(object element)
        {
            elements_[size_] = element;
            size_++;
        }

        private void grow()
        {
            object[] newElements = new object[elements_.Length + 10];
            for (int i = 0; i < size_; i++)
            {
                newElements[i] = elements_[i];
            }
            elements_ = newElements;
        }

        private bool atCapacity()
        {
            int newSize = elements_.Length + 1;
            if (newSize > elements_.Length)
            {
                return false;
            }
            return true;
        }

        public bool ReadOnly
        {
            get { return readOnly_; }
            set { readOnly_ = value; }
        }
    }
}
