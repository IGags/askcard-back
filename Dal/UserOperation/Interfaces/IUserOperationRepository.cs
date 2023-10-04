using System;
using Core.RepositoryBase.Repository.Interfaces;

namespace Dal.UserOperation.Interfaces;

public interface IUserOperationRepository : IRepository<UserOperationDal, Guid>
{
    
}