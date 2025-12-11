using SSL.Common.SSL.Common.Models.KendoCommon;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data;
using SSL.CS.SSL.Common.Models;

namespace SSL.Common.SSL.Common.Models.KendoCommon
{
    public class KendoGrid<T>
    {
        private static SqlDataAdapter da;
        private static SqlConnection dbConn;
        private static SqlCommand cmd;
        private static DataSet ds;
        private static DataTable dt;
        private static int totalCount = 0;
        private static string connectionString = "";
        //private static string connectionString = @"Data Source=192.168.15.100,1419\\MSSQLSERVER2019;Initial Catalog=SSLSupportDB_DevV2;User ID=sa;password=S123456_;TrustServerCertificate=true; connect Timeout=1000";
        // private static string connectionString = @"Data Source=18.118.71.248,1434;Initial Catalog=SSLSupportAuthDB_Dev;User ID=sa;password=S123456_;TrustServerCertificate=true; connect Timeout=1000";
        //public KendoGrid(IConfiguration configuration)
        //{
        //    // Retrieve the connection string from the configuration
        //    connectionString = configuration.GetConnectionString("DBContext");
        //}
        public static GridEntity<T> GetGridData(GridOptions gridOption, string ProcName, string CallType, string orderby, int param1 = 0, string param2 = "")
        {
            try
            {
                gridOption.take = gridOption.skip + gridOption.take;
                var filterby = "";
                if (gridOption.filter != null)
                {
                    filterby = gridOption != null ? GridQueryBuilder<T>.FilterCondition(gridOption.filter) : "";
                }
                if (gridOption.sort != null)
                {
                    orderby = gridOption.sort[0].field + " " + gridOption.sort[0].dir;
                }

                dbConn = new SqlConnection(SessionModel.sslConn);
                dbConn.Open();
                cmd = new SqlCommand(ProcName, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@skip", gridOption.skip));
                cmd.Parameters.Add(new SqlParameter("@take ", gridOption.take));
                cmd.Parameters.Add(new SqlParameter("@filter", filterby));
                cmd.Parameters.Add(new SqlParameter("@orderby", orderby.Trim()));
                cmd.Parameters.Add(new SqlParameter("@param1", param1));
                cmd.Parameters.Add(new SqlParameter("@param2", param2));
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dbConn.Close();

                dt = ds.Tables[1];
                totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                var dataList = (List<T>)ListConversion.ConvertTo<T>(dt).ToList();
                var result = new GridResult<T>().Data(dataList, totalCount);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static GridEntity<T> GetGenericGridData(GridOptions gridOption, string ProcName, string CallType, string orderby, int param1 = 0, string param2 = "")
        {
            try
            {
                gridOption.take = gridOption.skip + gridOption.take;
                var filterby = "";
                if (gridOption.filter != null)
                {
                    filterby = gridOption != null ? GridQueryBuilder<T>.FilterCondition(gridOption.filter) : "";
                }
                if (gridOption.sort != null)
                {
                    orderby = gridOption.sort[0].field + " " + gridOption.sort[0].dir;
                }

                dbConn = new SqlConnection(SessionModel.sslConn);
                dbConn.Open();
                cmd = new SqlCommand(ProcName, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@skip", gridOption.skip));
                cmd.Parameters.Add(new SqlParameter("@take ", gridOption.take));
                cmd.Parameters.Add(new SqlParameter("@filter", filterby));
                cmd.Parameters.Add(new SqlParameter("@orderby", orderby.Trim()));
                cmd.Parameters.Add(new SqlParameter("@param1", param1));
                cmd.Parameters.Add(new SqlParameter("@param2", param2));
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dbConn.Close();

                dt = ds.Tables[1];
                totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                var dataList = (List<T>)GenericListGenerator.GetList<T>(dt);
                var result = new GridResult<T>().Data(dataList, totalCount);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static GridEntity<T> GetGridData_5(GridOptions gridOption, string ProcName, string CallType, string orderby, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {
            try
            {
                gridOption.take = gridOption.skip + gridOption.take;
                var filterby = "";
                if (gridOption.filter != null)
                {
                    filterby = gridOption != null ? GridQueryBuilder<T>.FilterCondition(gridOption.filter) : "";
                }
                if (gridOption.sort != null)
                {
                    orderby = gridOption.sort[0].field + " " + gridOption.sort[0].dir;
                }

                dbConn = new SqlConnection(SessionModel.sslConn);
                dbConn.Open();
                cmd = new SqlCommand(ProcName, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@skip", gridOption.skip));
                cmd.Parameters.Add(new SqlParameter("@take ", gridOption.take));
                cmd.Parameters.Add(new SqlParameter("@filter", filterby));
                cmd.Parameters.Add(new SqlParameter("@orderby", orderby.Trim()));
                cmd.Parameters.Add(new SqlParameter("@param1", param1));
                cmd.Parameters.Add(new SqlParameter("@param2", param2));
                cmd.Parameters.Add(new SqlParameter("@param3", param3));
                cmd.Parameters.Add(new SqlParameter("@param4", param4));
                cmd.Parameters.Add(new SqlParameter("@param5", param5));
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dbConn.Close();
                dbConn.Dispose();
                cmd.Dispose();

                dt = ds.Tables[1];
                totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                var dataList = (List<T>)ListConversion.ConvertTo<T>(dt).ToList();
                var result = new GridResult<T>().Data(dataList, totalCount);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static GridEntity<T> GetGenericGridData_5(GridOptions gridOption, string ProcName, string CallType, string orderby, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {
            try
            {
                gridOption.take = gridOption.skip + gridOption.take;
                var filterby = "";
                if (gridOption.filter != null)
                {
                    filterby = gridOption != null ? GridQueryBuilder<T>.FilterCondition(gridOption.filter) : "";
                }
                if (gridOption.sort != null)
                {
                    orderby = gridOption.sort[0].field + " " + gridOption.sort[0].dir;
                }

                dbConn = new SqlConnection(SessionModel.sslConn);
                dbConn.Open();
                cmd = new SqlCommand(ProcName, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@skip", gridOption.skip));
                cmd.Parameters.Add(new SqlParameter("@take ", gridOption.take));
                cmd.Parameters.Add(new SqlParameter("@filter", filterby));
                cmd.Parameters.Add(new SqlParameter("@orderby", orderby.Trim()));
                cmd.Parameters.Add(new SqlParameter("@param1", param1));
                cmd.Parameters.Add(new SqlParameter("@param2", param2));
                cmd.Parameters.Add(new SqlParameter("@param3", param3));
                cmd.Parameters.Add(new SqlParameter("@param4", param4));
                cmd.Parameters.Add(new SqlParameter("@param5", param5));
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dbConn.Close();

                dt = ds.Tables[1];
                totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                var dataList = (List<T>)GenericListGenerator.GetList<T>(dt).ToList();
                var result = new GridResult<T>().Data(dataList, totalCount);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static GridEntity<T> GetGridData_No_Paging(string comname, string ProcName, string CallType, string parm1 = "", string parm2 = "", string parm3 = "", string parm4 = "", string parm5 = "", string parm6 = "", string parm7 = "", string parm8 = "", string parm9 = "", string parm10 = "")
        {
            try
            {
                dbConn = new SqlConnection(SessionModel.sslConn);
                dbConn.Open();
                cmd = new SqlCommand(ProcName, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ComC1", comname));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", parm1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", parm2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", parm3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", parm4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", parm5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", parm6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", parm7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", parm8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", parm9));
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dbConn.Close();

                dt = ds.Tables[0];
                totalCount = Convert.ToInt32(ds.Tables[0].Rows.Count);
                var dataList = (List<T>)ListConversion.ConvertTo<T>(dt).ToList();
                var result = new GridResult<T>().Data(dataList, totalCount);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static GridEntity<T> GetMPLGridData(GridOptions gridOption, string query, string orderby, string param1 = "", string param2 = "", string param3 = "", string param4 = "", string param5 = "")
        {
            try
            {
                gridOption.take = gridOption.skip + gridOption.take;
                var filterby = "";
                if (gridOption.filter != null)
                {
                    filterby = gridOption != null ? GridQueryBuilder<T>.FilterCondition(gridOption.filter) : "";
                }
                if (gridOption.sort != null)
                {
                    orderby = gridOption.sort[0].field + " " + gridOption.sort[0].dir;
                }

                if (gridOption.filter == null)
                {
                    query = @"
                                select  COUNT(*) AS totalcount FROM ViewMPLD1
                            ";
                }
                else
                {
                    var filterValue = gridOption.filter;

                    query = @"
                                DECLARE @sql NVARCHAR(MAX);
                                DECLARE @filter NVARCHAR(MAX) = N'{filterValue}';

                                SET @sql = N'
                                    SELECT COUNT(*) AS totalcount
                                    FROM PurchaseInvoiceHeaders pih 
                                    LEFT OUTER JOIN Vendors v ON pih.VendorID = v.VendorID 
                                    WHERE (' + @filter + ' OR @filter IS NULL)';
                                EXEC sp_executesql @sql, N'@filter NVARCHAR(MAX)', @filter;";
                }


                if (gridOption.filter == null)
                {
                    query = @"
                                DECLARE @filterquery NVARCHAR(MAX);

                                SET @filterquery = N'
                                    SELECT TOP 1000 * FROM  
                                    (
                                        SELECT ROW_NUMBER() OVER(ORDER BY ' + @orderby + ') AS rowindex,
                                            pih.PurchaseInvoiceNo,
                                            pih.ReceiveDate,
                                            pih.Post,  -- Removed extra comma here
                                            pih.InvoiceDateTime,
                                            v.VendorName VendorName,
                                            pih.BENumber,
                                            pih.TransactionType,
                                            pih.ImportIDExcel,
                                            vg.VendorGroupName,
                                            ISNULL(pih.BranchId,0) BranchId,
                                            ISNULL(pih.TotalAmount,0) TotalAmount,
                                            vds.Subtotal TotalSubtotal,
                                            ISNULL(pih.TotalVATAmount,0) TotalVATAmount,
                                            pih.SerialNo,
                                            pih.Comments,
                                            pih.CreatedBy,
                                            pih.CreatedOn,
                                            pih.LastModifiedBy,
                                            pih.LastModifiedOn,
                                            pih.ProductType,
                                            ISNULL(pih.CurrencyRateFromBDT,0) CurrencyRateFromBDT,
                                            pih.WithVDS,
                                            pih.PurchaseReturnId,
                                            pih.SerialNo1,
                                            pih.LCNumber,
                                            pih.LCDate,
                                            ISNULL(pih.LandedCost,0) LandedCost,
                                            pih.CustomHouse,
                                            pih.CustomCode,
                                            pih.IsVDSCompleted,
                                            pih.IsTDS,
                                            v.VendorGroupID,
                                            v.Address1 VendorAddress,
                                            ISNULL(VDS.VDSAmount,0) VDSAmount,
                                            ISNULL(pih.TDSAmount,0) TDSAmount,
                                            ISNULL(pih.USDInvoiceValue,0) USDInvoiceValue,
                                            pih.Id,
                                            pih.FiscalYear,
                                            pih.VendorID,
                                            pih.CurrencyID,
                                            pih.RebatePeriodID,
                                            pih.IsRebate,
                                            pih.RebateDate,
                                            pih.BankGuarantee,
                                            ISNULL(pih.TDSRate,0) TDSRate,
                                            ISNULL(pih.VehicleNo,''-'') VehicleNo,
                                            ISNULL(pih.VehicleType,''-'') VehicleType
                                        FROM PurchaseInvoiceHeaders pih
                                        LEFT OUTER JOIN Vendors v ON pih.VendorID = v.VendorID 
                                        INNER JOIN VendorGroups vg ON v.VendorGroupID = vg.VendorGroupID
                                        LEFT OUTER JOIN (
                                            SELECT DISTINCT PurchaseInvoiceNo,
                                                SUM(ISNULL(subtotal,0)) Subtotal,
                                                SUM(ISNULL(VDSAmount,0)) VDSAmount
                                            FROM PurchaseInvoiceDetails
                                            GROUP BY PurchaseInvoiceNo
                                        ) VDS ON pih.PurchaseInvoiceNo = VDS.PurchaseInvoiceNo
                                        WHERE 1 = 1
                                    ) AS subquery
                                    WHERE rowindex > @skip AND rowindex <= @take';

                                EXEC sp_executesql @filterquery, N'@skip INT, @take INT', @skip, @take;

                        ";

                }
                else
                {
                    query = @"
                                DECLARE @filterquery NVARCHAR(MAX);
                                DECLARE @filter NVARCHAR(MAX) = @filter;

                                SET @filterquery = N'
                                    SELECT TOP 1000 * FROM  
                                    (
                                        SELECT ROW_NUMBER() OVER(ORDER BY ' + @orderby + ') AS rowindex,
                                            pih.PurchaseInvoiceNo,
                                            pih.ReceiveDate,
                                            pih.Post,
                                            pih.InvoiceDateTime,
                                            v.VendorName AS VendorName,
                                            pih.BENumber,
                                            pih.TransactionType,
                                            pih.ImportIDExcel,
                                            vg.VendorGroupName,
                                            COALESCE(pih.BranchId, 0) AS BranchId,
                                            COALESCE(pih.TotalAmount, 0) AS TotalAmount,
                                            vds.Subtotal AS TotalSubtotal,
                                            COALESCE(pih.TotalVATAmount, 0) AS TotalVATAmount,
                                            pih.SerialNo,
                                            pih.Comments,
                                            pih.CreatedBy,
                                            pih.CreatedOn,
                                            pih.LastModifiedBy,
                                            pih.LastModifiedOn,
                                            pih.ProductType,
                                            COALESCE(pih.CurrencyRateFromBDT, 0) AS CurrencyRateFromBDT,
                                            pih.WithVDS,
                                            pih.PurchaseReturnId,
                                            pih.SerialNo1,
                                            pih.LCNumber,
                                            pih.LCDate,
                                            COALESCE(pih.LandedCost, 0) AS LandedCost,
                                            pih.CustomHouse,
                                            pih.CustomCode,
                                            pih.IsVDSCompleted,
                                            pih.IsTDS,
                                            v.VendorGroupID,
                                            v.Address1 AS VendorAddress,
                                            COALESCE(VDS.VDSAmount, 0) AS VDSAmount,
                                            COALESCE(pih.TDSAmount, 0) AS TDSAmount,
                                            COALESCE(pih.USDInvoiceValue, 0) AS USDInvoiceValue,
                                            pih.Id,
                                            pih.FiscalYear,
                                            pih.VendorID,
                                            pih.CurrencyID,
                                            pih.RebatePeriodID,
                                            pih.IsRebate,
                                            pih.RebateDate,
                                            pih.BankGuarantee,
                                            COALESCE(pih.TDSRate, 0) AS TDSRate,
                                            COALESCE(pih.VehicleNo, ''-'') AS VehicleNo,
                                            COALESCE(pih.VehicleType, ''-'') AS VehicleType
                                        FROM PurchaseInvoiceHeaders pih 
                                        LEFT OUTER JOIN Vendors v ON pih.VendorID = v.VendorID 
                                        INNER JOIN VendorGroups vg ON v.VendorGroupID = vg.VendorGroupID
                                        LEFT OUTER JOIN (
                                            SELECT DISTINCT 
                                                PurchaseInvoiceNo,
                                                SUM(ISNULL(subtotal, 0)) AS Subtotal,
                                                SUM(ISNULL(VDSAmount, 0)) AS VDSAmount
                                            FROM PurchaseInvoiceDetails
                                            GROUP BY PurchaseInvoiceNo
                                        ) VDS ON pih.PurchaseInvoiceNo = VDS.PurchaseInvoiceNo
                                        WHERE 1 = 1
                                        AND (' + @filter + ')
                                    );

                                EXEC sp_executesql @filterquery;
                            
                            ";
                }
                dbConn = new SqlConnection(SessionModel.sslConn);
                dbConn.Open();       

                cmd = new SqlCommand(query, dbConn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@skip", gridOption.skip));
                cmd.Parameters.Add(new SqlParameter("@take", gridOption.take));  // Fix the typo here
                cmd.Parameters.Add(new SqlParameter("@filter", filterby));
                cmd.Parameters.Add(new SqlParameter("@orderby", orderby.Trim()));
                cmd.Parameters.Add(new SqlParameter("@param1", param1));
                cmd.Parameters.Add(new SqlParameter("@param2", param2));
                cmd.Parameters.Add(new SqlParameter("@param3", param3));
                cmd.Parameters.Add(new SqlParameter("@param4", param4));
                cmd.Parameters.Add(new SqlParameter("@param5", param5));
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dbConn.Close();
                dbConn.Dispose();
                cmd.Dispose();

                dt = ds.Tables[1];
                totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                var dataList = (List<T>)ListConversion.ConvertTo<T>(dt).ToList();
                var result = new GridResult<T>().Data(dataList, totalCount);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
