using SSL.Common.SSL.Common.Core.Interfaces.Services;
using SSL.CS.SSL.Common.Core.Interfaces.UnitOfWork;
using SSL.CS.SSL.Common.Models;

namespace SSL.CS.SSL.Common.Services
{
    public class CommonService : ICommonService
    {

        private IUnitOfWork _unitOfWork;

        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public IList<UserBranch> GetBranch()
        //{
        //    List<UserBranch> records = new List<UserBranch>();
        //    using (var context = _unitOfWork.Create())
        //    {

        //        try
        //        {
        //            records = context.Repositories.CommonRepository.GetBranch().Select(c => new UserBranch
        //            {
        //                UserName = c.Name,
        //                BranchName = c.Value
        //            }).ToList();
        //            context.SaveChanges();

        //            return records;
        //        }
        //        catch (Exception e)
        //        {
        //            context.RollBack();


        //        }
        //    }
        //    return records;

        //}

         

        public List<CommonDropDown> GetAllPorts()
        {
            List<CommonDropDown> records = new List<CommonDropDown>();
            using (var context = _unitOfWork.Create())
            {
                try
                {
                    records = context.Repositories.CommonRepository.GetAllPorts().Select(c => new CommonDropDown
                    {
                        Name = c.Name,
                        Value = c.Value
                    }).ToList();
                    context.SaveChanges();

                    return records;
                }
                catch (Exception e)
                {
                    context.RollBack();
                }
            }

            return records;
        }
         
        public IList<CommonDropDown> GetAllHeaders()
        {
            List<CommonDropDown> records = new List<CommonDropDown>();
            using (var context = _unitOfWork.Create())
            {
                try
                {
                    records = context.Repositories.CommonRepository.GetAllHeaders().Select(c => new CommonDropDown
                    {
                        Name = c.Value,
                        Value = c.Name
                    }).ToList();
                    context.SaveChanges();

                    return records;
                }
                catch (Exception e)
                {
                    context.RollBack();
                }
            }

            return records;
        }

        public IList<CommonDropDown> EntryTypes()
        {
            List<CommonDropDown> records = new List<CommonDropDown>();
            using (var context = _unitOfWork.Create())
            {
                try
                {
                    records = context.Repositories.CommonRepository.EntryTypes().Select(c => new CommonDropDown
                    {
                        Name = c.Value,
                        Value = c.Name
                    }).ToList();
                    context.SaveChanges();

                    return records;
                }
                catch (Exception e)
                {
                    context.RollBack();
                }
            }

            return records;
        }

        //BANK 

        public IList<CommonDropDown> DocumentType()
        {

            List<CommonDropDown> records = new List<CommonDropDown>();
            using (var context = _unitOfWork.Create())
            {
                try
                {
                    records = context.Repositories.CommonRepository.DocumentType().Select(c => new CommonDropDown
                    {
                        Name = c.Value,
                        Value = c.Name
                    }).ToList();
                    context.SaveChanges();

                    return records;
                }
                catch (Exception e)
                {
                    context.RollBack();
                }
            }

            return records;


            ///List<CommonDropDown> records = new List<CommonDropDown>();
            //using var context = _unitOfWork.Create();
            //try
            //{
            ///    records = context.Repositories.CommonRepository.DocumentType().Select(c => new CommonDropDown
            //    {
            //        Name = c.Value,
            //        Value = c.Name
            //    }).ToList();
            //    context.SaveChanges();

            //    return records;
            //}
            //catch (Exception e)
            //{
            //    context.RollBack();
            //}

            //return records;
        }
         

        public List<CommonDropDown> ApplyMethod()
        {
            List<CommonDropDown> records = new List<CommonDropDown>();
            using (var context = _unitOfWork.Create())
            {
                try
                {
                    records = context.Repositories.CommonRepository.ApplyMethod().Select(c => new CommonDropDown
                    {
                        Name = c.Value,
                        Value = c.Name
                    }).ToList();
                    context.SaveChanges();

                    return records;
                }
                catch (Exception e)
                {
                    context.RollBack();
                }
            }

            return records;
        }

        public List<CommonDropDown> TransactionType()
        {
            List<CommonDropDown> records = new List<CommonDropDown>();
            using (var context = _unitOfWork.Create())
            {
                try
                {
                    records = context.Repositories.CommonRepository.TransactionType().Select(c => new CommonDropDown
                    {
                        Name = c.Value,
                        Value = c.Name
                    }).ToList();
                    context.SaveChanges();

                    return records;
                }
                catch (Exception e)
                {
                    context.RollBack();
                }
            }

            return records;
        }

        public List<CommonDropDown> OrderBy()
        {
            List<CommonDropDown> records = new List<CommonDropDown>();
            using (var context = _unitOfWork.Create())
            {
                try
                {
                    records = context.Repositories.CommonRepository.OrderBy().Select(c => new CommonDropDown
                    {
                        Name = c.Value,
                        Value = c.Name
                    }).ToList();
                    context.SaveChanges();

                    return records;
                }
                catch (Exception e)
                {
                    context.RollBack();
                }
            }

            return records;
        }
          




    }
}
