using FoundationsBusiness.Models;
using FoundationsBusiness.Repositories.Interfaces;
using FoundationsBusiness.Services.Interfaces;

namespace FoundationsBusiness.Services.Implementations;

internal class SemesterService(ISemesterRepository semesterRepository)
    : Service<Semester>(semesterRepository), ISemesterService
{
}
