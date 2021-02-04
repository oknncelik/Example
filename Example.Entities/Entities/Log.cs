#region

using System;
using Example.Entities.Abstract;

#endregion

namespace Example.Entities.Entities
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime LogDate { get; set; }
        public int? TransactionId { get; set; }
        public string Status { get; set; }
        public int? ResultCode { get; set; }
        public string MethodName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public TimeSpan? Interval { get; set; }
    }
}