using NUnit.Framework;
using RestCode_WebApplication.Domain.Models;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SalesTest.Steps
{
    [Binding]
    public class AddDetailsToSaleSteps
    {
        public static RestClient restClient;
        public static RestRequest restRequest;
        public static IRestResponse response;
        private static SaleDetail saleDetail;

        [Given(@"the owner wants to register on sale detail endpoint")]
        public void GivenTheOwnerWantsToRegisterOnSaleDetailEndpoint()
        {
            restClient = new RestClient("https://localhost:44316/");
            restRequest = new RestRequest("api/sale_details", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
        }
        
        [When(@"owner register a new sale detail")]
        public void WhenOwnerRegisterANewSaleDetail(Table table)
        {
            saleDetail = table.CreateInstance<SaleDetail>();

            saleDetail = new SaleDetail()
            {
                Description = "Extra salad",
                Quantity = 2,
            };

            restRequest.AddJsonBody(saleDetail);
            response = restClient.Execute(restRequest);
        }
        
        [Then(@"I register my sale detail successfully")]
        public void ThenIRegisterMySaleDetailSuccessfully()
        {
            Assert.That("Extra salad", Is.EqualTo(saleDetail.Description));
            Assert.That(2, Is.EqualTo(saleDetail.Quantity));
        }
    }
}
