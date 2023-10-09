using SagaPattern;
using static SagaPattern.SampleSetup;

Console.WriteLine("Saga Pattern Test!");

var activity1 = new Activity1();
var activity2 = new Activity2();
var activity3 = new Activity3();

Saga saga = new Saga();
saga.GetActivities().Add(activity1);
saga.GetActivities().Add(activity2);
saga.GetActivities().Add(activity3);

Console.WriteLine("Saga started ...");
var cancellationTokenSource = new CancellationTokenSource();
var status = await saga.Run(cancellationTokenSource.Token);

Console.WriteLine($"Saga returned {status.ToString()}");
