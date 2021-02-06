using Abp.Application.Services;
using Abp.Dependency;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Notification
{
    public class FirebaseAppService: IFirebaseAppService, ISingletonDependency
    {
        private FirebaseApp app;
        public FirebaseAppService()
        {
         
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json")),
                });
            

        }


        [RemoteService(false)]
        public async Task<String> SendMessage(Message message)
        {
            var messaging = FirebaseMessaging.DefaultInstance;
            return await messaging.SendAsync(message).ConfigureAwait(false);
        }
    }
}
