using ShelfShare.Business.Abstract;
using ShelfShare.Business.DTOs.FamilyDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Business.Concrete
{
    public class GroupService : IGroupService
    {
        public Task<GroupDto> CreateGroupAsync(CreateGroupDto createFamilyDto, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupMemberDto>> GetGroupMembersAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<GroupStatisticsDto> GetGroupStatisticsAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> JoinGroupAsync(string joinCode, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
