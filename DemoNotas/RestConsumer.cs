using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace DemoNotas
{
    public static class RestConsumer
    {
        private static readonly string baseURL = "http://localhost:3001/";
        public static async Task<List<Nota>> GetAll()
        {
            try
            {

                List<Nota> data = new List<Nota>();
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseURL + "notas"))
                    {
                        using (HttpContent content = res.Content)
                        {
                            Console.WriteLine(content);
                            data = await content.ReadFromJsonAsync<List<Nota>>();
                            if (data != null)
                            {
                                return data;
                            }
                        }
                    }

                }
                data = new List<Nota>();
                return data;
            }
            catch (Exception err)
            {
                MessageBox.Show($"Servicio no disponible");
                return null;
            }

        }

        public static async Task<string> Save(string texto, int num)
        {
            try
            {
                var inputData = new Dictionary<string, string>
        {
            {"texto", texto},
            {"num", num.ToString()}
        };
                var input = new FormUrlEncodedContent(inputData);

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsync(baseURL + "newNota", input))
                    {
                        using (HttpContent content = res.Content)
                        {

                        }
                    }
                }
                return "okey";
            }
            catch (Exception err)
            {
                MessageBox.Show($"Servicio no disponible");
                return null;
            }
        }


        public static async Task<string> Delete(int num)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.DeleteAsync(baseURL + "delete" + "/" + num.ToString()))
                    {
                        using (HttpContent content = res.Content)
                        {

                        }
                    }
                }
                return "okey";
            }
            catch (Exception err)
            {
                MessageBox.Show($"Servicio no disponible");
                return null;
            }

        }
    }

}
