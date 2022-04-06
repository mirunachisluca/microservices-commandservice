using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);

        IEnumerable<Command> GetAllCommandsForPlatform(int platformId);
        Command GetCommandForPlatform(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}
