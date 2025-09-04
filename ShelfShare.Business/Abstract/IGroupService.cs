using ShelfShare.Business.DTOs.FamilyDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.Abstract
{
    public interface IGroupService
    {
        Task<GroupDto> CreateGroupAsync(CreateGroupDto createFamilyDto, int userId);
        Task<bool> JoinGroupAsync(string joinCode, int userId);
        Task<IEnumerable<GroupMemberDto>> GetGroupMembersAsync(int groupId);
        Task<GroupStatisticsDto> GetGroupStatisticsAsync(int groupId);
    }
}
