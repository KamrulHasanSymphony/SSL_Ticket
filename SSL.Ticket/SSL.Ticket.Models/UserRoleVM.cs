using System.ComponentModel.DataAnnotations;

namespace SSL.Ticket.SSL.Ticket.Models
{
    public class UserRoleVM
    {
        public int Id { get; set; }
        [Display(Name = "Role Name")]
        public string Name { get; set; }

        public string Operation { get; set; }
        public string msg { get; set; }

        public Audit Audit;
        public UserRoleVM()
        {
            Audit = new Audit();
        }

    }

    public class MenuVM
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public int ParentId { get; set; }
        public int DisplayOrder { get; set; }

        public bool IsChecked { get; set; }
    }

    public class RoleMenuVM
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int MenuId { get; set; }
        public bool List { get; set; }
        public bool Insert { get; set; }
        public bool Delete { get; set; }
        public bool Post { get; set; }
        public bool IsChecked { get; set; }
        public int ParentId { get; set; }

        public string RoleName { get; set; }
        public string MenuName { get; set; }

        public string BranchName { get; set; }
        [Display(Name = "Branch Name")]
        public int BranchId { get; set; }
        public List<string> IDs { get; set; }
        public string Operation { get; set; }
        public string msg { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }

        public Audit Audit;
        public RoleMenuVM()
        {
            roleMenuList = new List<RoleMenuVM>();
            Audit = new Audit();
        }

        public List<RoleMenuVM> roleMenuList { get; set; }

    }

    public class UserMenuVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool List { get; set; }
        public bool Insert { get; set; }
        public bool Delete { get; set; }
        public bool Post { get; set; }
        public bool IsChecked { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string MenuName { get; set; }
        public int ParentId { get; set; }
        public int DisplayOrder { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string BranchName { get; set; }
        [Display(Name = "Branch Name")]
        public int BranchId { get; set; }
        public List<string> IDs { get; set; }
        public string Operation { get; set; }
        public string msg { get; set; }
        public Audit Audit;
        public UserMenuVM()
        {
            Audit = new Audit();
            userMenuList = new List<UserMenuVM>();
        }

        public List<UserMenuVM> userMenuList { get; set; }

    }


    public class UserMenu
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool List { get; set; }
        public bool Insert { get; set; }
        public bool Delete { get; set; }
        public bool Post { get; set; }
        public string MenuName { get; set; }
        public string IconClass { get; set; }
        public int ParentId { get; set; }
        public int DisplayOrder { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public int TotalChild { get; set; }


    }
}
