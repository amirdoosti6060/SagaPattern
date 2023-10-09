using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaPattern
{
    public class SampleSetup
    {
        public class InteractiveActivity
        {
            public ActivityStatus InteractiveExecute(string activityName)
            {
                ActivityStatus status;
                Console.WriteLine($"\nActivity {activityName} executing ...");
                Console.Write("Enter Return Status (Y=Succeeded, N=Failed)? ");
                var reply = Console.ReadLine();
                if (reply != null)
                {
                    reply = reply.Trim().ToLower();
                    if (reply == "y" || reply == "yes")
                        status = ActivityStatus.Succeeded;
                    else
                        status = ActivityStatus.Failed;
                }
                else
                    status = ActivityStatus.Failed;

                if (status == ActivityStatus.Succeeded)
                    Console.WriteLine($"Activity {activityName} Execution Succeeded.");
                else
                    Console.WriteLine($"Activity {activityName} Execution Failed!");

                return status;
            }

            public ActivityStatus InteractiveCompensate(string activityName)
            {
                ActivityStatus status;
                Console.WriteLine($"\nActivity {activityName} compensating ...");
                Console.Write("Enter Return Status (Y=Succeeded, N=Failed)? ");
                var reply = Console.ReadLine();
                if (reply != null)
                {
                    reply = reply.Trim().ToLower();
                    if (reply == "y" || reply == "yes")
                        status = ActivityStatus.Succeeded;
                    else
                        status = ActivityStatus.Failed;
                }
                else
                    status = ActivityStatus.Failed;

                if (status == ActivityStatus.Succeeded)
                    Console.WriteLine($"Activity {activityName} Compensation Succeeded.");
                else
                    Console.WriteLine($"Activity {activityName} Compensation Failed!");

                return status;
            }
        }

        public class Activity1 : InteractiveActivity, IActivity
        {
            async Task<ActivityStatus> IActivity.ExecuteAsync()
            {
                var task = Task.Run<ActivityStatus>(() =>
                {
                    return InteractiveExecute("One");
                });

                return await task;
            }

            async Task<ActivityStatus> IActivity.CompensateAsync()
            {
                var task = Task.Run<ActivityStatus>(() =>
                {
                    return InteractiveCompensate("One");
                });

                return await task;
            }
        }

        public class Activity2 : InteractiveActivity, IActivity
        {
            async Task<ActivityStatus> IActivity.ExecuteAsync()
            {
                var task = Task.Run<ActivityStatus>(() =>
                {
                    return InteractiveExecute("Two");
                });

                return await task;
            }

            async Task<ActivityStatus> IActivity.CompensateAsync()
            {
                var task = Task.Run<ActivityStatus>(() =>
                {
                    return InteractiveCompensate("Two");
                });

                return await task;
            }
        }


        public class Activity3 : InteractiveActivity, IActivity
        {
            async Task<ActivityStatus> IActivity.ExecuteAsync()
            {
                var task = Task.Run<ActivityStatus>(() =>
                {
                    return InteractiveExecute("Three");
                });

                return await task;
            }

            async Task<ActivityStatus> IActivity.CompensateAsync()
            {
                var task = Task.Run<ActivityStatus>(() =>
                {
                    return InteractiveCompensate("Three");
                });

                return await task;
            }
        }


    }
}
