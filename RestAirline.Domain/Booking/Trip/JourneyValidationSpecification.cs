using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Specifications;
using RestAirline.Domain.Booking.Extensions;

namespace RestAirline.Domain.Booking.Trip
{
    public class JourneyValidationSpecification : Specification<IReadOnlyList<Journey>>
    {
        public static JourneyValidationSpecification Create()
        {
            return new JourneyValidationSpecification();
        }
        
        protected override IEnumerable<string> IsNotSatisfiedBecause(IReadOnlyList<Journey> journeys)
        {
            if (journeys == null)
            {
                throw new ArgumentNullException($"{nameof(journeys)} is null");
            }
           
            if (!journeys.Any())
            {
                yield return $"{nameof(journeys)} is empty";
            }
            
            foreach (var journey in journeys)
            {
                if (!journey.DepartureDate.IsGreatThanNow())
                {
                    yield return $"Departure time of journey {journey.JourneyKey} is less than now";
                }
            }
        }
    }
}