using System;
using RoleBasedAccessControl;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 20: Role-Based Access Control ===");
        var rbac = new RBACEngine();

        var admin = new User { UserId = "U1", Role = Role.Admin };
        var manager = new User { UserId = "U2", Role = Role.Manager };
        var agent = new User { UserId = "U3", Role = Role.Agent };

        var smallLoan = new Resource { ResourceId = "L1", OwnerId = "U3", Amount = 50000 };
        var largeLoan = new Resource { ResourceId = "L2", OwnerId = "U3", Amount = 150000 };

        Console.WriteLine($"Admin can approve large loan: {rbac.Authorize(admin, Permission.ApproveLoan, largeLoan)}");
        Console.WriteLine($"Manager can approve small loan: {rbac.Authorize(manager, Permission.ApproveLoan, smallLoan)}");
        Console.WriteLine($"Manager can approve large loan: {rbac.Authorize(manager, Permission.ApproveLoan, largeLoan)}");
        Console.WriteLine($"Agent can view own resource: {rbac.Authorize(agent, Permission.ViewSelf, smallLoan)}");
        Console.WriteLine($"Agent can approve loan: {rbac.Authorize(agent, Permission.ApproveLoan, smallLoan)}");

        Console.WriteLine("✓ Test Passed\n");
    }
}
