using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Tests.Domain;
using Moq;

namespace DeskBooker.Core.Tests.Processor
{
    public class DeskBookingRequestProcessorTests
    {
        private readonly DeskBookingRequestProcessor _processor;
        private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
        private readonly DeskBookingRequest _deskBookingRequest;
        public DeskBookingRequestProcessorTests()
        {
            _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();

            //arrange
            _deskBookingRequest = new DeskBookingRequest
            {
                FirstName = "Renju",
                LastName = "Devassy",
                Email = "test@testmail.com",
                Date = new DateTime(2023, 09, 21)
            };

            _processor = new DeskBookingRequestProcessor(_deskBookingRepositoryMock.Object);

        }

        [Test]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            

            //act
            DeskBookingResult result = _processor.BookDesk(_deskBookingRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(_deskBookingRequest.FirstName, result.FirstName);
            Assert.AreEqual(_deskBookingRequest.LastName, result.LastName);
            Assert.AreEqual(_deskBookingRequest.Email, result.Email);
            Assert.AreEqual(_deskBookingRequest.Date, result.Date);

        }

        [Test]
        public void ShouldThrowExceptionWhenDeskBookingRequestValueIsNull()
        {

            //act
            var result = Assert.Throws<ArgumentNullException>(()=>_processor.BookDesk(null));

            //assert
            Assert.AreEqual("request", result.ParamName);

        }

        [Test]
        public void shouldSaveDeskBooking()
        {
            //arrange
            DeskBooking savedDeskBooking = null;
            _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(deskBooking => savedDeskBooking = deskBooking);

            //act
            _processor.BookDesk(_deskBookingRequest);

            
            _deskBookingRepositoryMock.Verify(x=>x.Save(It.IsAny<DeskBooking>()), Times.Once);

            Assert.IsNotNull(savedDeskBooking);
            Assert.AreEqual(_deskBookingRequest.FirstName, savedDeskBooking.FirstName);
            Assert.AreEqual(_deskBookingRequest.LastName, savedDeskBooking.LastName);
            Assert.AreEqual(_deskBookingRequest.Email, savedDeskBooking.Email);
            Assert.AreEqual(_deskBookingRequest.Date, savedDeskBooking.Date);
        }



    }
}
