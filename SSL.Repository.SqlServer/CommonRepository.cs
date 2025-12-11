using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SSL.Core.Interfaces.Repository;
using SSL_ERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Repository.SqlServer
{
    public class CommonRepository : Repository, ICommonRepository
    {
        public CommonRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        //public List<UserBranch> GetBranch()
        //{
        //    try
        //    {
        //        var command = CreateAdapter("SELECT ProductTypeId as Name,ProductType as Value FROM CSProductsTypes where IsActive=1 and isnull(IsArchive,0) = 0  ");

        //        DataTable table = new DataTable();
        //        command.Fill(table);
        //        return JsonConvert.DeserializeObject<List<UserBranch>>(JsonConvert.SerializeObject(table));
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}



        //ch
        





        public List<CommonDropDown> GetAllProductType()
        {
            try
            {
                var command = CreateAdapter("SELECT ProductTypeId as Name,ProductType as Value FROM CSProductsTypes where IsActive=1 and isnull(IsArchive,0) = 0  ");
               
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllStore()
        {
            try
            {
                var command = CreateAdapter("SELECT StoreId as Name,StoreName as Value FROM CSStores where IsActive=1 and isnull(IsArchive,0) = 0  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllUom()
        {
            try
            {
                var command = CreateAdapter("SELECT UomId as Name,UomName as Value FROM CSUoms where IsActive=1 and isnull(IsArchive,0) = 0  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CommonDropDown> GetAllColor()
        {
            try
            {
                var command = CreateAdapter("SELECT ColorId as Name,ColorName as Value FROM CSColors where IsActive=1 and isnull(IsArchive,0) = 0  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllCurrencys()
        {
            try
            {
                var command = CreateAdapter(" SELECT CurrencyId as Name,CurrencyName as Value FROM CSCurrencies where IsActive=1 and isnull(IsArchive,0) = 0  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllDepartment()
        {
            try
            {
                var command = CreateAdapter(" SELECT DepartmentId as Name,DepartmentName as Value FROM CSDepartments where IsActive=1 and isnull(IsArchive,0) = 0  ");
               // command.SelectCommand.Parameters.Add("@SearchBy", SqlDbType.NVarChar).Value =string.IsNullOrEmpty(SearchBy) ? "" : SearchBy;

                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllUserName()
        {
            try
            {
                var command = CreateAdapter(" SELECT Id as Name,NormalizedUserName as Value FROM AspNetUsers  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CommonDropDown> GetAllSize()
        {
            try
            {
                var command = CreateAdapter(" SELECT SizeId as Name,SizeName as Value FROM CSSizes where IsActive=1 and ISNULL(IsArchive,0)=0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllProduct()
        {
            try
            {
                var command = CreateAdapter("  SELECT ProductId as Name,ProductName  as Value  FROM CSProducts  where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllVendor()
        {
            try
            {
                var command = CreateAdapter(" SELECT VendorId as Name,VendorName as Value  FROM CSVendors where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<CommonDropDown> GetAllCustomers()
        {
            try
            {
                var command = CreateAdapter(" SELECT CustomerId as Name,CustomerName as Value  FROM CSCustomers where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CommonDropDown> GetAllOrderCategories()
        {
            try
            {
                var command = CreateAdapter(" SELECT OrderCategoryId as Name,OrderCategoryName as Value  FROM EXPOrderCategories where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
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
        public List<CommonDropDown> GetAllPaymentTerms()
        {
            try
            {
                var command = CreateAdapter(" SELECT PaymentTermId as Name,PaymentTerm as Value  FROM CSPaymentTerms where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllBanks()
        {
            try
            {
                var command = CreateAdapter(" SELECT BankId as Name,BankName as Value  FROM CSBanks where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllInsuranceCompanies()
        {
            try
            {
                var command = CreateAdapter(" SELECT InsuranceCompanyId as Name,InsuranceCompanyName as Value  FROM CSInsuranceCompanies where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllDeliveryTerms()
        {
            try
            {
                var command = CreateAdapter(" SELECT DeliveryTermId as Name,DeliveryTerm as Value  FROM CSDeliveryTerms where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllShipmentModes()
        {
            try
            {
                var command = CreateAdapter(" SELECT ShipmentModeId as Name,ShipmentModeName as Value  FROM CSShipmentModes where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllLCCategories()
        {
            try
            {
                var command = CreateAdapter(" SELECT LCCategoryId as Name,LCCategoryName as Value  FROM LCCategories where IsActive=1 and isnull(IsArchive,0) = 0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllMasterLC()
        {
            try
            {
                var command = CreateAdapter(" SELECT LCMasterId as Name,LCNo as Value  FROM LCMasters   where IsBtBLC=0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllPI()
        {
            try
            {
                var command = CreateAdapter(" SELECT PIMasterId as Name,PINo as Value  FROM ExpPIMasters  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllExpOrder()
        {
            try
            {
                var command = CreateAdapter(" SELECT OrderMasterId as Name,OrderNo as Value  FROM ExpOrderMasters  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllBtbLC()
        {
            try
            {
                var command = CreateAdapter("  SELECT LCMasterId as Name,LCNo as Value  FROM LCMasters where IsBtBLC=1 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllExportPIContracts()
        {
            try
            {
                var command = CreateAdapter(" SELECT PIContractMasterId as Name,PIContractNo as Value  FROM  ExpPIContractMasters  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllPackingMode()
        {
            try
            {
                var command = CreateAdapter(" SELECT PackingModeId as Name,PackingModeName as Value  FROM CSPackingModes  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllExpInvoice()
        {
            try
            {
                var command = CreateAdapter(" SELECT InvoiceMasterId as Name,InvoiceNo as Value  FROM ExpInvoiceMasters  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllCountry()
        {
            try
            {
                var command = CreateAdapter(" SELECT CountryId as Name,CountryName as Value  FROM CSCountry  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllCnFAgents()
        {
            try
            {
                var command = CreateAdapter(" SELECT CourierAgentId as Name,CNFAgentName as Value  FROM CSCnFAgents  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }  
        public List<CommonDropDown> GetAllCouriers()
        {
            try
            {
                var command = CreateAdapter(" SELECT CourierId as Name,CourierName as Value  FROM CSCouriers  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }  
        public List<CommonDropDown> GetAllImportTypes()
        {
            try
            {
                var command = CreateAdapter(" SELECT ImportTypeID as Name,ImportType as Value  FROM CSImportTypes  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllEmployees()
        {
            try
            {
                var command = CreateAdapter(" SELECT EmployeeId as Name,Employeename as Value  FROM CSEmployees  ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> GetAllProductCategories()
        {
            try
            {
                var command = CreateAdapter(" SELECT ProductCategoryId as Name,ProductCategoryName as Value  FROM CSProductCategories where IsActive=1 and ISNULL(IsArchive,0)=0 ");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
      

        #region "Äutocomplete"

        public List<CommonDropDown> AutocompleteProduct(string Prefix)
        {
            try
            {
                var command = CreateAdapter("SELECT ProductId as Id,ProductName as Name  FROM CSProducts where IsActive=1 and isnull(IsArchive,0) = 0  and  ProductName like '%" + Prefix + "%' ");

                command.SelectCommand.Parameters.Add("@Prefix", SqlDbType.NVarChar).Value = Prefix;
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> ProductIdByName(string Name)
        {
            try
            {
                var command = CreateAdapter("SELECT ProductId AS Id  FROM CSProducts where IsActive=1 and isnull(IsArchive,0) = 0  and ProductName= '" + Name.Trim() + "'");
                DataTable table = new DataTable();
                command.Fill(table);
                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CommonDropDown> AutocompleteRequisitionNo(string Prefix)
        {
            try
            {
                var command = CreateAdapter("SELECT RequisitionMasterId as Id,RequisitionNo as Name  FROM ProRequisitionMasters where   RequisitionNo like '%" + Prefix + "%' ");

                command.SelectCommand.Parameters.Add("@Prefix", SqlDbType.NVarChar).Value = Prefix;
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


        public List<CommonDropDown> APDocumentType()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'APDocumentType'
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
        public List<CommonDropDown> BankEntryType()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'BankEntryType'
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

        public List<CommonDropDown> CategoryType()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'CategoryType'
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

        public List<CommonDropDown> SectionNameList(string sageLocationCode )
        {
            try
            {
                string sqlText = @" SELECT 'All' Name,'All' Value ";
                sqlText += @" UNION ALL ";
                sqlText += @"
SELECT DISTINCT DLRLocationCode Name,DLRLocationCode AS Value  
FROM DLRLocationMapping WHERE 1=1  ";

                if (!string.IsNullOrWhiteSpace(sageLocationCode))
                {
                    sqlText += " AND SageLocationCode=@sageLocationCode";
                }

                SqlDataAdapter command = CreateAdapter(sqlText);

                if (!string.IsNullOrWhiteSpace(sageLocationCode))
                {
                    command.SelectCommand.Parameters.AddWithValue("@sageLocationCode", sageLocationCode);
                }

                DataTable table = new DataTable();
                command.Fill(table);

                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CommonDropDown> Branchs(string UserId)
        {
            try
            {
                string sqlText = @"
            			SELECT '-1' as Value, 'ALL' as Name
UNION ALL
SELECT DISTINCT b.BranchID as Value, b.BranchName as Name
FROM BranchProfiles b
LEFT OUTER JOIN UserBranchMap u ON b.BranchID = u.BranchId
WHERE u.UserId = @UserId";

                SqlCommand objComm = CreateCommand(sqlText);
                objComm.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter command = CreateAdapter(sqlText);
                command.SelectCommand.Parameters.AddWithValue("@UserId", UserId);

                DataTable table = new DataTable();
                command.Fill(table);

                return JsonConvert.DeserializeObject<List<CommonDropDown>>(JsonConvert.SerializeObject(table));
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        public List<CommonDropDown> DepositType()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'BankPaymentType'
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
        public List<CommonDropDown> ProrationMethod()
        {
            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'ProrationMethod'
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

        public List<CommonDropDown> POType()
        {

            try
            {
                string sqlText = @"
SELECT EnumValue Name,EnumValue as Value  
FROM Enums
where EnumType = 'POType'
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

        public List<CommonDropDown> UserBranch()
        {
            try
            {
                string sqlText = @"
SELECT BranchId as Value, 
BranchName as Name
FROM BranchProfiles


order by BranchId";

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

        public List<CommonDropDown> UserId()
        {
            try
            {
                string sqlText = @"
SELECT Id as Value , UserName as Name
FROM AuthDB.dbo.AspNetUsers


order by Id";

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



        public CommonDropDown NextPreviousWithBatch(int id, string status, string tableName)
        {
	        try
	        {
		        CommonDropDown records = new CommonDropDown();

                int count = 0;
                int getId = 0;
				int BranchId = 0;
		        string SageBatchNo = "";
		        int PreviousId = 0;
		        int NextId = 0;
		        string sqlText = "";
                
		        sqlText = $@" SELECT SageBatchNo,BranchId  FROM {tableName} WHERE 1=1 AND Id=@Id ";

		        SqlDataAdapter command = CreateAdapter(sqlText);
		        command.SelectCommand.Parameters.AddWithValue("@Id", id);

				DataTable dt = new DataTable();
		        command.Fill(dt);

		        if (dt.Rows.Count > 0)
		        {
					BranchId = Convert.ToInt16(dt.Rows[0]["BranchId"]);
					SageBatchNo = dt.Rows[0]["SageBatchNo"].ToString();
		        }

                string getSqlText = "";
				if (status.ToLower() == "previous")
		        {
			        getSqlText = $@" SELECT TOP 1 Id  FROM {tableName} WHERE 1=1 AND SageBatchNo=@SageBatchNo AND BranchId=@BranchId AND  Id<@Id ";
				}
                else if (status.ToLower() == "next")
		        {
			        getSqlText = $@" SELECT TOP 1 Id  FROM {tableName} WHERE 1=1 AND SageBatchNo=@SageBatchNo AND BranchId=@BranchId AND  Id>@Id ";
				}
                if (status.ToLower() == "previous")
                {
                    getSqlText += " ORDER BY Id DESC ";
                }
                else if (status.ToLower() == "next")
                {
                    getSqlText += " ORDER BY Id ASC ";
                }

                SqlDataAdapter preCommand = CreateAdapter(getSqlText);
                
		        preCommand.SelectCommand.Parameters.AddWithValue("@Id", id);
				preCommand.SelectCommand.Parameters.AddWithValue("@SageBatchNo", SageBatchNo);
				preCommand.SelectCommand.Parameters.AddWithValue("@BranchId", BranchId);

				DataTable table = new DataTable();
				preCommand.Fill(table);

		        if (table.Rows.Count > 0)
		        {
                    getId = Convert.ToInt16(table.Rows[0]["Id"]);
		        }

                records.Id = getId;

				return records;
	        }
	        catch (Exception e)
	        {
		        throw e;
	        }
        }
        public CommonDropDown NextPreviousWithBK(int id, string status, string tableName)
        {
            try
            {
                CommonDropDown records = new CommonDropDown();

                int getId = 0;
                int BranchId = 0;
                string TransactionType = "";
                string sqlText = "";

                sqlText = $@" SELECT TransactionType,BranchId  FROM {tableName} WHERE 1=1 AND Id=@Id ";

                SqlDataAdapter command = CreateAdapter(sqlText);
                command.SelectCommand.Parameters.AddWithValue("@Id", id);

                DataTable dt = new DataTable();
                command.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    BranchId = Convert.ToInt16(dt.Rows[0]["BranchId"]);
                    TransactionType = dt.Rows[0]["TransactionType"].ToString();
                }

                string getSqlText = "";
                if (status.ToLower() == "previous")
                {
                    getSqlText = $@" SELECT TOP 1 Id  FROM {tableName} WHERE 1=1 AND TransactionType=@TransactionType AND BranchId=@BranchId AND  Id<@Id ";
                }
                else if (status.ToLower() == "next")
                {
                    getSqlText = $@" SELECT TOP 1 Id  FROM {tableName} WHERE 1=1 AND TransactionType=@TransactionType AND BranchId=@BranchId AND  Id>@Id ";
                }
                if (status.ToLower() == "previous")
                {
                    getSqlText += " ORDER BY Id DESC ";
                }
                else if (status.ToLower() == "next")
                {
                    getSqlText += " ORDER BY Id ASC ";
                }

                SqlDataAdapter preCommand = CreateAdapter(getSqlText);

                preCommand.SelectCommand.Parameters.AddWithValue("@Id", id);
                preCommand.SelectCommand.Parameters.AddWithValue("@BranchId", BranchId);
                preCommand.SelectCommand.Parameters.AddWithValue("@TransactionType", TransactionType);

                DataTable table = new DataTable();
                preCommand.Fill(table);

                if (table.Rows.Count > 0)
                {
                    getId = Convert.ToInt16(table.Rows[0]["Id"]);
                }

                records.Id = getId;

                return records;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public CommonDropDown NextPreviousOther(int id, string status, string tableName)
        {
            try
            {
                CommonDropDown records = new CommonDropDown();

                int getId = 0;
                int BranchId = 0;
                string sqlText = "";

                sqlText = $@" SELECT BranchId  FROM {tableName} WHERE 1=1 AND Id=@Id ";

                SqlDataAdapter command = CreateAdapter(sqlText);
                command.SelectCommand.Parameters.AddWithValue("@Id", id);

                DataTable dt = new DataTable();
                command.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    BranchId = Convert.ToInt16(dt.Rows[0]["BranchId"]);
                }

                string getSqlText = "";
                if (status.ToLower() == "previous")
                {
                    getSqlText = $@" SELECT TOP 1 Id  FROM {tableName} WHERE 1=1 AND BranchId=@BranchId AND  Id<@Id ";
                }
                else if (status.ToLower() == "next")
                {
                    getSqlText = $@" SELECT TOP 1 Id  FROM {tableName} WHERE 1=1 AND BranchId=@BranchId AND  Id>@Id ";
                }
                if (status.ToLower() == "previous")
                {
                    getSqlText += " ORDER BY Id DESC ";
                }
                else if (status.ToLower() == "next")
                {
                    getSqlText += " ORDER BY Id ASC ";
                }

                SqlDataAdapter preCommand = CreateAdapter(getSqlText);

                preCommand.SelectCommand.Parameters.AddWithValue("@Id", id);
                preCommand.SelectCommand.Parameters.AddWithValue("@BranchId", BranchId);

                DataTable table = new DataTable();
                preCommand.Fill(table);

                if (table.Rows.Count > 0)
                {
                    getId = Convert.ToInt16(table.Rows[0]["Id"]);
                }

                records.Id = getId;

                return records;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        


    }

    #endregion "Äutocomplete"
}

