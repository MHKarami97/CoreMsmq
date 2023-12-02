using CoreMsmq;
using MSMQ.Messaging;

const int maxCount = 100;
Console.WriteLine("start");

const string queueName = @".\private$\CoreMsmq";
Console.WriteLine($"QueueName : {queueName}");

QueueMaker.CreateQueue(queueName, false);

var queue = new MessageQueue(queueName);

for (var i = 0; i < maxCount; i++)
{
    queue.Send($"test-{i}", $"label-test-{i}");
}

Console.WriteLine("finish");