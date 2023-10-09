namespace SagaPattern
{
    public enum SagaStatus
    {
        NotStarted,
        Running,
        Succeeded,
        Failed,
        UnexpectedError
    }

    public enum ActivityStatus
    {
        Succeeded,
        Failed
    }

    public class Saga
    {
        protected SagaContext Context { get; set; }

        public Saga()
        {
            Context = new SagaContext();
            Context.SagaId = new Guid();
            Context.Status = SagaStatus.NotStarted;
            Context.Activities = new List<IActivity>();
        }

        public IList<IActivity> GetActivities()
        {
            return Context.Activities!;
        }

        private async Task<ActivityStatus> ExecutingActivities(CancellationToken cancellationToken)
        {
            ActivityStatus activityStatus = ActivityStatus.Failed;
            IActivity activity;

            for (Context.CurrentActivity = 0; Context.CurrentActivity < Context.Activities!.Count; ++Context.CurrentActivity)
            {
                Context.LastActivity = Context.CurrentActivity;

                activity = Context.Activities[Context.CurrentActivity];
                try
                {
                    activityStatus = await activity.ExecuteAsync();
                }
                catch
                {
                    activityStatus = ActivityStatus.Failed;
                }

                if (cancellationToken.IsCancellationRequested)
                    break;

                if (activityStatus == ActivityStatus.Failed)
                    break;
            }

            return activityStatus;
        }

        private async Task<ActivityStatus> CompensatingActivities()
        {
            ActivityStatus activityStatus = ActivityStatus.Succeeded;
            IActivity activity;

            --Context.CurrentActivity;

            Context.Status = SagaStatus.Failed;
            for (; Context.CurrentActivity >= 0; --Context.CurrentActivity)
            {
                activity = Context.Activities![Context.CurrentActivity];
                try
                {
                    if (await activity.CompensateAsync() != ActivityStatus.Succeeded)
                        activityStatus = ActivityStatus.Failed;
                }
                catch
                {
                    activityStatus = ActivityStatus.Failed;
                }
            }

            return activityStatus;
        }

        public async Task<SagaStatus> Run(CancellationToken cancellationToken)
        {
            if (Context.Activities!.Count == 0)
                return Context.Status;

            Context.Status = SagaStatus.Running;
            ActivityStatus activityStatus;

            activityStatus = await ExecutingActivities(cancellationToken);

            if (activityStatus == ActivityStatus.Succeeded)
            {
                Context.Status = SagaStatus.Succeeded;
                return Context.Status;
            }
            
            if (Context.CurrentActivity == 0)
            {
                Context.Status = SagaStatus.Failed;
                return Context.Status;
            }

            activityStatus = await CompensatingActivities();

            if (activityStatus == ActivityStatus.Failed)
                Context.Status = SagaStatus.UnexpectedError;

            return Context.Status;
        }
    }
}
