namespace PassIn.Infrastructure.Entities;

public class Attendee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid Event_Id { get; set; }
    public DateTime Created_At { get; set; }
    //Essa cópdigo abaixo é nescessário pelo fato do banco de dados estar errado, a relação de participantes e checkin era pra ser feita 1 pra 1 e esta 1 pra n
    public CheckIn? Checkin { get; set; }
}