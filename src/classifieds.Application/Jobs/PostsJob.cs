using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using classifieds.Posts;
using classifieds.Settings.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Jobs
{
    public class PostsJob : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<Post> _postRepository;
        private readonly ISettingManager _settingManager;

        public PostsJob(AbpTimer timer, IRepository<Post> postRepository, ISettingManager settingManager) : base(timer)
        {
            _settingManager = settingManager;
            _postRepository = postRepository;
            Timer.Period = 60*60*8* 1000;

        }
        [UnitOfWork]
        protected override void DoWork()
        {
            int days =  int.TryParse(_settingManager.GetSettingValue(SiteSettings.ExpirationDays),out days)? days : 30;
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var numberOfDatys = Clock.Now.Subtract(TimeSpan.FromDays(days));

                var inactiveUsers = _postRepository.GetAllList(u =>
                    u.IsVerified && (u.CreationTime < numberOfDatys));

                foreach (var inactiveUser in inactiveUsers)
                {
                    inactiveUser.IsVerified = false;
                    Logger.Info(inactiveUser + " made passive since he/she did not login in last 30 days.");
                }

                CurrentUnitOfWork.SaveChanges();
            }
        }
    }
}
