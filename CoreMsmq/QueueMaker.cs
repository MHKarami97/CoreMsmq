using MSMQ.Messaging;

namespace CoreMsmq
{
    public static class QueueMaker
    {
        public static void CreateQueue(string queueName, bool useJournalQueue, bool isTransactional = false)
        {
            const string pathPrefix = @".\private$\";
            var qPath = queueName;

            if (!qPath.Contains(pathPrefix, StringComparison.CurrentCultureIgnoreCase))
            {
                qPath = pathPrefix + queueName;
            }

            if (MessageQueue.Exists(qPath))
                return;

            MessageQueue.Create(qPath, isTransactional);
            using var messageQ = new MessageQueue(qPath);

            if (!MessageQueue.Exists(qPath))
                return;

            messageQ.SetPermissions(@"Everyone", MessageQueueAccessRights.FullControl);
            messageQ.SetPermissions(@"ANONYMOUS LOGON", MessageQueueAccessRights.FullControl);
            messageQ.UseJournalQueue = useJournalQueue;
        }
    }
}