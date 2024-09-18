using iTrack.Data;
using iTrack.Models.Responses;
using iTrack.Data;
using Microsoft.EntityFrameworkCore;
using iTrack.Models.DTOs;
using iTrack.Models;

namespace iTrack.Services;

public interface IClientServices
{
    Task<GetClientResponse> GetClients();
    Task<BaseResponse>AddClient(AddClientForm form);
    Task<_GetClientResponse> GetClient(int id);
    Task<BaseResponse> DeleteClient(Client client);
    Task<BaseResponse> EditClient(Client client);
  
}

public class ClientServices: IClientServices
{
    private readonly IDbContextFactory<DataContext> _factory;

    public ClientServices(IDbContextFactory<DataContext> factory)
    {
        _factory = factory;
    }

	public async Task<BaseResponse> AddClient(AddClientForm form)
	{
		var response = new BaseResponse();
        try
        {
            using (var context = _factory.CreateDbContext())
            {
                context.Add(new Client
                {
                    Name = form.Name,
                    Email = form.Email,
                    ImgUrl = form.ImgUrl,
                    Enrolment = form.Enrolment,
                    ServicePrice = form.ServicePrice,
                    Status = form.Status,
                    Debt = form.Debt
                });

                var result = await context.SaveChangesAsync();

                if(result == 1)
                {
					response.SatusCode = 200;
					response.Message = "Client added successfully";
				}
				else
                {
					response.SatusCode = 400;
					response.Message = "Error adding client";
				}
            }
        }
        catch (Exception ex)
        {
			response.SatusCode = 500;
			response.Message = "Error adding client:"+ex.Message;
		}
        return response;
	}



	public async Task<GetClientResponse> GetClients()
    {
        var response = new GetClientResponse();

        try
        {
            using(var context = _factory.CreateDbContext())
            {
                response.Clients = await context.Clients.ToListAsync();
                response.SatusCode = 200;
                response.Message = "Clients retrieved successfully";
            }
        }
        catch (Exception ex)
        {
            response.SatusCode = 500;
            response.Message = "Error retrieving clients:"+ex.Message;
            response.Clients = null;
        }

        return response;
       
    }

	public async Task<BaseResponse> DeleteClient(Client client)
	{
		var response = new BaseResponse();
		try
		{
			using (var context = _factory.CreateDbContext())
			{
				context.Remove(client);
				

				var result = await context.SaveChangesAsync();

				if (result == 1)
				{
					response.SatusCode = 200;
					response.Message = "Client deleted successfully";
				}
				else
				{
					response.SatusCode = 400;
					response.Message = "Error deliting client";
				}
			}
		}
		catch (Exception ex)
		{
			response.SatusCode = 500;
			response.Message = "Error deliting client:" + ex.Message;
		}
		return response;
	}

	public async Task<BaseResponse> EditClient(Client client)
	{
		var response = new BaseResponse();
		try
		{
			using (var context = _factory.CreateDbContext())
			{
				context.Update(client);

				var result = await context.SaveChangesAsync();

				if (result == 1)
				{
					response.SatusCode = 200;
					response.Message = "Client edited successfully";
				}
				else
				{
					response.SatusCode = 400;
					response.Message = "Error editing client";
				}
			}
		}
		catch (Exception ex)
		{
			response.SatusCode = 500;
			response.Message = "Error editing client:" + ex.Message;
		}
		return response;
	}

	public async Task<_GetClientResponse> GetClient(int id)
	{
		var response = new _GetClientResponse();
		try
		{
			using (var context = _factory.CreateDbContext())
			{
				var client =  await context.Clients.FirstOrDefaultAsync(x=>x.Id==id);
				response.SatusCode = 200;
				response.Message = "Success";
                response.Client = client;
			}
		}
		catch (Exception ex)
		{
			response.SatusCode = 500;
			response.Message = "Error retrieving clients:" + ex.Message;
			response.Client = null;
		}

		return response;
	}
}

