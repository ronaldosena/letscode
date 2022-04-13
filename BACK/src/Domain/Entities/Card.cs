using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Card: IBaseEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Group { get; set; }
    }
}
