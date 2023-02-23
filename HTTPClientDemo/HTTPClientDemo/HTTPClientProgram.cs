using Newtonsoft.Json;


namespace HTTPClientDemo
{
    public class HTTPClientProgram
    {
        public static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

            HttpResponseMessage responce = httpClient.GetAsync("todos").Result;
            if (responce.IsSuccessStatusCode)
            {
                string todo = responce.Content.ReadAsStringAsync().Result.ToString();
                //Console.WriteLine(todo);
                List<Todo> todo1 = JsonConvert.DeserializeObject<List<Todo>>(todo.ToString());
                foreach (var item in todo1)
                {
                    Console.WriteLine(item.userId);
                    Console.WriteLine(item.id);
                    Console.WriteLine(item.title);
                    Console.WriteLine(item.completed);
                }
            }
            else
            {
                Console.WriteLine(responce.StatusCode + " " + responce.ReasonPhrase);
            }
        }
    }
}


