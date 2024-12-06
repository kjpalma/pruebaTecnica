namespace CRUD_MVC_EF_CORE
{
   
    public class ClientApi
    {
        private readonly HttpClient? _client;
        public ClientApi() 
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5079/Product")
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        
    }
}
