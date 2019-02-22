using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Globalization;

namespace Digitalizacion2014.Clases
{
    public class ColumnSorter : IComparer
    {
        private int sortColumn;

        public int SortColumn
        {
            set { sortColumn = value; }
            get { return sortColumn; }
        }

        private SortOrder sortOrder;

        public SortOrder Order
        {
            set { sortOrder = value; }
            get { return sortOrder; }
        }

        private Comparer listViewItemComparer;

        public ColumnSorter()
        {
            sortColumn = 0;

            sortOrder = SortOrder.None;

            listViewItemComparer = new Comparer(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            try
            {
                ListViewItem lviX = (ListViewItem)x;
                ListViewItem lviY = (ListViewItem)y;

                int compareResult = 0;

                if (lviX.SubItems[sortColumn].Tag != null && lviY.SubItems[sortColumn].Tag != null)
                {
                    compareResult = listViewItemComparer.Compare(lviX.SubItems[sortColumn].Tag, lviY.SubItems[sortColumn].Tag);
                }
                else
                {
                    compareResult = listViewItemComparer.Compare(lviX.SubItems[sortColumn].Text, lviY.SubItems[sortColumn].Text);
                }

                if (sortOrder == SortOrder.Ascending)
                {
                    return compareResult;
                }
                else if (sortOrder == SortOrder.Descending)
                {
                    return (-compareResult);
                }
                else
                {
                    return 0;
                }

            }
            catch
            {
                return 0;
            }
        }
    }
}
