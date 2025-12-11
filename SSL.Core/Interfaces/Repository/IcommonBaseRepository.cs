
using SSL_ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Core.Interfaces.Repository
{
    public interface IcommonBaseRepository<TModel> where TModel : class, new()
    {
        //bank
        List<TModel> DepositType();
        List<TModel> BankEntryType();
        //mychange
        List<TModel> CategoryType();
        List<TModel> SectionNameList(string sageLocationCode);
        //user_branch
		List<TModel> UserBranch();
        List<TModel> Branchs(string UserId);
        List<TModel> UserId();
        List<TModel> GetAllHeaders();
        List<TModel> EntryTypes();
        List<TModel> DocumentType();
        List<TModel> APDocumentType();
        List<TModel> ProrationMethod();
        List<TModel> POType();


        List<TModel> ApplyMethod();
        List<TModel> OrderBy();
        List<TModel> TransactionType();
        List<TModel> GetAllProductType();
        List<TModel> GetAllStore();
        List<TModel> GetAllUom();
        List<TModel> GetAllColor();
        List<TModel> GetAllCurrencys();

        List<TModel> GetAllDepartment();
        List<TModel> GetAllUserName();
        List<TModel> GetAllSize();
        List<TModel> GetAllProduct();
        List<TModel> GetAllVendor();
        List<TModel> GetAllOrderCategories();

        List<TModel> GetAllCustomers();

        List<TModel> GetAllPorts();
        List<TModel> GetAllPaymentTerms();
        List<TModel> GetAllBanks();

        List<TModel> GetAllInsuranceCompanies();
        List<TModel> GetAllDeliveryTerms();
        List<TModel> GetAllShipmentModes();
        List<TModel> GetAllLCCategories();
        List<TModel> GetAllMasterLC();
        List<TModel> GetAllPI();
        List<TModel> GetAllExpOrder();
        List<TModel> GetAllBtbLC();
        List<TModel> GetAllExportPIContracts();
        List<TModel> GetAllPackingMode();
        List<TModel> GetAllExpInvoice();
        List<TModel> GetAllCountry();
        List<TModel> GetAllCnFAgents();
        List<TModel> GetAllCouriers();
        List<TModel> GetAllImportTypes();

        List<TModel> GetAllEmployees();

		//List<CommonDropDown> POType();
		//List<CommonDropDown> ApplyMethod();
		//List<CommonDropDown> TransactionType();

		
        TModel NextPreviousWithBatch(int id, string status, string tableName);
        TModel NextPreviousWithBK(int id, string status, string tableName);
        TModel NextPreviousOther(int id, string status, string tableName);

        List<TModel> GetAllProductCategories();
        #region "Autocomplete"

        List<TModel> AutocompleteProduct(string Prefix); 
        List<TModel> ProductIdByName(string Name);

        List<TModel> AutocompleteRequisitionNo(string Prefix);
        #endregion "Autocomplete"
    }
}
