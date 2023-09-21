using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskBooker.Core.Tests.Domain;

namespace DeskBooker.Core.Tests.Processor
{
    public class DeskBookingRequestProcessorTests
    {
        [Test]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            //arrange
            var request = new DeskBookingRequest
            {
                FirstName = "Renju",
                LastName = "Devassy",
                Email = "test@testmail.com",
                Date = new DateTime(2023, 09, 21)
            };
            var processor = new DeskBookingRequestProcessor();

            //act
            DeskBookingResult result = processor.BookDesk(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(request.FirstName, result.FirstName);
            Assert.AreEqual(request.LastName, result.LastName);
            Assert.AreEqual(request.Email, result.Email);
            Assert.AreEqual(request.Date, result.Date);

        }

        [Test]
        public void ShouldThrowExceptionWhenDeskBookingRequestValueIsNull()
        {
            var processor = new DeskBookingRequestProcessor();

            //act
            var result = Assert.Throws<ArgumentNullException>(()=>processor.BookDesk(null));

            //assert
            Assert.AreEqual("request", result.ParamName);

        }

    }
}
