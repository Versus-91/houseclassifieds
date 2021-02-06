using Abp.Application.Services;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Notification
{
    public interface IFirebaseAppService 
    {
        Task<string> SendMessage(Message message);
    }
}
