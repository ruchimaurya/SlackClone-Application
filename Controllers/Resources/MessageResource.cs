﻿
namespace SlackClone.Controllers.Resources
{
    public class MessageResource
    {
        public int Id { get; set; }

        public int Sender { get; set; }

        public int Receiver { get; set; }

        public string Content { get; set; }

        public string DateTime { get; set; }

    }
}
