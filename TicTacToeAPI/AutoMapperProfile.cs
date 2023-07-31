namespace TicTacToeApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Game, GetGameDto>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                new TicTacToeDescription
                {
                    Board = src.Board,
                    NextPlayer = src.NextPlayer,
                    Winner = src.Winner,
                    GameState = src.GameState
                }));
        CreateMap<StartNewGameDto, Game>();
        CreateMap<UpdateGameDto, Game>();
        CreateMap<Game, TicTacToeDescription>();
        CreateMap<TicTacToeDescription, Game>();
    }   
}