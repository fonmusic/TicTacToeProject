namespace TicTacToeApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Game, GetGameDto>();
        CreateMap<StartNewGameDto, Game>();
        CreateMap<UpdateGameDto, Game>();
    }   
}