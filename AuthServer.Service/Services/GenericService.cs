using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using AuthServer.Service.Mappings;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTOs;

namespace AuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return ResponseDto<TDto>.Fail($"{typeof(TDto).Name}({id}) not found", 404, true);
            }
            var dto = ObjectMapper.Mapper.Map<TDto>(entity);
            return ResponseDto<TDto>.Success(dto, 200);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var entites = await _repository.GetAllAsync();
            if (entites == null)
            {
                return ResponseDto<IEnumerable<TDto>>.Fail(new ErrorDto($"{typeof(TDto).Name} not found", true), 404);
            }
            var dtos = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(entites);
            return ResponseDto<IEnumerable<TDto>>.Success(dtos, 200);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> WhereAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _repository.Where(expression).ToList();
            if (entities == null)
            {
                return ResponseDto<IEnumerable<TDto>>.Fail(new ErrorDto($"{typeof(TDto).Name} not found", true), 404);
            }
            var dtos = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(entities);
            return ResponseDto<IEnumerable<TDto>>.Success(dtos, 200);
        }

        public async Task<ResponseDto<TDto>> AddAsync(TDto dto)
        {
            var newEntity = await _repository.AddAsync(ObjectMapper.Mapper.Map<TEntity>(dto));
            var responseDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<TDto>.Success(responseDto, 201);
        }

        public async Task<ResponseDto<NoContentDto>> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return ResponseDto<NoContentDto>.Fail($"{typeof(TDto).Name}({id}) not found", 404, true);
            }
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(204);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateAsync(TDto dto, int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return ResponseDto<NoContentDto>.Fail($"{typeof(TDto).Name}({id}) not found", 404, true);
            }
            _repository.Update(ObjectMapper.Mapper.Map(dto, entity));
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(204);
        }
    }
}
