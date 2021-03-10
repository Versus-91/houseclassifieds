using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using classifieds.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Jobs
{
    public class PostsJob : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<Post> _postRepository;
        public PostsJob(AbpTimer timer, IRepository<Post> postRepository): base(timer)
        {
            _postRepository = postRepository;
            Timer.Period = 5000; //5 seconds (good for tests, but normally will be more)
        }
        [UnitOfWork]
        protected override void DoWork()
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var oneMonthAgo = Clock.Now.Subtract(TimeSpan.FromDays(30));

                var inactiveUsers = _postRepository.GetAllList(u =>
                    u.IsVerified && (u.CreationTime < oneMonthAgo));

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
