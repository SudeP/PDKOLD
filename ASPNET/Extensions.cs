using System.Data;

namespace PDK.ASPNET
{
    public static class Extensions
    {
        public static bool NotNull(this DataSet dataSet) => dataSet is null ? false : true;
        public static bool HasOneTable(this DataSet dataSet, out DataTable dataTable)
        {
            dataTable = null;
            if (!(dataSet is null) && dataSet.Tables.Count > 0)
            {
                dataTable = dataSet.Tables[0];
                return true;
            }
            return false;
        }
    }
}
