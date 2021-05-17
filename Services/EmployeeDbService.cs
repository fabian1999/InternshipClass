﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RazorMvc.Data;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    public class EmployeeDbService
    {
        private readonly InternDbContext db;
        private IConfiguration configuration;

        public EmployeeDbService(InternDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        public Employee AddEmployee(Employee employee)
        {
            db.Employees.AddRange(employee);
            db.SaveChanges();
            return employee;
        }

        public IList<Employee> GetEmployees()
        {
            var employees = db.Employees.ToList();
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            return db.Find<Employee>(id);
        }

        public void UpdateEmployee(Employee employee)
        {
            var itemToBeUpdated = GetEmployeeById(employee.Id);
            itemToBeUpdated.FirstName = employee.FirstName;
            itemToBeUpdated.LastName = employee.LastName;
            itemToBeUpdated.Email = employee.Email;

            db.Employees.Update(itemToBeUpdated);
            db.SaveChanges();
        }

        public void RemoveEmployee(int id)
        {
            var employee = GetEmployeeById(id);
            if (employee == null)
            {
                return;
            }

            db.Remove<Employee>(employee);
            db.SaveChanges();
        }
    }
}
