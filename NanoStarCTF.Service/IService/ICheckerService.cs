using NanoStarCTF.Domain.Entitys;

namespace NanoStarCTF.Service.IService
{
    public interface ICheckerService
    {
        ValueTask<bool> Check(Checker checkDto);
    }
}
