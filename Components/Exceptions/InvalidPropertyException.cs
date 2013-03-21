using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRV.Application.Components.Exceptions
{
    public class InvalidPropertyException : Exception
    {
        private const string _defaultErrorMessage = "PaginatedSortedTableComponent - Property value must be greater than ZERO!";
        private string _message;

        public InvalidPropertyException()
        {
            _message = _defaultErrorMessage;

        }

        public InvalidPropertyException(string message)
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
