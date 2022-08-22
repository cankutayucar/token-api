using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.DTOs;

namespace AuthServer.Core.Services
{
    public interface IGenericService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<ResponseDto<TDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();
        Task<ResponseDto<IEnumerable<TDto>>> WhereAsync(Expression<Func<TEntity, bool>> expression);
        Task<ResponseDto<TDto>> AddAsync(TDto dto);
        Task<ResponseDto<NoContentDto>> DeleteAsync(int id);
        Task<ResponseDto<NoContentDto>> UpdateAsync(TDto dto, int id);
    }
}
