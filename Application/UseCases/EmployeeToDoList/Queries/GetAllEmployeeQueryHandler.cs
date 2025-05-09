﻿using Application.Common.Interfaces;
using Application.Models.EmployeeModels;
using AutoMapper;
using Domain.Configurations;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.UseCases.EmployeeToDoList.Queries
{
    public class GetAllEmployeeQueryHandler(
        IAppDbContext appDbContext,
        IMapper mapper
        ) : IRequestHandler<GetAllEmployeeQuery, ResponseViewModel<List<EmployeeViewModel>>>
    {
        private readonly IAppDbContext _dbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        async Task<ResponseViewModel<List<EmployeeViewModel>>> IRequestHandler<GetAllEmployeeQuery, ResponseViewModel<List<EmployeeViewModel>>>.Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Employee> query = _dbContext.Employees.AsQueryable().OrderByDescending(x => x.CreatedAt);

                if (!string.IsNullOrEmpty(request.Filter?.SearchingText))
                {
                    var searchText = request.Filter.SearchingText.Trim().ToLower();
                    query = query.Where(e => e.Forenames.ToLower().Contains(searchText.ToLower()) ||
                                              e.Surname.ToLower().Contains(searchText.ToLower()) ||
                                              e.Payroll_Number.ToLower().Contains(searchText.ToLower()));
                }

                query = ApplySorting(query, request.Filter?.Ascending ?? false);

                int Count = query.Count();

                query = ApplyPagination(query, request.PaginationParams);

                var employees = await query.ToListAsync(cancellationToken);
                var mappedEmployee = _mapper.Map<List<EmployeeViewModel>>(employees);
                return new ResponseViewModel<List<EmployeeViewModel>>
                {
                    Data = mappedEmployee,
                    TotalCount = Count,
                    PageIndex = request.PaginationParams?.PageIndex ?? 0,
                    PageSize = request.PaginationParams?.PageSize ?? 20
                };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel<List<EmployeeViewModel>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null,
                    Error = ex,
                    TotalCount = 0,
                    PageIndex = request.PaginationParams?.PageIndex ?? 0,
                    PageSize = request.PaginationParams?.PageSize ?? 20
                };
            }
        }

        private IQueryable<Employee> ApplyPagination(IQueryable<Employee> query, PaginationParams paginationParams)
        {
            var pageIndex = paginationParams?.PageIndex ?? 0;
            var pageSize = paginationParams?.PageSize ?? 20;

            if (pageIndex < 0) pageIndex = 0;
            if (pageSize <= 0) pageSize = 20;

            return query.Skip(pageIndex * pageSize).Take(pageSize);
        }

        private IQueryable<Employee> ApplySorting(IQueryable<Employee> query, bool ascending)
        {
            return ascending ? query.OrderBy(e => e.Surname) : query.OrderByDescending(e => e.Surname);
        }


    }

}
