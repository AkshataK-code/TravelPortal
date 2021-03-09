using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TravelPortal.Controllers;
using TravelPortal.Models;
using TravelPortal.Repository.IRepository;

namespace TravelPortalUnitTests
{
    [TestClass]
    public class TravelPortalUnitTests
    {
        CandidateController _candidateController;
        LocationController _cityController;
        ILocationService _cityRepositoryTest;

        PassengerListingsController _listingsController;
        IPassengerListingsService _listingsRepositoryTest;


        public TravelPortalUnitTests()
        {
            _candidateController = new CandidateController();

            //Mock IHttpClientFactory
            Mock<IHttpClientFactory> mockFactory = new Mock<IHttpClientFactory>();

            // We need to create explicit actual HTTPClient mock objects to passed to the extension method of
            // IHttpClientFactory i.e CreateClient().

            // Create a mock HTTPClient instance by mocking HttpMessageHandler.
            // Mocking of HttpMessageHandler will also ensure the client calling actual endpoints are faked by intercepting it.

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'name':thecodebuzz,'city':'USA'}"),
                });



            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            //Controllers with mocked IHttpClientFactory
            _cityRepositoryTest = new LocationService(mockFactory.Object);
            _cityController = new LocationController(_cityRepositoryTest);

            _listingsRepositoryTest = new PassengerListingsService(mockFactory.Object);
            _listingsController = new PassengerListingsController(_listingsRepositoryTest);            
        }

        [TestMethod]
        public void GetCandidate_WhenCalled_ReturnsOkResult()
        {
            // Act
            var result = _candidateController.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<string>));            
        }

        [TestMethod]
        public void GetCity_WhenCalled_ReturnsOkResult()
        {
            // Act
            var result = _cityController.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
        }

        [TestMethod]
        public void GetPassengerListings_WhenCalled_ReturnsOkResult()
        {
            // Act
            int numberOfPassengers = 2;
            var result = _listingsController.Get(numberOfPassengers);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
        }

        [TestMethod]
        public void GetPassengerListings_When_Number_Of_Passengers_Is_LessThanOrEqualTo_Zero()
        {
            // Act
            int numberOfPassengers = 0;
            var result = _listingsController.Get(numberOfPassengers);
            string str = result.ToString();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPassengerListings_When_Number_Of_Passengers_Is_Out_Of_Range()
        {
            // Act
            int numberOfPassengers = 100;
            var result = _listingsController.Get(numberOfPassengers);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Task<ActionResult>));
            Assert.IsNotNull(result);
        }
    }



}
