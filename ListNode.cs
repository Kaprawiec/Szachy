using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szachy_menu
{
    public class ListNode
    {
        private string _data;

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private ListNode _next;

        public ListNode Next
        {
            get { return _next; }
            set { _next = value; }
        }

        public ListNode(string data, ListNode next)
        {
            _data = data;
            _next = next;
        }

        public ListNode(string data)
            : this(data, null)
        {
        }

        public ListNode()
            : this(string.Empty, null)
        {
        }
    }
    public class SimpleList
    {
        private ListNode _head;

        public SimpleList()
        {
            _head = null;
        }

        public IIterator CreateIterator()
        {
            return new SimpleListIterator(_head);
        }

        public void AddToFront(string date)
        {
            ListNode node = new ListNode(date);
            if (_head == null)
            {
                _head = node;
            }
            else
            {
                node.Next = _head;
                _head = node;
            }
        }

        public void AddToEnd(string data)
        {
            ListNode node = new ListNode(data);
            ListNode tmp = _head;
            if (_head == null)
            {
                _head = node;
            }
            else
            {
                while (tmp.Next != null)
                {
                    tmp = tmp.Next;
                }
                tmp.Next = node;
            }
        }
    }
    public class SimpleListIterator : IIterator
    {
        ListNode listNode;
        ListNode position;

        public SimpleListIterator(ListNode head)
        {
            listNode = head;
            position = head;
        }

        public object Next()
        {
            ListNode tmp = new ListNode();
            tmp.Data = position.Data;
            tmp.Next = position.Next;

            position = position.Next;
            return tmp;
        }

        public bool HasNext()
        {
            if (position != null)
            {
                return true;
            }
            return false;
        }

        public object First()
        {
            return listNode;
        }
        
    }

    public interface IIterator
    {
        object Next();

        bool HasNext();

        object First();

    }

}

