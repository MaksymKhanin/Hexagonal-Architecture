// This code is under Copyright (C) 2021 of Cegid SAS all right reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Consumer
{
    public interface ICanGetPayload
    {
        public Task<Payload> GetAsync(Guid id);
    }
}
