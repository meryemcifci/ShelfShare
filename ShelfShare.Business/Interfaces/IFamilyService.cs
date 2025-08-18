using ShelfShare.Business.DTOs.FamilyDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.Interfaces
{
    public interface IFamilyService
    {
        Task<FamilyDto> CreateFamilyAsync(CreateFamilyDto createFamilyDto, int userId);
        Task<bool> JoinFamilyAsync(string familyCode, int userId);
        Task<IEnumerable<FamilyMemberDto>> GetFamilyMembersAsync(int familyId);
        Task<FamilyStatisticsDto> GetFamilyStatisticsAsync(int familyId);
    }
}
