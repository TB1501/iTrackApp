namespace iTrack.Models.Responses;

public class GetClientResponse: BaseResponse
{
    public List<Client> Clients { get; set; }
}
