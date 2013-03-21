using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Configuration;
using NHibernate.Criterion;
using System.Reflection;
using SRV.Application.Components.Util;

//Autores : Geovanny Ribeiro e Emerson Barbosa

namespace SRV.Application.Components.Core
{
    public class PaginatedSortedTable<T> where T : class
    {
        public int IndexPage { get; set; }
        public int CurrentPage
        {
            get { return IndexPage + 1; }
        }

        public int NextPage
        {
            get
            {

                if (CurrentPage == LastPage)
                {
                    return LastPage;
                }
                else
                {
                    return CurrentPage + 1;

                }

            }
        }

        public int PreviousPage
        {
            get
            {

                if (CurrentPage == IndexPage)
                {
                    return IndexPage;
                }
                else
                {
                    return CurrentPage - 1;

                }

            }
        }

        public int LastPage
        {
            get { return RetrievePaginationLastPage(); }
        }

        public List<int> PagesList
        {
            get { return RetrieveNavigationPagesList(); }
        }

        public ICriteria BaseQuery { get; set; }
        private IList<T> list;
        public IList<T> List
        {
            set
            {
                list = value;
            }
            get
            {
                if (list == null)
                {
                    if (BaseQuery == null)
                    {
                        return new List<T>();
                    }
                    else
                    {
                        ICriteria cloneBaseQuery = (ICriteria)BaseQuery.Clone();
                        cloneBaseQuery.ClearOrders();

                        cloneBaseQuery.AddOrder(new Order(Header.Replace('_', '.'), Order == "ASC" ? true : false));

                        cloneBaseQuery.SetFirstResult(IndexPage * ResultsPerPage);
                        cloneBaseQuery.SetMaxResults(ResultsPerPage);

                        return cloneBaseQuery.List<T>();
                    }
                }
                else
                {
                    return list;
                }
            }
        }


        public string Header { get; set; }
        public string Order { get; set; }

        private const int resultsPerPageDefault = 15;
        private int resultsPerPage;
        public int ResultsPerPage
        {
            get
            {
                if (resultsPerPage > 0)
                {
                    return resultsPerPage;
                }
                else
                {
                    return resultsPerPageDefault;
                }
            }
        }

        public PaginatedSortedTable(string header, string order, int indexPage, ICriteria baseQuery, int resultsPerPage)
        {
            PaginatedSortedTableUtil.ValidateBasicTableProperties(header, order, indexPage, baseQuery, resultsPerPage);
            this.Header = header;
            this.Order = order;
            this.IndexPage = indexPage;
            this.BaseQuery = baseQuery;
            this.resultsPerPage = resultsPerPage;
        }

        private int RetrievePaginationLastPage()
        {
            ICriteria totalRows = ((ICriteria)BaseQuery.Clone()).SetProjection(Projections.RowCount());
            return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows.UniqueResult()) / resultsPerPage));
        }

        public void ForceListRefresh()
        {
            list = null;
            var varList = List;
        }

        private List<int> RetrieveNavigationPagesList()
        {
            int leftLimitPage = 1;
            List<int> pagesList = new List<int>();
            int startPage = CurrentPage - 2 < leftLimitPage ? leftLimitPage : CurrentPage - 2;

            if (CurrentPage > LastPage - 4)
            {
                startPage = LastPage - 4;
            }

            if (CurrentPage - 4 <= 0)
            {
                startPage = 1;
            }

            for (int i = 1; i <= 5; i++)
            {
                if (startPage <= LastPage && startPage > 0)
                {
                    pagesList.Add(startPage);
                    startPage++;
                }
            }

            return pagesList;
        }

    }

}
