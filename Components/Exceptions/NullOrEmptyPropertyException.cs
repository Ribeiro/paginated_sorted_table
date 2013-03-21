using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRV.Application.Components.Exceptions
{
    public class NullOrEmptyPropertyException : Exception
    {
        private const string _defaultErrorMessage = "PaginatedSortedTableComponent - Property value cannot be NULL or EMPTY!";
        private string _message;

        public NullOrEmptyPropertyException()
        {
            _message = _defaultErrorMessage;

        }

        public NullOrEmptyPropertyException(string message)
        {
            _message = "PaginatedSortedTableComponent - " + message;

        }

        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message))
                    _message = base.Message;

                return _message;
            }
        }
    }
}
