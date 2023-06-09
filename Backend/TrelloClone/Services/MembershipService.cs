﻿using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;

        public MembershipService(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task AddMembership(string username, int boardId)
        {
            var memExists = await this.MembershipExists(username, boardId);
            if (!memExists)
            {
                await _membershipRepository.AddMembership(username, boardId);
            }
            else
            {
                // TODO THROW ERROR OR IGNORE SINCE ALREADY EXISTS
            }

        }

        public async Task<bool> MembershipExists(string username, int boardId)
        {
            return await _membershipRepository.MembershipExists(username, boardId);
        }

        public async Task RemoveMembership(string username, int boardId)
        {
            var memExists = await MembershipExists(username, boardId);
            if (memExists)
            {
                await _membershipRepository.RemoveMembership(username, boardId);
            }
        }
    }
}
