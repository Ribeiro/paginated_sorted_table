using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using System.Text.RegularExpressions;
using SRV.Application.Components.Util;
using SRV.Application.Components.Exceptions;

//Autores : Geovanny Ribeiro e Emerson Barbosa

namespace SRV.Application.Components.Util
{
    public class PaginatedSortedTableUtil
    {

        private static void CheckForNullOrEmptyPropertyValue(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new NullOrEmptyPropertyException();
            }

        }

        private static void CheckForInvalidPropertyValue(int value)
        {
            if (value < 0)
            {
                throw new InvalidPropertyException();
            }
        }

        private static void CheckForNullPropertyValue(object value)
        {
            if(value == null)
            {
                throw new NullOrEmptyPropertyException("Property value cannot be NULL!");
            }

        }

        private static void CheckForInvalidOrder(string order)
        {
            CheckForNullOrEmptyPropertyValue(order);
            order = order.ToUpper();
            Regex ascendingOrderRegex = new Regex("^ASC$");
            Regex descendingOrderRegex = new Regex("^DESC$");

            if (!ascendingOrderRegex.IsMatch(order) && !descendingOrderRegex.IsMatch(order))
            {
                throw new InvalidOrderException();
            }
        }

        public static void ValidateBasicTableProperties(string header, string order, int indexPage, ICriteria baseQuery, int resultsPerpage)
        {
            CheckForNullOrEmptyPropertyValue(header);
            CheckForInvalidOrder(order);
            CheckForInvalidPropertyValue(indexPage);
            CheckForNullPropertyValue(baseQuery);
            CheckForInvalidPropertyValue(resultsPerpage);
        }


    }
}