
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using CsvHelper;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Text;
using CsvHelper.Configuration;

namespace WebApplication1.Controllers
{
    public class ImportProductsRequest
    {
        public List<Product> ProductList;
    }


    public class DataController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        List<Product> ProductList = new List<Product>(); 

        string PostProduct = "http://dev.shopiconnect.com/api/productImport/ImportDeltaProducts";

        // GET: Data
        public async Task<string> Index()
        {
            ProductList.Clear(); //reset list

            using (var reader = new StreamReader("C:/Users/Burak/source/repos/WebApplication1/WebApplication1/App_Data/sample.csv"))

            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.Delimiter = "|";
                csv.Configuration.RegisterClassMap<ProductMap>(); //mapping Auto
                csv.Configuration.MissingFieldFound = null; //sets missing field to null

                csv.Read();
                csv.ReadHeader(); //skip header

                while (csv.Read())
                {
                    var record = csv.GetRecord<Product>();
                    ProductList.Add(record); //Final list
                }

            }
           
            string serializedObject = JsonConvert.SerializeObject(new
            {
                ProductList = ProductList,
            });
            return serializedObject;
        }

        // GET: Data/Create
        public async Task<string> Create()
        {
           // client.DefaultRequestHeaders
           //.Accept
           //.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT/Content-Type

            string serializedObject = JsonConvert.SerializeObject(new
            {
                ProductList = ProductList,
            });

            StringContent httpContent = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(PostProduct, httpContent);

            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public sealed class ProductMap : ClassMap<Product>
        {
            public ProductMap()
            {
                Map(m => m.BaseProductCode).Index(0).Default(null);
                Map(m => m.ColorVariantCode).Index(1).Default(null);
                Map(m => m.Sku).Index(2).Default(null);
                Map(m => m.StockAmount).Index(3).Default(null);
                Map(m => m.Ean).Index(4).Default(null);
                Map(m => m.TaxRate).Index(5).Default(null);
                Map(m => m.Size).Index(6).Default(null);
                Map(m => m.Title).Index(7).Default(null);
                Map(m => m.Description).Index(8).Default(null);
                Map(m => m.MainCategory).Index(9).Default(null);
                Map(m => m.FirstSellingVat).Index(10).Default(null);
                Map(m => m.LastSellingVat).Index(11).Default(null);
                Map(m => m.Color).Index(12).Default(null);
                Map(m => m.Image1Link).Index(13).Default(null);
                Map(m => m.Image2Link).Index(14).Default(null);
                Map(m => m.Image3Link).Index(15).Default(null);
                Map(m => m.Image4Link).Index(16).Default(null);
                Map(m => m.Image5Link).Index(17).Default(null);
                Map(m => m.WebCategory).Index(18).Default(null);
            }
        }

    }
}

