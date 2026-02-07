namespace RoleBasedAccessControl
{
    public enum Role
    {
        Admin,
        Manager,
        Agent
    }

    public enum Permission
    {
        CreateLoan,
        ApproveLoan,
        RejectLoan,
        ViewAll,
        ViewSelf
    }

    public class User
    {
        public string UserId { get; set; } = string.Empty;
        public Role Role { get; set; }
    }

    public class Resource
    {
        public string ResourceId { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    public class RBACEngine
    {
        private const decimal ManagerApprovalLimit = 100000;

        public bool Authorize(User user, Permission permission, Resource? resource = null)
        {
            return user.Role switch
            {
                Role.Admin => AuthorizeAdmin(permission),
                Role.Manager => AuthorizeManager(permission, resource),
                Role.Agent => AuthorizeAgent(user, permission, resource),
                _ => false
            };
        }

        private bool AuthorizeAdmin(Permission permission)
        {
            return true;
        }

        private bool AuthorizeManager(Permission permission, Resource? resource)
        {
            return permission switch
            {
                Permission.CreateLoan => true,
                Permission.ApproveLoan => resource != null && resource.Amount <= ManagerApprovalLimit,
                Permission.RejectLoan => true,
                Permission.ViewAll => true,
                Permission.ViewSelf => true,
                _ => false
            };
        }

        private bool AuthorizeAgent(User user, Permission permission, Resource? resource)
        {
            return permission switch
            {
                Permission.CreateLoan => true,
                Permission.ViewSelf => resource != null && resource.OwnerId == user.UserId,
                _ => false
            };
        }
    }
}
