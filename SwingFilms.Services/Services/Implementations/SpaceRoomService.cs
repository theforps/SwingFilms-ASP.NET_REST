using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Services.Implementations;

public class SpaceRoomService : ISpaceRoomService
{
    public string GenerateSpaceRoomCode()
    {
        string spaceRoomCode = Guid.NewGuid().ToString();
        var spaceRoomCodeBytes = System.Text.Encoding.UTF8.GetBytes(spaceRoomCode);
        return Convert.ToBase64String(spaceRoomCodeBytes).Substring(0, 6);
    }
}