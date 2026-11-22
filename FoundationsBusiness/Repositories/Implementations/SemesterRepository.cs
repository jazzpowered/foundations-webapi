using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;

namespace FoundationsBusiness.Repositories.Implementations;

internal class SemesterRepository(IDbConnectionFactory connectionFactory)
    : Repository<Semester>(connectionFactory, "dbo.Semesters", "Id", "Name"),
        ISemesterRepository
{
}
