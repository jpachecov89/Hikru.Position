using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Domain.Entities;
using Hikru.Position.Backend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hikru.Position.Backend.Infrastructure.Persistence.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly AppDbContext _context;

		public DepartmentRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Department?> GetByIdAsync(Guid id) =>
			await _context.Departments.FindAsync(id);

		public async Task<List<Department>> GetAllAsync() =>
			await _context.Departments.ToListAsync();

		public async Task AddAsync(Department department) =>
			await _context.Departments.AddAsync(department);

		public void Update(Department department) =>
			_context.Departments.Update(department);

		public void Delete(Department department) =>
			_context.Departments.Remove(department);

		public async Task<bool> ExistsAsync(Guid id) =>
			await _context.Departments.AnyAsync(x => x.Id  == id);
	}
}
