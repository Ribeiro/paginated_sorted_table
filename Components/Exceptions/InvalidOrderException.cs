using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRV.Application.Components.Exceptions
{
    public class InvalidOrderException : Exception
    {
        private const string _defaultErrorMessage = "PaginatedSortedTableComponent - Order error: Only 'ASC' or 'DESC' are accepted!";
        private string _message;

        public InvalidOrderException()
        {
            _message = _defaultErrorMessage;

        }

        public InvalidOrderException(string message)
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