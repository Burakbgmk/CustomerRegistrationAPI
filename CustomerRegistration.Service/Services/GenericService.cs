using CustomerRegistration.Core.Services;
using CustomerRegistration.Core.UnitOfWork;
using CustomerRegistration.Core.Repositories;
using CustomerRegistration.Service.Mappers;
using SharedLibrary.DTOs;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IGenericRepository<TEntity, TDto> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<TEntity, TDto> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task<Response<TDto>> CreateAsync(TDto dto)
        {
            var entity = ObjectMapper.Mapper.Map<TEntity>(dto);
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(entity);
            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<TDto>> RemoveAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
                return Response<TDto>.Fail("Id is not found!",404,true);
            _genericRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return Response<TDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = await _genericRepository.GetAllAsync();
            if (entities == null)
                return Response<IEnumerable<TDto>>.Fail("No data is found!",404,true);
            var dtos = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(entities);
            return Response<IEnumerable<TDto>>.Success(dtos, 200);

        }
        public async Task<Response<IEnumerable<TDto>>> GetAllAsync(int page, int pageCapacity)
        {
            var entities = await _genericRepository.GetAllAsync(page,pageCapacity);
            if (entities == null)
                return Response<IEnumerable<TDto>>.Fail("No data is found!", 404, true);
            var dtos = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(entities);
            return Response<IEnumerable<TDto>>.Success(dtos, 200);

        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
                return Response<TDto>.Fail("Id is not found!", 404, true);
            var dto = ObjectMapper.Mapper.Map<TDto>(entity);
            return Response<TDto>.Success(dto, 200);
        }

        public async Task<Response<TDto>> Update(int id, TDto dto)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
                return Response<TDto>.Fail("Id is not found!", 404, true);
            _genericRepository.Update(entity, dto);
            await _unitOfWork.CommitAsync();
            return Response<TDto>.Success(204);
        }
        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);
            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }

        
    }
}
