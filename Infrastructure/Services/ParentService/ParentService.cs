namespace Infrastructure.Services.ParentService.cs;

using Domain.Dtos;
using Domain.Entities;
using Domain.Filters.ParentFilter;
using Domain.Responses;
using global::AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

public class ParentService : IParentService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ParentService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper; 
    }

    async public Task<Response<List<GetParentDto>>> GetAllParents(GetParentFilter filter)
    {
        var parents = _dataContext.Parents.AsQueryable();
        // if (string.IsNullOrEmpty(filter.FirstName)==false)
        // parents=parents.Where(p=>p.FirstName.ToLower().Contains(filter.FirstName));
        var totalRecord = await parents.CountAsync();
        var result = _mapper.Map<List<GetParentDto>>(parents);
        return new PagedResponse<List<GetParentDto>>(result, filter.PageNumber, filter.PageSize, totalRecord);

    }

    async public Task<Response<GetParentDto>> GetParentById(int id)
    {
        var parent = await _dataContext.Parents.FindAsync(id);
        var result = _mapper.Map<GetParentDto>(parent);
        return new Response<GetParentDto>(result);
    }
    async public Task<Response<GetParentDto>> AddParent(AddParentDto parent)
    {
        var mapped = _mapper.Map<Parent>(parent);
        await _dataContext.Parents.AddAsync(mapped);
        await _dataContext.SaveChangesAsync();
        var result = _mapper.Map<GetParentDto>(parent);
        return new Response<GetParentDto>(result);


    }
    async public Task<Response<GetParentDto>> UpdateParent(AddParentDto parent)
    {
        var query = await _dataContext.Parents.AsQueryable().FirstAsync(p => p.Id == parent.Id);
        var result = _mapper.Map<Parent>(query);
        await _dataContext.Parents.AddAsync(result);
        _dataContext.SaveChanges();
        var response = _mapper.Map<GetParentDto>(result);
        return new Response<GetParentDto>(response);

    }
    async public Task<Response<bool>> DeleteParent(int id)
    {
        var parent = await _dataContext.Parents.FindAsync(id);
        _dataContext.Parents.Remove(parent);
        await _dataContext.SaveChangesAsync();
        return new Response<bool>(System.Net.HttpStatusCode.OK, "Parent was delete successfuly");

    }


}
