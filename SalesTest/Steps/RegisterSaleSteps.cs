using NUnit.Framework;
using RestCode_WebApplication.Domain.Models;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SalesTest.Steps
{
    [Binding]
    public class RegisterSaleSteps
    {
        public static RestClient restClient;
        public static RestRequest restRequest;
        public static IRestResponse response;
        private static Sale sale;

        [Given(@"the owner wants to register on sale endpoint")]
        public void GivenTheOwnerWantsToRegisterOnSaleEndpoint()
        {
            restClient = new RestClient("https://localhost:44316/");
            restRequest = new RestRequest("api/sales", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
        }
        
        [When(@"owner register a new sale")]
        public void WhenOwnerRegisterANewSale(Table table)
        {
            sale = table.CreateInstance<Sale>();

            sale = new Sale()
            {
                ClientFullName = "Lucero Alba",
            };

            restRequest.AddJsonBody(sale);
            response = restClient.Execute(restRequest);
        }
        
        [Then(@"I register my sale successfully")]
        public void ThenIRegisterMySaleSuccessfully()
        {
            Assert.That("Lucero Alba", Is.EqualTo(sale.ClientFullName));
        }
    }
}
