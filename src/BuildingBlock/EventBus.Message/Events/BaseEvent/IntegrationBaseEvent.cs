using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Message.Events.BaseEvent
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = DateTime.UtcNow;
        }
        public IntegrationBaseEvent(string id, DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }

        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
