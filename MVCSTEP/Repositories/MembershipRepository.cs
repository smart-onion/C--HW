using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Repositories;

public class MembershipRepository : IMembership
{
    private readonly ApplicationContext _context;

    public MembershipRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Membership>> GetAllMembershipsAsync()
    {
        return await _context.Memberships.ToListAsync();
    }

    public async Task<Membership> GetMembershipAsync(int id)
    {
        return await _context.Memberships.FindAsync(id);
    }

    public async Task<bool> ExistsMembershipByCodeAsync(string code)
    {
        return await _context.Memberships.AnyAsync(m => m.Code == code);
    }

    public async Task<bool> EnableCodeMembershipByCodeAsync(string code)
    {
        var member = await _context.Memberships.FirstOrDefaultAsync(m => m.Code == code);
        if (member == null) return false;
        member.IsEnable =  true;
        await _context.SaveChangesAsync();
        return true;
        
    }

    public async Task DisableMembershipCodeAsync(string code)
    {
        var member = await _context.Memberships.FirstOrDefaultAsync(m => m.Code == code);
        if (member == null) return;
        member.IsEnable =  false;
        await _context.SaveChangesAsync();
    }

    public async Task AddMembershipAsync(Membership membership)
    {
        _context.Memberships.Add(membership);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMembershipAsync(Membership membership)
    {
        _context.Memberships.Remove(membership);
        await _context.SaveChangesAsync();
    }
    
}