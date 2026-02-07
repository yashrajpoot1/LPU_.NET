namespace WorkflowEngine
{
    public enum LoanState
    {
        Draft,
        Submitted,
        InReview,
        Approved,
        Rejected,
        Disbursed
    }

    public enum LoanAction
    {
        Submit,
        StartReview,
        Approve,
        Reject,
        Disburse
    }

    public class StateTransition
    {
        public DateTime Timestamp { get; set; }
        public LoanState FromState { get; set; }
        public LoanState ToState { get; set; }
        public string? Reason { get; set; }
    }

    public class LoanApplication
    {
        public string ApplicationId { get; set; } = string.Empty;
        public LoanState CurrentState { get; set; } = LoanState.Draft;
        public List<StateTransition> History { get; } = new();
    }

    public class LoanWorkflowEngine
    {
        private readonly Dictionary<string, LoanApplication> _applications = new();
        private readonly Dictionary<(LoanState, LoanAction), LoanState> _transitions = new()
        {
            { (LoanState.Draft, LoanAction.Submit), LoanState.Submitted },
            { (LoanState.Submitted, LoanAction.StartReview), LoanState.InReview },
            { (LoanState.InReview, LoanAction.Approve), LoanState.Approved },
            { (LoanState.InReview, LoanAction.Reject), LoanState.Rejected },
            { (LoanState.Approved, LoanAction.Disburse), LoanState.Disbursed }
        };

        public void CreateApplication(string appId)
        {
            _applications[appId] = new LoanApplication { ApplicationId = appId };
        }

        public bool ChangeState(string appId, LoanAction action, string? reason = null)
        {
            if (!_applications.TryGetValue(appId, out var app))
                throw new ArgumentException($"Application {appId} not found");

            var key = (app.CurrentState, action);
            if (!_transitions.TryGetValue(key, out var newState))
            {
                return false;
            }

            var transition = new StateTransition
            {
                Timestamp = DateTime.UtcNow,
                FromState = app.CurrentState,
                ToState = newState,
                Reason = reason
            };

            app.History.Add(transition);
            app.CurrentState = newState;
            return true;
        }

        public LoanApplication? GetApplication(string appId)
        {
            return _applications.TryGetValue(appId, out var app) ? app : null;
        }
    }
}
