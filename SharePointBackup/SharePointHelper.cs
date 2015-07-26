namespace SharePointBackup
{
    using System;
    using System.Data;
    using Microsoft.SharePoint.Client;

    class SharePointHelper
    {
        internal static DataTable GetAllListItems(string siteUrl, string listName, string userName, string password)
        {
            var dataTable = new DataTable(listName);
            try
            {
                using (var clientContext = new ClientContext(siteUrl))
                {
                    clientContext.Credentials = Helper.GetNetworkCredential(userName, password);
                    var list = clientContext.Web.Lists.GetByTitle(listName);
                    var query = CamlQuery.CreateAllItemsQuery();
                    var listItems = list.GetItems(query);
                    var listFields = list.Fields;
                    clientContext.Load(listItems);
                    clientContext.Load(listFields);
                    clientContext.ExecuteQuery();

                    foreach (var listField in listFields)
                    {
                        if ((listField.ReadOnlyField == false && listField.FromBaseType == false) || listField.Required)
                        {
                            dataTable.Columns.Add(listField.InternalName);
                        }
                    }

                    foreach (var listItem in listItems)
                    {
                        var dataRow = dataTable.NewRow();
                        foreach (var listField in listFields)
                        {
                            if ((listField.ReadOnlyField == false && listField.FromBaseType == false)
                                || listField.Required)
                            {
                                dataRow[listField.InternalName] = listItem[listField.InternalName];
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("GetAllListItems failed:" + exception.Message, exception);
            }
            return dataTable;
        }
    }
}
