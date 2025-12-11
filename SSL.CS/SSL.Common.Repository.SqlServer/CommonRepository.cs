using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SSL.Common.SSL.Common.Core.Interfaces.Repository;
using SSL.CS.SSL.Common.Models;
using System.Data;
//using System.Data.SqlClient;

namespace SSL.CS.SSL.Common.Repository.SqlServer
{
    public class CommonRepository : Repository, ICommonRepository
    {
        public CommonRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        
        public List<CommonDropDown> GetAllPorts()
        {
            try
            {
                var command = CreateAdapter(" SELECT PortId as Name,PortName as Value  FROM CSPorts where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        } 

        //mychange
        public List<CommonDropDown> GetAllHeaders()
        {
            try
            {
                var one = new { Value = 1, Name = "Recept Number" };
                var two = new { Value = 2, Name = "Description" };
                var three = new { Value = 3, Name = "Recept Date" };

                List<object> common = new List<object>();
                common.Add(one);
                common.Add(two);
                common.Add(three);

                //var command = CreateAdapter(" SELECT CurrencyId as Name,CurrencyName as Value FROM CSCurrencies where IsActive=1 and isnull(IsArchive,0) = 0  ");
                DataTable table = new DataTable();
                //common.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(common));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CommonDropDown> EntryTypes()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'ReceiptType'
and ActiveStatus = '1'
order by SLNo";

                SqlDataAdapter command = CreateAdapter(sqlText);

               
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //BANK
        //public List<CommonDropDown> DepositType()
        //{
        //    try
        //    {
        //        var one = new { Value = 1, Name = "Savings" };
        //        var two = new { Value = 2, Name = "current" };

        //        List<object> common = new List<object>();
        //        common.Add(one);
        //        common.Add(two);


        //        //var command = CreateAdapter(" SELECT CurrencyId as Name,CurrencyName as Value FROM CSCurrencies where IsActive=1 and isnull(IsArchive,0) = 0  ");
        //        DataTable table = new DataTable();
        //        //common.Fill(table);
        //        return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(common));
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //public List<CommonDropDown> BankEntryType()
        //{
        //    try
        //    {
        //        var one = new { Value = 1, Name = "Transaction entry" };
        //        var two = new { Value = 2, Name = "Closing entry" };

        //        List<object> common = new List<object>();
        //        common.Add(one);
        //        common.Add(two);


        //        //var command = CreateAdapter(" SELECT CurrencyId as Name,CurrencyName as Value FROM CSCurrencies where IsActive=1 and isnull(IsArchive,0) = 0  ");
        //        DataTable table = new DataTable();
        //        //common.Fill(table);
        //        return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(common));
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        public List<CommonDropDown> DocumentType()
        {

            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'DocumentType'
and ActiveStatus = '1'
order by SLNo";

                SqlDataAdapter command = CreateAdapter(sqlText);

                DataTable table = new DataTable();
                command.Fill(table);

                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));

            }
            catch (Exception e)
            {
                throw e;
            }


            //try
            //{
            //    var one = new { Value = 1, Name = "Transfer" };
            //    var two = new { Value = 2, Name = "Transit Transfer" };
            //    var three = new { Value = 3, Name = "Transit Receipt" };

            //    List<object> common = new List<object>();
            //    common.Add(one);
            //    common.Add(two);
            //    common.Add(three);


            //    //var command = CreateAdapter(" SELECT CurrencyId as Name,CurrencyName as Value FROM CSCurrencies where IsActive=1 and isnull(IsArchive,0) = 0  ");
            //    DataTable table = new DataTable();
            //    //common.Fill(table);
            //    return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(common));
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        public List<CommonDropDown> ApplyMethod()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'ApplyMethod'
and ActiveStatus = '1'
order by SLNo";

                SqlDataAdapter command = CreateAdapter(sqlText);


                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CommonDropDown> TransactionType()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'TransactionType'
and ActiveStatus = '1'
order by SLNo";

                SqlDataAdapter command = CreateAdapter(sqlText);


                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CommonDropDown> OrderBy()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'OrderBy'
and ActiveStatus = '1'
order by SLNo";

                SqlDataAdapter command = CreateAdapter(sqlText);


                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
         
        


    }

}

