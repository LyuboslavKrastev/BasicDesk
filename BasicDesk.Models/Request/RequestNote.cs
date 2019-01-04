using System;

namespace BasicDesk.Models
{
    public class RequestNote
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }
		public User Creator{get; set;}       
		
		public DateTime CreationTime {get; set;}

        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}
