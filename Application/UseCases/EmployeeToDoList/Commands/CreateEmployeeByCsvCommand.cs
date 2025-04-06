﻿using Application.Models.EmployeeModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class CreateEmployeeByCsvCommand : IRequest<List<EmployeeViewModel>>
    {
        [Required]
        public IFormFile CsvFile { get; set; } = null!;
    }
}
