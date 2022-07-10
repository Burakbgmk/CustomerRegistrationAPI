using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Core.Services
{
    public interface ICommercialActivityService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {

    }
}
