using NUnit.Framework;
using RestCode_WebApplication.Domain.Models;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace InventoryTest.Steps
{
    [Binding]
    public class AddProductToCategorySteps
    {
        public static RestClient restClient;
        public static RestRequest restRequest;
        public static IRestResponse response;
        private static Product product;

        [Given(@"the owner wants to add on product endpoint")]
        public void GivenTheOwnerWantsToAddOnProductEndpoint()
        {
            restClient = new RestClient("https://localhost:44316/");
            restRequest = new RestRequest("api/products", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
        }
        
        [When(@"owner add a new product")]
        public void WhenOwnerAddANewProduct(Table table)
        {
            product = table.CreateInstance<Product>();
            product = new Product()
            {
                Name = "Pollo a la brasa",
                Price = 20
            };
            restRequest.AddJsonBody(product);
            response = restClient.Execute(restRequest);
        }
        
        [Then(@"the product will be added succesfully")]
        public void ThenTheProductWillBeAddedSuccesfully()
        {
            Assert.That("Pollo a la brasa", Is.EqualTo(product.Name));
            Assert.That(20, Is.EqualTo(product.Price));
        }
    }
}
