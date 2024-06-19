﻿using SiGaHRMS.Data.DataContext;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;


namespace SiGaHRMS.Data.Repository;

public class EmployeeSalaryStructureRepository : GenericRepository<EmployeeSalaryStructure>, IEmployeeSalaryStructureRepository
{
    public EmployeeSalaryStructureRepository(AppDbContext Context) : base(Context)
    {
    }

}
