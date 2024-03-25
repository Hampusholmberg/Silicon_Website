using Infrastructure.Entities;
using Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Infrastructure.Services;

public class ContactService
{
    private readonly string _apiKey = "?key=ZTMwZjkzYzUtMzg2My00MzBlLThiNjItMzU2ZGQ1NTIxMTBi";

    public async Task<ContactResponse> SendContactRequest(ContactRequestEntity contact)
    {
        string success = "Your message has been sent!";
        string badRequest = "Something went wrong, your message has not been sent!";
        string error = "Could not connect to the web server, please try again later.";

        ContactResponse response = new();

        using var http = new HttpClient();
        var contactRequestAsJson = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

        try
        {
            var result = await http.PostAsync($"https://localhost:7153/api/contact{_apiKey}", contactRequestAsJson);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    response.SuccessMessage = success;
                    return response;
                
                case HttpStatusCode.BadRequest:
                    response.ErrorMessage = badRequest;
                    return response;

                case HttpStatusCode.InternalServerError:
                    response.ErrorMessage = error;
                    return response;
            }
        }
        catch
        {
            response.ErrorMessage = error;
            return response;
        }

        response.ErrorMessage = badRequest;
        return response;
    }
}