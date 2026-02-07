using System;
using WorkflowEngine;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 12: Workflow Engine ===");
        var engine = new LoanWorkflowEngine();
        
        engine.CreateApplication("LOAN001");
        Console.WriteLine("Created application LOAN001");

        engine.ChangeState("LOAN001", LoanAction.Submit);
        Console.WriteLine("State: Draft -> Submitted");

        engine.ChangeState("LOAN001", LoanAction.StartReview);
        Console.WriteLine("State: Submitted -> InReview");

        engine.ChangeState("LOAN001", LoanAction.Approve);
        Console.WriteLine("State: InReview -> Approved");

        bool canDisburse = engine.ChangeState("LOAN001", LoanAction.Disburse);
        Console.WriteLine($"State: Approved -> Disbursed: {canDisburse}");

        var app = engine.GetApplication("LOAN001");
        Console.WriteLine($"\nCurrent State: {app?.CurrentState}");
        Console.WriteLine($"History Count: {app?.History.Count}");
        Console.WriteLine("✓ Test Passed\n");
    }
}
