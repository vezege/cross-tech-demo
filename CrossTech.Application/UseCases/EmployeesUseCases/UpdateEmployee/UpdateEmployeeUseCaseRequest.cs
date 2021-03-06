﻿using CrossTech.Application.Interfaces.UseCases;
using System;

namespace CrossTech.Application.UseCases.EmployeesUseCases.UpdateEmployee
{
    public class UpdateEmployeeUseCaseRequest : IUseCaseRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int PositionId { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
